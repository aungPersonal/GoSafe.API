using GoSafe.Dto.Common;

namespace GoSafe.API.Dtos.Trip
{
    #region Bus
    public class SaveBusRequest
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string PlateNo { get; set; } = null!;
        public string? Description { get; set; }
    }

    public class GetBusListRequest : PaginationRequest
    {
        public string? Name { get; set; }
        public string? PlateNo { get; set; }
    }


    public class BusModel
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string PlateNo { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class GetBusListResponse
    {
        public Response Result { get; set; } = new Response();
        public List<BusModel> Items { get; set; } = new();
        public int TotalItem { get; set; }
    }
    #endregion
}
