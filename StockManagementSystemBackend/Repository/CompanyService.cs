using Dapper;
using StockManagementSystemBackend.DTO;
using StockManagementSystemBackend.Interface;
using System.Data;

namespace StockManagementSystemBackend.Repository
{
    public class CompanyService : ICompany
    {
        public async Task<int> InsertBulkCompany(List<CompanyDTO> companies, int TenantId, IDbConnection dbconnection)
        {
            try
            {

                DataTable dt = new DataTable();
                dt.Columns.Add("CompanyName", typeof(string));
                dt.Columns.Add("Priroty", typeof(string));
                foreach (var company in companies)
                {
                    dt.Rows.Add(company.CompanyName, company.Priroty);
                }
                var parameter = new
                {
                    companytype = dt.AsTableValuedParameter("[dbo].[CompanyType]"),
                    tenantId = TenantId,
                };
                using (var connection = dbconnection)
                {
                    var result = await connection.ExecuteAsync(SQL.InsertBulkCompany, parameter, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }

        public async Task<int> InsertCompany(string CompanyName, int TenantId, string Priroty, IDbConnection dbconnection)
        {
            try
            {
                
                var parameters = new DynamicParameters();
                parameters.Add("CompanyName", CompanyName, DbType.String);              
                parameters.Add("Priroty", Priroty, DbType.String);
                parameters.Add("TenantId", TenantId, DbType.Int16);
                parameters.Add("IsActive", true, DbType.Boolean);

                using (var connection = dbconnection)
                {
                  return await connection.ExecuteAsync(SQL.InsertCompany, parameters);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }

        public async Task<IEnumerable<CompanyDTO>> GetAllCompaniesByTenant(int TenantId, IDbConnection dbconnection)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("TenantId", TenantId, DbType.Int16);
                using (var connection = dbconnection)
                {
                  return (await connection.QueryAsync<CompanyDTO>(SQL.GetAllCompaniesbyTenantId, parameters)).ToList();
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex); 
            }
        }

        public async Task<CompanyDTO> GetCompanyByTenant(int TenantId, int CompanyId, IDbConnection dbconnection)
        {
            try
            {
                var parameters = new DynamicParameters();
                string Query = string.Empty;
                parameters.Add("TenantId", TenantId, DbType.Int32);
                parameters.Add("CompanyId", CompanyId, DbType.Int32);    
                using (var connection = dbconnection)
                {
                    return await connection.QueryFirstOrDefaultAsync<CompanyDTO>(SQL.GetComapanyByTenantId, parameters);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<int> DeleteCompanyByTenant(int TenantId, int CompanyId, IDbConnection dbconnection)
        {
            try
            {
                var parameters = new DynamicParameters();
                string Query = string.Empty;
                parameters.Add("TenantId", TenantId, DbType.Int32);
                parameters.Add("CompanyId", CompanyId, DbType.Int32);            
                using (var connection = dbconnection)
                {
                    return await connection.ExecuteAsync(SQL.DeleteCompanyByTenantId, parameters);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
