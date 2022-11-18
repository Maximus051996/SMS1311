using StockManagementSystemBackend.DTO;

namespace StockManagementSystemBackend.Interface
{
    public interface IEmail
    {
        Task SendEmailAsync(string UserEmail, string UserName, string UserPassword, string Role);
    }
}

