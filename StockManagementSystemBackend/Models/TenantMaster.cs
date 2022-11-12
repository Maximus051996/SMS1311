using System.ComponentModel.DataAnnotations;

namespace StockManagementSystemBackend.Models
{
    public class TenantMaster
    {
        [Key]
        public int TenantId { get; set; }
        [Required]
        public string? TenantName { get; set; }

        public DateTime? CreatedDateTime { get; set; }

        public DateTime? ModifiedDateTime { get; set; }

        public bool IsActive { get; set; }
    }
}
