using System.ComponentModel.DataAnnotations;

namespace GoSafe.API.Dtos.Model
{
    public class RoleType
    {
        public int Id { get; set; }

        [StringLength(300)]
        public string Name { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }
    }
}
