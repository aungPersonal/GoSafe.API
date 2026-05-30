using GoSafe.Dto.Common;
using GoSafe.Dto.User;

namespace GoSafe.API.Interfaces
{
    public interface IUserRepo
    {
        Task<LoginResponse> Login(LoginRequest req);
        Task<RefreshTokenResponse> RefreshToken(RefreshTokenRequest req);
        Task<RegisterResponse> Register(RegisterRequest req);

        #region CRUD
        Task<CommonResult> SaveUser(SaveUserRequest req, long loginUserId);
        Task<CommonResult> DeleteUser(long Id, long loginUserId);
        Task<GetUserListResponse> GetUserList(GetUserListRequest req);
        #endregion
    }
}
