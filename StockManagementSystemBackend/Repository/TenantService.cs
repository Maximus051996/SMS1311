    using Dapper;
using StockManagementSystemBackend.DTO;
using StockManagementSystemBackend.Interface;
using System.Data;
using System.Linq;

namespace StockManagementSystemBackend.Repository
{
    public class TenantService : ITenant
    {
        public async Task<int> EnableDisableTenants(long tenantId, bool IsActive, IDbConnection dbconnection)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("TenantId", tenantId, DbType.Int32);
                parameters.Add("IsActive", IsActive, DbType.Boolean);

                using (var connection = dbconnection)
                {
                    return await connection.ExecuteAsync(SQL.EnableDisableTenant, parameters);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<TenantDTO> GetTenantById(long tenantId, IDbConnection dbconnection)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("TenantId", tenantId, DbType.Int32);

                using (var connection = dbconnection)
                {
                    return await connection.QueryFirstOrDefaultAsync<TenantDTO>(SQL.GetTenantById, parameters);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<IEnumerable<TenantDTO>> GetTenants(IDbConnection dbconnection)
        {
            try
            {
                using (var connection = dbconnection)
                {
                    return (await connection.QueryAsync<TenantDTO>(SQL.GetTenants)).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<int> InsertTenant(string TenantName, IDbConnection dbconnection)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("TenantName", TenantName, DbType.String);
                parameters.Add("IsActive", true, DbType.Boolean);

                using (var connection = dbconnection)
                {
                    return await connection.QueryFirstOrDefaultAsync<int>(SQL.InsertTenant, parameters);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<int> UpdateTenant(long TenantId, string TenantName, IDbConnection dbconnection)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("TenantName", TenantName, DbType.String);
                parameters.Add("TenantId", TenantId, DbType.Int16);


                using (var connection = dbconnection)
                {
                    return await connection.ExecuteAsync(SQL.UpdateTenant, parameters);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<IEnumerable<dynamic>> TotalUserByTenant(IDbConnection dbconnection)
        {
            try
            {
                using (var connection = dbconnection)
                {
                    return await connection.QueryAsync<dynamic>(SQL.TotalUserByTenanat);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public async Task<List<dynamic>> GetRoleCountByTenant(List<string> roleResult, IDbConnection dbconnection)
        {
            List<dynamic> workerCountByRolName = new List<dynamic>();
            try
            {
                using (var connection = dbconnection)
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    foreach (var roleName in roleResult)
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("RoleName", roleName, DbType.String);
                        int[] workerCountListByRole = (await connection.QueryAsync<int>(SQL.GetRoleCountByTenant, parameters)).ToArray();
                        workerCountByRolName.Add(new { RoleName = roleName, RoleCount = workerCountListByRole });//
                    }
                }
                return workerCountByRolName;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<int> ValidUserTenantId(string tenantName, string userName, IDbConnection dbconnection)
        {
            try
            {
                using (var connection = dbconnection)
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("TenantName", tenantName, DbType.String);
                    parameters.Add("UserName", userName, DbType.String);
                    return await connection.QueryFirstOrDefaultAsync<int>(SQL.ValidUserTenantId, parameters);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}