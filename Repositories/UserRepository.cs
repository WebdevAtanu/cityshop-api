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
        public UserRepository(ApplicationDBContext context, EncryptionService encryptionService, JwtTokenService jwtTokenService)
        {
            _context = context;
            _encryptionService = encryptionService;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<bool> IsEmailExists(string email)
        {
            return await _context.Users.AnyAsync(u => u.UserEmail == email);
        }

        public async Task<bool> UserRegister(RegisterRequest registerRequest)
        {
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

        public async Task<LoginResponse?> UserLogin(LoginRequest loginRequest)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserEmail == loginRequest.Email);
            if (user == null)
            {
                return null;
            }
            var Password = _encryptionService.Decrypt(user.Password);
            if (loginRequest.Password != Password)
            {
                return null;
            }
            return new LoginResponse
            {
                Token = _jwtTokenService.GenerateToken(user.UserId.ToString(), user.UserEmail)
            };
        }
    }
}
