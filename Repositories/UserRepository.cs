using cityshop_api.DTO;
using cityshop_api.Interfaces;
using cityshop_api.Model;
using Microsoft.EntityFrameworkCore;
using cityshop_api.Helpers;
using cityshop_api.Services;

namespace cityshop_api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly EncryptionService _encryptionService;
        private readonly JwtTokenService _jwtTokenService;
        private readonly EmailValidationService _emailValidationService;
        public UserRepository(ApplicationDBContext context, EncryptionService encryptionService, JwtTokenService jwtTokenService, EmailValidationService emailValidationService)
        {
            _context = context;
            _encryptionService = encryptionService;
            _jwtTokenService = jwtTokenService;
            _emailValidationService = emailValidationService;
        }

        public async Task<bool> IsEmailExists(string email)
        {
            return await _context.Users.AnyAsync(u => u.UserEmail == email);
        }

        public async Task<bool> UserRegister(RegisterRequest registerRequest)
        {
            bool otpValid = await _emailValidationService.ValidateOtp(registerRequest.Email, registerRequest.Otp);
            if (!otpValid)
                throw new Exception("Invalid OTP provided");

            var hasedPassword = _encryptionService.Encrypt(registerRequest.Password);

            await _context.Users.AddAsync(new User
            {
                UserName = registerRequest.FullName,
                UserEmail = registerRequest.Email,
                UserPhone = registerRequest.Phone,
                Password = hasedPassword
            });

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<UserResponse?> UserLogin(LoginRequest loginRequest)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserEmail == loginRequest.Email);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var Password = _encryptionService.Decrypt(user.Password ?? "");
            if (loginRequest.Password != Password)
            {
                throw new Exception("Invalid password");
            }

            string newToken = _jwtTokenService.GenerateToken(user.UserId.ToString(), user.UserEmail ?? "");
            LoginResponse loginResponse = new()
            {
                Token = newToken,
            };

            await _context.ActiveUsers.AddAsync(new ActiveUser()
            {
                MapId = Guid.NewGuid(),
                UserId = user.UserId,
                AccessToken = newToken,
                RefreshToken = "",
                LoginDateTime = DateTime.Now,
            });

            await _context.SaveChangesAsync();

            return new UserResponse
            {
                UserId = user.UserId,
                UserName = user.UserName,
                UserEmail = user.UserEmail,
                UserPhone = user.UserPhone,
                Role = user.Role,
                IsActive = user.IsActive,
                LoginResponse = loginResponse
            };
        }
    }
}
