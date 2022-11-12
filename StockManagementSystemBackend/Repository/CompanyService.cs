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
                    string Query = string.Empty;
                    if (TenantId == 1)
                    {
                        Query = SQL.InsertCompanyNewMaaDurga;
                    }
                    return await connection.ExecuteAsync(Query, parameters);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }
    }
}
