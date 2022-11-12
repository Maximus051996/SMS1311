namespace StockManagementSystemBackend.DTO
{
    public class TenantDTO
    {
        public int TenantId { get; set; }

        public string? TenantName { get; set; }

        public DateTime? CreatedDateTime { get; set; }

        public DateTime? ModifiedDateTime { get; set; }

        public bool IsActive { get; set; }
    }

    public class TenantEnableDisableDTO
    {
        public int TenantId { get; set; }
        public bool IsActive { get; set; }
    }

    public class TenantUpdateDTO
    {
        public int TenantId { get; set; }
        public string TenantName { get; set; }
    }

}
