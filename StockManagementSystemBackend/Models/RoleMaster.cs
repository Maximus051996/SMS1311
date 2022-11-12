using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockManagementSystemBackend.Models
{
    public class RoleMaster
    {
        [Key]
        public long RoleId { get; set; }
        [Required]
        public string? RoleName { get; set; }
        [Required]
        [ForeignKey("TenantMaster")]
        public int TenantId { get; set; }
        public virtual TenantMaster? TenantMaster { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
        public bool IsActive { get; set; }
    }
}
