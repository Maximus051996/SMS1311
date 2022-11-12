using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockManagementSystemBackend.Models
{
    public class ProductMaster
    {
        [Key]
        public long ProductId { get; set; }
        [Required]
        public string? ProductName { get; set; }
        [Required]
        [ForeignKey("CompanyMaster")]
        public long? CompantyId { get; set; }
        public virtual CompanyMaster? CompanyMaster { get; set; }
        [Required]
        [ForeignKey("TenantMaster")]
        public int TenantId { get; set; }
        public virtual TenantMaster? TenantMaster { get; set; }

        public long? Quantity { get; set; }
        public decimal? MRP { get; set; }
        public decimal? Percentage { get; set; }

        public string? Formula { get; set; }
        public DateTime? CreatedDateTime { get; set; }

        public DateTime? ModifiedDateTime { get; set; }

        public bool IsActive { get; set; }
    }
}
