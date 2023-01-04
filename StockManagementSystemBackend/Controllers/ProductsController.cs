using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockManagementSystemBackend.Data;
using StockManagementSystemBackend.Interface;

namespace StockManagementSystemBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ProductsController : ControllerBase
    {
        public IConfiguration _configuration;
        public readonly ApplicationDbContext _applicationDbContext;
        private IProduct _IProduct;
        private ITenant _ITenant;
        public ProductsController(IConfiguration configuration, ApplicationDbContext applicationDbContext, IProduct IProduct, ITenant ITenant)
        {
            _configuration = configuration;
            _applicationDbContext = applicationDbContext;
            _IProduct = IProduct;
            _ITenant = ITenant;
        }

    }
}
