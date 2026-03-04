using cityshop_api.DTO;

namespace cityshop_api.Interfaces
{
    public interface IUserService
    {
        Task<bool> UserRegister(RegisterRequest registerRequest);
        Task<UserResponse?> UserLogin(LoginRequest loginRequest);
    }
}
