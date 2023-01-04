using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using StockManagementSystemBackend.Data;
using StockManagementSystemBackend.DTO;
using StockManagementSystemBackend.Interface;


namespace StockManagementSystemBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class UsersController : ControllerBase
    {
        public IConfiguration _configuration;

        public readonly ApplicationDbContext _applicationDbContext;

        private IUser _IUser;
        private IRole _IRole;
        private ITenant _ITenant;
        private readonly IEmail mailService;
        

        public UsersController(IConfiguration configuration, ApplicationDbContext applicationDbContext, IUser IUser, IRole IRole, ITenant ITenant, IEmail mailService)
        {
            _configuration = configuration;
            _applicationDbContext = applicationDbContext;
            _IUser = IUser;
            _IRole = IRole;
            _ITenant = ITenant;
            this.mailService = mailService;
        }

        [HttpPost("EnableDisableUser")]
        public async Task<IActionResult> EnableDisableUser(UserEnableDisableDTO userEnableDisableDTO)
        {
            var result = await _IUser.EnableDisableUser(userEnableDisableDTO.UserId, userEnableDisableDTO.IsActive, new SqlConnection(_configuration.GetConnectionString("DefaultConnection")));
            if (result > 0)
            {
                return Ok(new { Message = userEnableDisableDTO.IsActive == true ? Enums.Active.GetDescription() : Enums.Delete.GetDescription(), IsSuccess = "True" });
            }
            else
            {
                return BadRequest(new { Message = Enums.Failure.GetDescription(), IsSuccess = "False" });
            }
        }

        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById(long UserId)
        {
            var result = await _IUser.GetUserById(UserId, new SqlConnection(_configuration.GetConnectionString("DefaultConnection")));
            if (result != null)
            {
                return Ok(new { Message = Enums.Fetch.GetDescription(), IsSuccess = "True", Records = result });
            }
            else
            {
                return BadRequest(new { Message = Enums.Failure.GetDescription(), IsSuccess = "False" });
            }
        }

        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers(int TenantId)
        {
            var result = await _IUser.GetUsers(TenantId, new SqlConnection(_configuration.GetConnectionString("DefaultConnection")));
            if (result.Count() > 0)
            {
                return Ok(new { Message = Enums.Fetch.GetDescription(), IsSuccess = "True", Records = result });
            }
            else
            {
                return BadRequest(new { Message = Enums.Failure.GetDescription(), IsSuccess = "False" });
            }
        }

        [HttpPost("InsertUpdateUser")]
        public async Task<IActionResult> InsertUpdateUser(UserDTO userDTO)
        {
            if (userDTO == null)
                return BadRequest(new { Message = Enums.Failure.GetDescription(), IsSuccess = "False" });
            else
            {
                {
                   if (userDTO.RoleId == null)
                    {
                    repeat: var roledetails = await _IRole.GetRoles(userDTO.TenantId, new SqlConnection(_configuration.GetConnectionString("DefaultConnection")));


                        if (userDTO.RoleName == "Admin" && (roledetails.Count() == 0 || roledetails == null))
                        {
                            await _IRole.InsertRole("Admin", userDTO.TenantId, new SqlConnection(_configuration.GetConnectionString("DefaultConnection")));
                            goto repeat;
                        }
                        userDTO.RoleId = roledetails.Where(x => x.RoleName == userDTO.RoleName).Select(c => c.RoleId).ToList()[0];
                    }
                    var result = await _IUser.InsertUpdateUser(userDTO, new SqlConnection(_configuration.GetConnectionString("DefaultConnection")));
                    var tenantDetails = await _ITenant.GetTenantById(userDTO.TenantId, new SqlConnection(_configuration.GetConnectionString("DefaultConnection")));
                    if (result > 0)
                    {
                        if(userDTO.Operation == "Insert")
                        {
                            await mailService.SendEmailAsync(userDTO.Email, tenantDetails.TenantName, userDTO.UserName, userDTO.UserPassword, "Admin");
                        }                
                        return Ok(new { Message = userDTO.UserId > 0 ? Enums.Update.GetDescription() : Enums.Insert.GetDescription(), IsSuccess = "True" });
                    }
                    else if (result == 0 || result == -1)
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


        [HttpGet("UserDetailsByTenant")]
        public async Task<IActionResult> GetUserDetailsByTenant()
        {
            var result = await _IUser.GetUserDetailsByTenant(new SqlConnection(_configuration.GetConnectionString("DefaultConnection")));
            if (result.Count() > 0)
            {
                return Ok(new { Message = Enums.Fetch.GetDescription(), IsSuccess = "True", Records = result });
            }
            else if (result == null)
            {
                return BadRequest(new { Message = Enums.Failure.GetDescription(), IsSuccess = "False" });
            }
            else
            {
                return Ok(new { Message = Enums.Fetch.GetDescription(), IsSuccess = "True", Records = result });
            }

        }
    }
}
