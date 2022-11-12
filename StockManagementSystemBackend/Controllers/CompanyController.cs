using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using StockManagementSystemBackend.Data;
using StockManagementSystemBackend.Interface;

namespace StockManagementSystemBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        public IConfiguration _configuration;

        public readonly ApplicationDbContext _applicationDbContext;
        private ICompany _ICompany;
        private ITenant _ITenant;


        public CompanyController(IConfiguration configuration, ApplicationDbContext applicationDbContext, ICompany ICompany, ITenant ITenant)
        {
            _configuration = configuration;
            _applicationDbContext = applicationDbContext;
            _ICompany = ICompany;
            _ITenant = ITenant;
        }

        [HttpPost("InsertCompany")]
        public async Task<IActionResult> InsertCompany(string CompanyName, string TenantName,string UserName,string Priroty)
        {
            int TenantId = await _ITenant.ValidUserTenantId(TenantName, UserName, new SqlConnection(_configuration.GetConnectionString("DefaultConnection")));
            if (string.IsNullOrEmpty(CompanyName) && string.IsNullOrEmpty(Priroty))
                return Ok(new { Message = Enums.Failure.GetDescription(), IsSuccess = "False" });
            else if(TenantId>0)
            {
                {
                    var result = await _ICompany.InsertCompany(CompanyName, TenantId,Priroty, new SqlConnection(_configuration.GetConnectionString("DefaultConnection")));

                    if (result > 0)
                    {
                        return Ok(new { Message = Enums.Insert.GetDescription(), IsSuccess = "True" });
                    }
                    else if (result == 0|| result == -1)
                    {
                        return Ok(new { Message = Enums.Dublicate.GetDescription(), IsSuccess = "False" });
                    }
                    else
                    {
                        return Ok(new { Message = Enums.Failure.GetDescription(), IsSuccess = "False" });
                    }
                }
            }
            else
            {
                return Ok(new { Message = Enums.UserInvalid.GetDescription(), IsSuccess = "False" });
            }

        }
    }
}
