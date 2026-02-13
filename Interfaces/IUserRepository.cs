using cityshop_api.DTO;

namespace cityshop_api.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> UserRegister(RegisterRequest registerRequest);
        Task<LoginResponse?> UserLogin(LoginRequest loginRequest);
        Task<bool> IsEmailExists(string email);
    }
}
