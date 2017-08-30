using CoreTemplate.Managers;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace CoreTemplate.Managers
{
    public static class EmailManagerExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailManager emailManager, string email, string link)
        {
            return emailManager.SendEmailAsync(email, "Confirm your email",
                $"Please confirm your account by clicking this link: <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>");
        }
    }
}
