using System.ComponentModel.DataAnnotations.Schema;

namespace GoSafe.API.Models
{
    public class TblBus : EntityCommon
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string PlateNo { get; set; }
        public string? Description { get; set; }
    }
}
