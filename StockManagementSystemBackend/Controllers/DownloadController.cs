using Fingers10.ExcelExport.ActionResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using StockManagementSystemBackend.Data;
using StockManagementSystemBackend.DTO;
using StockManagementSystemBackend.Interface;

namespace StockManagementSystemBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class DownloadController : ControllerBase
    {
        public IConfiguration _configuration;
        public readonly ApplicationDbContext _applicationDbContext;
        private ICompany _ICompany;
        private ITenant _ITenant;

        public DownloadController(IConfiguration configuration, ApplicationDbContext applicationDbContext, ICompany ICompany, ITenant ITenant)
        {
            _configuration = configuration;
            _applicationDbContext = applicationDbContext;
            _ICompany = ICompany;
            _ITenant = ITenant;
        }


        [HttpGet("DownloadExcel")]
        public async Task<IActionResult> DownloadExcel(string fileType, string tenantName, string userName)
        {
            int tenantId;
            List<CompanyDTO>? companyresultSet =null;
            var datetimeinfo = DateTime.Now.ToString("dd-MM-yyyy_HH:mm:ss:tt");
            tenantId = await _ITenant.ValidUserTenantId(tenantName, userName, new SqlConnection(_configuration.GetConnectionString("DefaultConnection")));
            if (tenantId == 0)
            {
                return Ok(new { Message = Enums.UserInvalid.GetDescription(), IsSuccess = "False" });
            }
            else
            {
                switch (fileType)
                {
                    case "Company":
                        {
                           
                            companyresultSet = (await _ICompany.GetAllCompaniesByTenant(tenantId, new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))).ToList();
                            if(companyresultSet.Count == 0)
                            {
                                return Ok(new { Message = Enums.FileEmpty.GetDescription(), IsSuccess = "False" });
                            }                          
                        }
                        break;
                }              
                return new ExcelResult<CompanyDTO>(companyresultSet, "Company", $"SMS_{tenantName}_Company_{datetimeinfo}");
            }
        }
    }
}
