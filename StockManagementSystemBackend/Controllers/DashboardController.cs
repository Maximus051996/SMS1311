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
    [Authorize(Roles = "SuperAdmin")]
    public class DashboardController : ControllerBase
    {
        public IConfiguration _configuration;

        public readonly ApplicationDbContext _applicationDbContext;

        private ITenant _ITenant;
        private IRole _Role;

        public DashboardController(IConfiguration configuration, ApplicationDbContext applicationDbContext, ITenant ITenant, IRole Role)
        {
            _configuration = configuration;
            _applicationDbContext = applicationDbContext;
            _ITenant = ITenant; 
            _Role = Role;
        }

        [HttpGet("GetAdminDashboardDetails")]
        public async Task<IActionResult> GetTenants()
        {
            List<TotalWorkerByTenantDTO> totalUserByTenantlist = new List<TotalWorkerByTenantDTO>();
            var tenantresult = await _ITenant.GetTenants(new SqlConnection(_configuration.GetConnectionString("DefaultConnection")));
            var totalUserByTenantresult = await _ITenant.TotalUserByTenant(new SqlConnection(_configuration.GetConnectionString("DefaultConnection")));

            foreach (var item in totalUserByTenantresult.ToList())
            {
                totalUserByTenantlist.Add(new TotalWorkerByTenantDTO() { TenantName = item.TenantName, TenantCount = item.TenantCount});
            }
            List<string> roleResult = (await _Role.GetAllActiveRoles(new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))).ToList();
            var roleCountByTenantresult = await _ITenant.GetRoleCountByTenant(roleResult, new SqlConnection(_configuration.GetConnectionString("DefaultConnection")));

            if (tenantresult.Count()>0 || totalUserByTenantlist.Count()>0 || roleCountByTenantresult.Count()>0)
            {
                return Ok(new { Message = Enums.Fetch.GetDescription(), IsSuccess = "True", 
                    TenantResult= tenantresult, UserByTenantResult= totalUserByTenantlist,
                    RoleCountByTenantResult= roleCountByTenantresult , AllActiveRoles= roleResult
                });
            }
            else if(tenantresult == null || totalUserByTenantlist ==null || roleCountByTenantresult ==null)
            {
                return BadRequest(new { Message = Enums.Failure.GetDescription(), IsSuccess = "False" });
            }
            else
            {
                return Ok(new
                {
                    Message = Enums.Fetch.GetDescription(),
                    IsSuccess = "True",
                    TenantResult = tenantresult,
                    UserByTenantResult = totalUserByTenantlist,
                    RoleCountByTenantResult = roleCountByTenantresult,
                    AllActiveRoles = roleResult
                });
            }
        }
    }
}
