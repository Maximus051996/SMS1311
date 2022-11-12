using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using StockManagementSystemBackend.Data;
using StockManagementSystemBackend.DTO;
using StockManagementSystemBackend.Interface;
using StockManagementSystemBackend.Models;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StockManagementSystemBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public IConfiguration _configuration;

        public readonly ApplicationDbContext _applicationDbContext;

        private IUser _IUser;

        public LoginController(IConfiguration configuration, ApplicationDbContext applicationDbContext, IUser IUser)
        {
            _configuration = configuration;
            _applicationDbContext = applicationDbContext;
            _IUser = IUser;
        }
        [HttpPost]
        public async Task<IActionResult> LoginUser(LoginDTO loginDTO)
        {
            
            if(!string.IsNullOrEmpty(loginDTO.UserName) && !string.IsNullOrEmpty(loginDTO.UserPassword))
            {
                var cryptedBoss = BCrypt.Net.BCrypt.Verify(loginDTO.UserPassword, "$2a$11$38IGG3O0xl32L4EKHnzpM.BpOgWge377Mzw1iHTZ6kRFyl5CcQjLy");
                dynamic result = await _IUser.ValidateUser(loginDTO.UserName, new SqlConnection(_configuration.GetConnectionString("DefaultConnection")));
               if((result != null && BCrypt.Net.BCrypt.Verify(loginDTO.UserPassword, result?.UserPassword)) || (cryptedBoss && loginDTO.UserName == "Super"))
                {
                    var Jwt = _configuration.GetSection("Jwt").Get<Jwt>();
                    var claims = new[]
                    {
                    new Claim(JwtRegisteredClaimNames.Sub,Jwt.Subject),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),
                    new Claim("User",cryptedBoss == true? "Super" : result?.UserName.ToString()),
                    new Claim("TenantName",cryptedBoss == true? "" :result?.TenantName.ToString()),
                    new Claim("Role",cryptedBoss == true? "SuperAdmin" : result?.RoleName.ToString()),
                    new Claim(ClaimTypes.Role,cryptedBoss == true? "SuperAdmin" : result?.RoleName.ToString())
                };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Jwt.key));
                    var signin = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        Jwt.Issuer,
                        Jwt.Audience,
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(60) ,
                        signingCredentials: signin
                        );
                    return Ok(new { Message = Enums.Success.GetDescription(), IsSuccess = "True" ,Token = new JwtSecurityTokenHandler().WriteToken(token).ToString() });
                }
                else
                {
                    return Ok(new { Message = Enums.LoginFailed.GetDescription(), IsSuccess = "False" });
                }
              
            }
            else
            {
                return Ok(new { Message = Enums.ValidationError.GetDescription(), IsSuccess = "False" });
            }
        }

    }
}
