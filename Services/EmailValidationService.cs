using cityshop_api.Model;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using MimeKit;

namespace cityshop_api.Services
{
    public class EmailValidationService
    {
        private readonly IConfiguration _config;
        private readonly ApplicationDBContext _context;

        public EmailValidationService(IConfiguration configuration, ApplicationDBContext context)
        {
            _config = configuration;
            _context = context;
        }

        public async Task<bool> CanSendOtp(string email)
        {
            var timeLimit = DateTime.Now.AddMinutes(-5);

            var otpCount = await _context.OtpStores
                .Where(x => x.Email == email && x.createTime >= timeLimit)
                .CountAsync();

            return otpCount < 3;
        }

        public async Task SendOtpEmail(string toEmail, string otp)
        {
            if (!await CanSendOtp(toEmail))
                throw new Exception("Too many OTP requests. Try again later.");

            var emailAddress = _config["EmailSettings:Email"];
            var password = _config["EmailSettings:Password"];
            var host = _config["EmailSettings:Host"];
            var port = _config.GetValue<int>("EmailSettings:Port");

            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("CityShop", emailAddress));
            message.To.Add(MailboxAddress.Parse(toEmail));

            message.Subject = "Your OTP Verification Code";

            message.Body = new TextPart("html")
            {
                Text = $@"
                <h2>OTP Verification</h2>
                <p>Your OTP code is:</p>
                <h1>{otp}</h1>
                <p>This code will expire in 5 minutes.</p>"
            };

            // Save OTP first
            var otpEntity = new OtpStore
            {
                TrackId = Guid.NewGuid(),
                Email = toEmail,
                Otp = otp,
                createTime = DateTime.Now,
                ExpiryTime = DateTime.Now.AddMinutes(5),
                IsUsed = false
            };

            await _context.OtpStores.AddAsync(otpEntity);
            await _context.SaveChangesAsync();

            using var smtp = new SmtpClient();

            await smtp.ConnectAsync(host, port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(emailAddress, password);
            await smtp.SendAsync(message);
            await smtp.DisconnectAsync(true);
        }

        public async Task<bool> ValidateOtp(string emailAddress, string otp)
        {
            var otpData = await _context.OtpStores
                .Where(o => o.Email == emailAddress)
                .OrderByDescending(o => o.createTime)
                .FirstOrDefaultAsync();

            if (otpData == null)
                return false;

            if (otpData.IsUsed == true)
                return false;

            if (otpData.ExpiryTime < DateTime.Now)
                return false;

            if (otpData.Otp != otp)
                return false;

            // Mark OTP as used
            otpData.IsUsed = true;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}