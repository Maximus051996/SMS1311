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
    public class TenantsController : ControllerBase
    {
        public IConfiguration _configuration;

        public readonly ApplicationDbContext _applicationDbContext;

        private ITenant _ITenant;
        private IRole _Role;

        public TenantsController(IConfiguration configuration, ApplicationDbContext applicationDbContext, ITenant ITenant, IRole Role)
        {
            _configuration = configuration;
            _applicationDbContext = applicationDbContext;
            _ITenant = ITenant;
            _Role = Role;
        }
      

        //[HttpGet("TotalUserByTenant")]
        //public async Task<IActionResult> TotalUserByTenantAsync()
        //{
        //    List<TotalWorkerByTenantDTO> listobj = new List<TotalWorkerByTenantDTO>();

        //    var result = await _ITenant.TotalUserByTenant(new SqlConnection(_configuration.GetConnectionString("DefaultConnection")));

        //    foreach (var item in result.ToList())
        //    {
        //        listobj.Add(new TotalWorkerByTenantDTO() { TenantName = item.TenantName, TenantCount = item.TenantCount, TenantColor = Common.getRandColor() });
        //    }
        //    if (listobj.Count > 0)
        //    {
        //        return Ok(new { Message = Enums.Fetch.GetDescription(), IsSuccess = "True", Records = listobj });
        //    }
        //    else
        //    {
        //        return Ok(new { Message = Enums.Failure.GetDescription(), IsSuccess = "False" });
        //    }
        //}

        //[HttpGet("RoleCountByTenant")]
        //public async Task<IActionResult> GetRoleCountByTenant()
        //{
        //    List<string> roleResult=(await _Role.GetAllActiveRoles(new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))).ToList();          
        //    var resultSet =await _ITenant.GetRoleCountByTenant(roleResult, new SqlConnection(_configuration.GetConnectionString("DefaultConnection")));
        //    return Ok(new { Message = Enums.Fetch.GetDescription(), IsSuccess = "True", Records = resultSet });
        //}


        [HttpGet("GetTenants")]
        public async Task<IActionResult> GetTenants()
        {
            var result = await _ITenant.GetTenants(new SqlConnection(_configuration.GetConnectionString("DefaultConnection")));
            if (result.Count() > 0)
            {
                return Ok(new { Message = Enums.Fetch.GetDescription(), IsSuccess = "True", Records = result });
            }
            else if(result == null)
            {
                return BadRequest(new { Message = Enums.Failure.GetDescription(), IsSuccess = "False" });
            }
            else
            {
                return Ok(new { Message = Enums.Fetch.GetDescription(), IsSuccess = "True", Records = result });
            }
        }

        [HttpGet("GetTenantById")]
        public async Task<IActionResult> GetTenantById(long TenantId)
        {
            var result = await _ITenant.GetTenantById(TenantId, new SqlConnection(_configuration.GetConnectionString("DefaultConnection")));
            if (result != null)
            {
                return Ok(new { Message = Enums.Fetch.GetDescription(), IsSuccess = "True", Records = result });
            }
            else
            {
                return BadRequest(new { Message = Enums.Failure.GetDescription(), IsSuccess = "False" });
            }
        }

        [HttpPost("InsertTenant")]
        public async Task<IActionResult> InsertTenant(string TenantName)
        {
            if (string.IsNullOrEmpty(TenantName))
                return Ok(new { Message = Enums.Failure.GetDescription(), IsSuccess = "False" });
            else
            {
                {
                    var result = await _ITenant.InsertTenant(TenantName, new SqlConnection(_configuration.GetConnectionString("DefaultConnection")));

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
                        return Ok(new { Message = Enums.Unknown.GetDescription(), IsSuccess = "False" });
                    }
                }
            }
        }

        [HttpPost("UpdateTenant")]
        public async Task<IActionResult> UpdateTenant(TenantUpdateDTO tenantUpdateDTO)
        {
            var result = await _ITenant.UpdateTenant(tenantUpdateDTO.TenantId, tenantUpdateDTO.TenantName, new SqlConnection(_configuration.GetConnectionString("DefaultConnection")));
            if (result > 0)
            {
                return Ok(new { Message = Enums.Update.GetDescription(), IsSuccess = "True" });
            }
            else
            {
                return BadRequest(new { Message = Enums.Failure.GetDescription(), IsSuccess = "False" });
            }

        }

        [HttpPost("EnableDisableTenant")]
        public async Task<IActionResult> EnableDisableTenant(TenantEnableDisableDTO tenantEnableDisable)
        {
            var result = await _ITenant.EnableDisableTenants(tenantEnableDisable.TenantId, tenantEnableDisable.IsActive, new SqlConnection(_configuration.GetConnectionString("DefaultConnection")));
            if (result > 0)
            {
                return Ok(new { Message = tenantEnableDisable.IsActive == true ? Enums.Active.GetDescription() : Enums.Delete.GetDescription(), IsSuccess = "True" });
            }
            else
            {
                return BadRequest(new { Message = Enums.Failure.GetDescription(), IsSuccess = "False" });
            }

        }  

    }
}
