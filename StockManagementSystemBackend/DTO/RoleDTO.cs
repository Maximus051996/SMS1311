namespace StockManagementSystemBackend.DTO
{
    public class RoleDTO
    {
        public long? RoleId { get; set; }
        public string? RoleName { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
        public int TenantId { get; set; }
        public bool IsActive { get; set; }
    }
}
