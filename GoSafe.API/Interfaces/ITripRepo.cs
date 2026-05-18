using GoSafe.API.Dtos.Trip;
using GoSafe.Dto.Common;

namespace GoSafe.API.Interfaces
{
    public interface ITripRepo
    {

        #region Bus
        Task<CommonResult> SaveBus(SaveBusRequest req, long loginUserId);
        Task<CommonResult> DeleteBus(long Id, long loginUserId);
        Task<GetBusListResponse> GetBusList(GetBusListRequest req);
        #endregion

    }
}
