using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoSafe.API.Models
{
    public class TblUser : EntityCommon
    {
        [Key]
        public long Id { get; set; } // BIGINT maps to long
        public string LoginName { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string FullName { get; set; }

        [MaxLength(50)]
        public string? Phone { get; set; }

        public int? RoleId { get; set; } 

        public bool IsDeleted { get; set; } = false;

        [MaxLength(400)]
        public string PasswordHash { get; set; }

        [MaxLength(50)]
        public string VCode { get; set; }
        
    }
}
