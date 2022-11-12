using Dapper;
using StockManagementSystemBackend.DTO;
using StockManagementSystemBackend.Interface;
using System.Data;

namespace StockManagementSystemBackend.Repository
{
    public class RoleService : IRole
    {
        public async Task<int> EnableDisableRole(long roleId, int TenantId, bool IsActive, IDbConnection dbconnection)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("RoleId", roleId, DbType.Int32);
                parameters.Add("TenantId", TenantId, DbType.Int32);
                parameters.Add("IsActive", IsActive, DbType.Boolean);

                using (var connection = dbconnection)
                {
                    return await connection.ExecuteAsync(SQL.EnableDisableRole, parameters);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<RoleDTO> GetRoleById(long roleId, int TenantId, IDbConnection dbconnection)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("RoleId", roleId, DbType.Int32);
                parameters.Add("TenantId", TenantId, DbType.Int32);
                using (var connection = dbconnection)
                {
                    return await connection.QueryFirstOrDefaultAsync<RoleDTO>(SQL.GetRoleById, parameters);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<IEnumerable<RoleDTO>> GetRoles(int TenantId, IDbConnection dbconnection)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("TenantId", TenantId, DbType.Int64);
                using (var connection = dbconnection)
                {
                    return (await connection.QueryAsync<RoleDTO>(SQL.GetRoles, parameters)).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }

        public async Task<IEnumerable<string>> GetAllActiveRoles(IDbConnection dbconnection)
        {
            try
            {
                using (var connection = dbconnection)
                {
                    return (await connection.QueryAsync<string>(SQL.GetAllActiveRoles)).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }

        public async Task<int> InsertRole(string RoleName,int TenantId, IDbConnection dbconnection)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("RoleName", RoleName, DbType.String);
                parameters.Add("TenantId", TenantId, DbType.Int16);
                parameters.Add("IsActive", true, DbType.Boolean);

                using (var connection = dbconnection)
                {
                    return await connection.ExecuteAsync(SQL.InsertRole, parameters);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<int> UpdateRole(long RoleId, string RoleName, int TenantId, IDbConnection dbconnection)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("RoleName", RoleName, DbType.String);
                parameters.Add("RoleId", RoleId, DbType.Int16);
                parameters.Add("TenantId", TenantId, DbType.Int32);

                using (var connection = dbconnection)
                {
                    return await connection.ExecuteAsync(SQL.UpdateRole, parameters);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
