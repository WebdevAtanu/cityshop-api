using cityshop_api.DTO;

namespace cityshop_api.Interfaces
{
    public interface IUserService
    {
        Task<bool> UserRegister(RegisterRequest registerRequest);
        Task<LoginResponse?> UserLogin(LoginRequest loginRequest);
    }
}
