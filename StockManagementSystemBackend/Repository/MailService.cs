using Microsoft.Extensions.Options;
using StockManagementSystemBackend.DTO;
using StockManagementSystemBackend.Interface;
using System.Net.Mail;

namespace StockManagementSystemBackend.Repository
{
    public class MailService : IEmail
    {
        private readonly MailSettingsDTO _mailSettings;
        public MailService(IOptions<MailSettingsDTO> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }
        public async Task SendEmailAsync(string UserEmail,string UserName,string UserPassword,string Role)
        {
          
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(_mailSettings.Mail);
                mail.To.Add(new MailAddress(UserEmail));
                mail.Subject = _mailSettings.Subject;
                mail.Body = $"Hello {UserEmail},<br><br><br>Account created Successfully for {Role} Role." +
                    $"<br>UserName & Password is -" +
                    $" <b>{UserName}<b> & <b>{UserPassword}<b><br><br><br><br>" +
                    $"Please Login to SMS ,If you face any issue please contact with Super using mail id - " +
                    $"<b>{_mailSettings.Mail}<b><br><br><br>" +
                    $"Thanks & Regards,<br>Super<br>ADM | SMS";
                mail.IsBodyHtml = true;
                using (SmtpClient smtp = new SmtpClient(_mailSettings.Host, _mailSettings.Port))
                {
                    smtp.Credentials = new System.Net.NetworkCredential(_mailSettings.Mail, _mailSettings.DesktopToPasskey);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }

            }


        }
    }
}
