namespace cityshop_api.DTO
{
    public class UserResponse
    {
        public Guid UserId { get; set; }
        public string? UserName { get; set; }
        public string? UserPhone { get; set; }
        public string? UserEmail { get; set; }
        public string? Role { get; set; }
        public bool? IsActive { get; set; } = true;
        public LoginResponse? LoginResponse { get; set; }
    }

    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
    }
}
