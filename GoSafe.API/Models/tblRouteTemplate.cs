namespace GoSafe.API.Models
{
    public class tblRouteTemplate
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // Audit Fields
        public DateTimeOffset CreatedAt { get; set; }
        public long CreatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
