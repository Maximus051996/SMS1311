using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using OfficeOpenXml;
using StockManagementSystemBackend.Data;
using StockManagementSystemBackend.DTO;
using StockManagementSystemBackend.Interface;

namespace StockManagementSystemBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UploadController : ControllerBase
    {
        public IConfiguration _configuration;

        public readonly ApplicationDbContext _applicationDbContext;

        private ICompany _ICompany;

        private ITenant _ITenant;
        public UploadController(IConfiguration configuration, ApplicationDbContext applicationDbContext, ICompany ICompany, ITenant ITenant)
        {
            _configuration = configuration;
            _applicationDbContext = applicationDbContext;
            _ICompany = ICompany;
            _ITenant = ITenant;
        }

        [HttpPost("Upload")]
        public async Task<IActionResult> UploadExcel(string fileType, string tenantName, string userName)
        {

            int count=0;
            int tenantId;
            tenantId = await _ITenant.ValidUserTenantId(tenantName, userName, new SqlConnection(_configuration.GetConnectionString("DefaultConnection")));
            if (tenantId == 0)
            {
                return Ok(new { Message = Enums.UserInvalid.GetDescription(), IsSuccess = "False" });
            }
            else
            {
                IFormFile formFile = Request.Form.Files[0];
                ExcelPackage.LicenseContext = LicenseContext.Commercial;
                if (formFile == null || formFile.Length <= 0)
                {
                    return Ok(new { Message = Enums.FileEmpty.GetDescription(), IsSuccess = "False" });
                }

                if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                {
                    return Ok(new { Message = Enums.FileFormat.GetDescription(), IsSuccess = "False" });
                }


                switch (fileType)
                {
                    case "Company":
                        {                        
                          var productList = new List<CompanyDTO>();
                          using (var stream = new MemoryStream())
                          {
                              await formFile.CopyToAsync(stream);

                              using (var package = new ExcelPackage(stream))
                              {
                                  ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                                  var rowCount = worksheet.Dimension.Rows;
                                  for (var row = 2; row <= rowCount; row++)
                                  {
                                      productList.Add(new CompanyDTO
                                      {
                                          CompanyName = worksheet.Cells[row, 1].Value.ToString()?.Trim(),
                                          Priroty = worksheet.Cells[row, 2].Value.ToString()?.Trim()
                                      });

                                  }
                              }
                          }
                          count= await _ICompany.InsertBulkCompany(productList, tenantId, new SqlConnection(_configuration.GetConnectionString("DefaultConnection")));
                        }
                        break;
                    case "Product":
                        Console.WriteLine("Product");
                        break;
                }
                return Ok(new { Message = count == -1 ? Enums.Dublicate.GetDescription() : Enums.Insert.GetDescription(), IsSuccess = "True" });

            }
        }
    }
}
