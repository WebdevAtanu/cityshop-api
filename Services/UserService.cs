using cityshop_api.Interfaces;
namespace cityshop_api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        public async Task<bool> UserRegister(DTO.RegisterRequest registerRequest)
        {
            try
            {
                bool isEmailExist = await _userRepository.IsEmailExists(registerRequest.Email);
                if (isEmailExist)
                {
                    throw new Exception("Email already exists.");
                }
                return await _userRepository.UserRegister(registerRequest);
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public async Task<DTO.LoginResponse?> UserLogin(DTO.LoginRequest loginRequest)
        {
            return await _userRepository.UserLogin(loginRequest);
        }
    }
}
