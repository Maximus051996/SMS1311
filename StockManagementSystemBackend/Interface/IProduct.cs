using StockManagementSystemBackend.DTO;
using System.Data;

namespace StockManagementSystemBackend.Interface
{
    public interface IProduct
    {
        public Task<int> InserProduct(string ProductName,string CompanyName, int TenantId, string Priroty, IDbConnection dbconnection);
        public Task<IEnumerable<ProductDTO>> GetAllProductsByTenant(int TenantId, IDbConnection dbconnection);
        public Task<ProductDTO> GetProductByTenant(int TenantId, int ProductId, IDbConnection dbconnection);
        public Task<int> DeleteProductByTenant(int TenantId, int ProductId, IDbConnection dbconnection);
    }
}
