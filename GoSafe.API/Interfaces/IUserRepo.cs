using GoSafe.Dto.User;

namespace GoSafe.API.Interfaces
{
    public interface IUserRepo
    {
        Task<RegisterResponse> Register(RegisterRequest req);
    }
}
