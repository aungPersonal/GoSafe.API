using GoSafe.Dto.User;

namespace GoSafe.API.Interfaces
{
    public interface IUserRepo
    {
        Task<LoginResponse> Login(LoginRequest req);
        Task<RefreshTokenResponse> RefreshToken(RefreshTokenRequest req);
        Task<RegisterResponse> Register(RegisterRequest req);
    }
}
