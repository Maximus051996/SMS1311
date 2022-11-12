using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using StockManagementSystemBackend.Data;
using StockManagementSystemBackend.Interface;

namespace StockManagementSystemBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "SuperAdmin")]
    public class RolesController : ControllerBase
    {
        public IConfiguration _configuration;

        public readonly ApplicationDbContext _applicationDbContext;

        private IRole _IRole;

        public RolesController(IConfiguration configuration, ApplicationDbContext applicationDbContext, IRole IRole)
        {
            _configuration = configuration;
            _applicationDbContext = applicationDbContext;
            _IRole = IRole;
        }

        [HttpPost("InsertRole")]
        public async Task<IActionResult> InsertRole(string RoleName, int TenantId)
        {
            if (string.IsNullOrEmpty(RoleName))
                return BadRequest(new { Message = Enums.Failure.GetDescription(), IsSuccess = "False" });
            else
            {
                {
                    var result = await _IRole.InsertRole(RoleName, TenantId, new SqlConnection(_configuration.GetConnectionString("DefaultConnection")));

                    if (result > 0)
                    {
                        return Ok(new { Message = Enums.Insert.GetDescription(), IsSuccess = "True" });
                    }
                    else if (result == 0)
                    {
                        return Ok(new { Message = Enums.Dublicate.GetDescription(), IsSuccess = "False" });
                    }
                    else
                    {
                        return BadRequest(new { Message = Enums.Failure.GetDescription(), IsSuccess = "False" });
                    }
                }
            }
        }

        [HttpPut("UpdateRole")]
        public async Task<IActionResult> UpdateRole(long RoleId, string RoleName, int TenantId)
        {
            var result = await _IRole.UpdateRole(RoleId, RoleName, TenantId, new SqlConnection(_configuration.GetConnectionString("DefaultConnection")));
            if (result > 0)
            {
                return Ok(new { Message = Enums.Update.GetDescription(), IsSuccess = "True" });
            }
            else
            {
                return BadRequest(new { Message = Enums.Failure.GetDescription(), IsSuccess = "False" });
            }

        }

        [HttpPut("EnableDisableRole")]
        public async Task<IActionResult> EnableDisableRole(long RoleId, int TenantId, bool IsActive)
        {
            var result = await _IRole.EnableDisableRole(RoleId, TenantId, IsActive, new SqlConnection(_configuration.GetConnectionString("DefaultConnection")));
            if (result > 0)
            {
                return Ok(new { Message = IsActive == true ? Enums.Active.GetDescription(): Enums.Delete.GetDescription(), IsSuccess = "True" });
            }
            else
            {
                return BadRequest(new { Message = Enums.Failure.GetDescription(), IsSuccess = "False" });
            }

        }

        [HttpGet("GetRoleById")]
        public async Task<IActionResult> GetRoleById(long RoleId, int TenantId)
        {
            var result = await _IRole.GetRoleById(RoleId, TenantId, new SqlConnection(_configuration.GetConnectionString("DefaultConnection")));
            if (result != null)
            {
                return Ok(new { Message = Enums.Fetch.GetDescription(), IsSuccess = "True", Records = result });
            }
            else
            {
                return BadRequest(new { Message = Enums.Failure.GetDescription(), IsSuccess = "False" });
            }
        }

        [HttpGet("GetRoles")]
        public async Task<IActionResult> GetRoles(int TenantId)
        {
            var result = await _IRole.GetRoles(TenantId,new SqlConnection(_configuration.GetConnectionString("DefaultConnection")));
            if (result.Count() > 0)
            {
                return Ok(new { Message = Enums.Fetch.GetDescription(), IsSuccess = "True", Records = result });
            }
            else
            {
                return Ok(new { Message = Enums.Failure.GetDescription(), IsSuccess = "False" });
            }
        }



    }
}
