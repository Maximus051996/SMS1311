using StockManagementSystemBackend.DTO;
using System.Data;

namespace StockManagementSystemBackend.Interface
{
    public interface IRole
    {
        public Task<int> InsertRole(string RoleName, int TenantId, IDbConnection dbconnection);
        public Task<int> UpdateRole(long RoleId, string RoleName, int TenantId, IDbConnection dbconnection);
        public Task<IEnumerable<RoleDTO>>  GetRoles(int TenantId,IDbConnection dbconnection);
        public Task<IEnumerable<string>> GetAllActiveRoles(IDbConnection dbconnection);
        public Task<int> EnableDisableRole(long roleId, int TenantId, bool IsActive, IDbConnection dbconnection);
        public Task<RoleDTO> GetRoleById(long roleId, int TenantId, IDbConnection dbconnection);

    }
}
