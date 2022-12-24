using StockManagementSystemBackend.DTO;
using System.Data;

namespace StockManagementSystemBackend.Interface
{
    public interface ICompany
    {
        public Task<int> InsertBulkCompany(List<CompanyDTO> companies, int TenantId, IDbConnection dbconnection);
        public Task<int> InsertCompany(string CompanyName, int TenantId, string Priroty, IDbConnection dbconnection);
        public Task<IEnumerable<CompanyDTO>> GetAllCompaniesByTenant(int TenantId, IDbConnection dbconnection);
        public Task<CompanyDTO> GetCompanyByTenant(int TenantId, int CompanyId, IDbConnection dbconnection);
        public Task<int> DeleteCompanyByTenant(int TenantId, int CompanyId, IDbConnection dbconnection);

    }
}
