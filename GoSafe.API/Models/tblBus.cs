namespace GoSafe.API.Models
{
    public class tblBus
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string PlateNo { get; set; }
        public string Description { get; set; }

        // Audit Fields
        public DateTime CreatedAt { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
