using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockManagementSystemBackend.Models
{
    public class CompanyMaster
    {
        [Key]
        public long CompanyId { get; set; }
        [Required]
        public string? CompanyName { get; set; }
        [Required]
        [ForeignKey("TenantMaster")]
        public int TenantId { get; set; }
        public virtual TenantMaster? TenantMaster { get; set; }

        public DateTime? CreatedDateTime { get; set; }

        public DateTime? ModifiedDateTime { get; set; }

        public string? Priroty { get; set; }

        public bool IsActive { get; set; }

    }
}
