using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockManagementSystemBackend.Models
{
    public class FormulaMaster
    {
        [Key]
        public long FId { get; set; }

        [Required]
        public string? Formula { get; set; }

        [Required]
        [ForeignKey("TenantMaster")]
        public int TenantId { get; set; }
        public virtual TenantMaster? TenantMaster { get; set; }

        public bool IsActive { get; set; }
    }
}
