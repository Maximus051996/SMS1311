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
        public decimal? MRP { get; set; }
        public DateTime? ManufacturingDate { get; set; }
        public long? Quantity { get; set; }
        public decimal? Default_Percentage { get; set; }
        public decimal? DiscountRate_Percentage { get; set; }
        public decimal? Special_Percentage { get; set; }
        public decimal? ActualPrice { get; set; }
        public DateTime? CreatedDateTime { get; set; }
       
        [ForeignKey("FormulaMaster")]
        public long? FId { get; set; }
        public virtual FormulaMaster? FormulaMaster { get; set; }
        public bool IsActive { get; set; }
    }
}
