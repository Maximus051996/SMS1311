using StockManagementSystemBackend.DTO;
using System.Data;

namespace StockManagementSystemBackend.Interface
{
    public interface ICompany
    {
        public Task<int> InsertBulkCompany(List<CompanyDTO> companies, int TenantId, IDbConnection dbconnection);

        public Task<int> InsertCompany(string CompanyName, int TenantId, string Priroty, IDbConnection dbconnection);
    }
}
