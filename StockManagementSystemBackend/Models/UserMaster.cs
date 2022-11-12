using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockManagementSystemBackend.Models
{
    public class UserMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [Required]
        [ForeignKey("Role")]
        public long RoleId { get; set; }
        public virtual RoleMaster? Role { get; set; }
        public string? UserName { get; set; }
        public string? UserPassword { get; set; }
        public string? Email { get; set; }
        public long? ContactNumber { get; set; }

        public string? Address { get; set; }

        [Required]
        [ForeignKey("TenantMaster")]
        public int TenantId { get; set; }
        public virtual TenantMaster? TenantMaster { get; set; }
        public DateTime? CreatedDateTime { get; set; }

        public string? IsUpdated { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
        public bool IsActive { get; set; }
    }
}
