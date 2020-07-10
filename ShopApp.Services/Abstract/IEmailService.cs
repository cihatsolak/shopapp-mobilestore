using System.Threading.Tasks;

namespace ShopApp.Services.Abstract
{
    public partial interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string ApprovedUrl);

        string PrepareEmailHtmlTemplate(string ApprovedUrl, string subject);
    }
}
