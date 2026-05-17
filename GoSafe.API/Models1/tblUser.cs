using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoSafe.API.Models
{
    public class tblUser
    {
        [Key]
        public long Id { get; set; } // BIGINT maps to long

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Phone { get; set; }

        public int? RoleId { get; set; } // Foreign Key

        public bool IsDeleted { get; set; } = false;

        [MaxLength(400)]
        public string PasswordHash { get; set; }

        [MaxLength(50)]
        public string VCode { get; set; }

        // Audit Fields
        public DateTime CreatedAt { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
