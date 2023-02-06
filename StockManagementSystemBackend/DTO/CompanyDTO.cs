using Fingers10.ExcelExport.Attributes;

namespace StockManagementSystemBackend.DTO
{
    public class CompanyDTO
    {
        public int CompanyId { get; set; }

        [IncludeInReport(Order=1)]
        public string? CompanyName { get; set; }

        [IncludeInReport(Order = 2)]
        public string? Priroty { get; set; }
        public bool? IsActive { get; set; }
    }
}
