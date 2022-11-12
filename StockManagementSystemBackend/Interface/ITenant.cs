using StockManagementSystemBackend.DTO;
using System.Data;

namespace StockManagementSystemBackend.Interface
{
    public interface ITenant
    {
        public Task<int> InsertTenant(string TenantName, IDbConnection dbconnection);
        public Task<int> UpdateTenant(long TenantId, string TenantName, IDbConnection dbconnection);
        public Task<IEnumerable<TenantDTO>> GetTenants(IDbConnection dbconnection);
        public Task<int> EnableDisableTenants(long tenantId, bool IsActive, IDbConnection dbconnection);
        public Task<TenantDTO> GetTenantById(long tenantId, IDbConnection dbconnection);
        public Task<int> ValidUserTenantId(string tenantName,string userName, IDbConnection dbconnection);
        public Task<IEnumerable<dynamic>> TotalUserByTenant(IDbConnection dbconnection);
        public Task<List<dynamic>> GetRoleCountByTenant(List<string> roleResult,IDbConnection dbconnection);
    }
}
