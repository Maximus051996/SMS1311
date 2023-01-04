using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockManagementSystemBackend.DTO;
using StockManagementSystemBackend.Interface;

namespace StockManagementSystemBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailsController : ControllerBase
    {
        private readonly IEmail mailService;
        public EmailsController(IEmail mailService)
        {
            this.mailService = mailService;
        }
        [HttpPost("send")]
        public async Task<IActionResult> SendMail(string EmailAddress)
        {
            try
            {
                //await mailService.SendEmailAsync(EmailAddress,"Sayan","CC","Admin");
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }

        }
    }
}
