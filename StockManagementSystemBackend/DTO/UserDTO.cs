using StockManagementSystemBackend.Models;
using System.ComponentModel;

namespace StockManagementSystemBackend.DTO
{
    public class UserDTO
    {
        public int? UserId { get; set; }
        public string? UserName { get; set; }
        public long? RoleId { get; set; }
        public string? RoleName { get; set; }
        public string? Address { get; set; }
        public string? UserPassword { get; set; }
        public string? Email { get; set; }
        public long? ContactNumber { get; set; }
        public int TenantId { get; set; }
        public string? TenantName { get; set; }
        public bool? IsActive { get; set; }
        public string? Operation { get; set; }

    }
    public class UserEnableDisableDTO
    {
        public int UserId { get; set; }
        public bool IsActive { get; set; }
    }
    public class LoginDTO
    {
        public string? UserName { get; set; }
        public string? UserPassword { get; set; }
    }

    public class TotalWorkerByTenantDTO
    {
        public string? TenantName { get; set; }       
        public int TenantCount { get; set; }
        public string? TenantColor { get; set; }
    }
}
