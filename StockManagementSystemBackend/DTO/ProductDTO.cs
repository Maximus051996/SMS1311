namespace StockManagementSystemBackend.DTO
{
    public class ProductDTO
    {
        public long ProductId { get; set; }       
        public string? ProductName { get; set; }       
        public long? CompantyId { get; set; }       
        public int TenantId { get; set; }
        public long? Quantity { get; set; }
        public decimal? MRP { get; set; }
        public decimal? Percentage { get; set; }
        public string? Formula { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
        public bool IsActive { get; set; }
    }
}
