using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using StockManagementSystemBackend.Data;
using StockManagementSystemBackend.Interface;

namespace StockManagementSystemBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
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

        [HttpGet("GetAllCompaniesByTenant")]
        public async Task<IActionResult> GetAllCompaniesByTenant(string TenantName, string UserName)
        {
            int TenantId = await _ITenant.ValidUserTenantId(TenantName, UserName, new SqlConnection(_configuration.GetConnectionString("DefaultConnection")));
            var result = await _ICompany.GetAllCompaniesByTenant(TenantId, new SqlConnection(_configuration.GetConnectionString("DefaultConnection")));
            if (result.Count() > 0)
            {
                return Ok(new { Message = Enums.Fetch.GetDescription(), IsSuccess = "True", Records = result });
            }
            else
            {
                return BadRequest(new { Message = Enums.Failure.GetDescription(), IsSuccess = "False" });
            }
        }

        [HttpPost("GetCompanyByTenant")]
        public async Task<IActionResult> GetCompanyByTenant(string TenantName, string UserName,int CompanyId)
        {
            int TenantId = await _ITenant.ValidUserTenantId(TenantName, UserName, new SqlConnection(_configuration.GetConnectionString("DefaultConnection")));

            var result = await _ICompany.GetCompanyByTenant(TenantId, CompanyId, new SqlConnection(_configuration.GetConnectionString("DefaultConnection")));

            if (result != null)
            {
                return Ok(new { Message = Enums.Fetch.GetDescription(), IsSuccess = "True" , Records = result });
            }
            else
            {
                return BadRequest(new { Message = Enums.Failure.GetDescription(), IsSuccess = "False" });
            }
        }

        [HttpDelete("DeleteCompanyByTenant")]
        public async Task<IActionResult> UpdateCompanyByTenant(string TenantName, string UserName, int CompanyId)
        {
            int TenantId = await _ITenant.ValidUserTenantId(TenantName, UserName, new SqlConnection(_configuration.GetConnectionString("DefaultConnection")));

            var result = await _ICompany.DeleteCompanyByTenant(TenantId, CompanyId, new SqlConnection(_configuration.GetConnectionString("DefaultConnection")));

            if (result > 0)
            {
                return Ok(new { Message = Enums.Delete.GetDescription(), IsSuccess = "True", Records = result });
            }
            else
            {
                return BadRequest(new { Message = Enums.Failure.GetDescription(), IsSuccess = "False" });
            }
        }






    }
}
