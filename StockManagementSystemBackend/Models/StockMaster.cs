using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockManagementSystemBackend.Models
{
    public class StockMaster
    {
        public long StcpId { get; set; }       
     
        public DateTime? CreatedDateTime { get; set; }
        public bool IsActive { get; set; }

    }
}
