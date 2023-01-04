using StockManagementSystemBackend.DTO;
using StockManagementSystemBackend.Interface;
using System.Data;

namespace StockManagementSystemBackend.Repository
{
    public class ProductService : IProduct
    {
        Task<int> IProduct.DeleteProductByTenant(int TenantId, int ProductId, IDbConnection dbconnection)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<ProductDTO>> IProduct.GetAllProductsByTenant(int TenantId, IDbConnection dbconnection)
        {
            throw new NotImplementedException();
        }

        Task<ProductDTO> IProduct.GetProductByTenant(int TenantId, int ProductId, IDbConnection dbconnection)
        {
            throw new NotImplementedException();
        }

        Task<int> IProduct.InserProduct(string ProductName, string CompanyName, int TenantId, string Priroty, IDbConnection dbconnection)
        {
            throw new NotImplementedException();
        }
    }
}
