using cityshop_api.Helpers;
using cityshop_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cityshop_api.Controllers
{
    public class OtpSendController : BaseController
    {
        private readonly EmailValidationService _emailValidationService;
        public OtpSendController(EmailValidationService emailValidationService)
        {
            _emailValidationService = emailValidationService;
        }

        public class UserMail
        {
            public string? Email { get; set; }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Index(UserMail userMail)
        {
            if (!string.IsNullOrEmpty(userMail.Email))
            {
                string otp = OtpHelper.GenerateOtp();
                await _emailValidationService.SendOtpEmail(userMail.Email, otp);
            }
            return Success("OTP sent to the email");
        }
    }
}
