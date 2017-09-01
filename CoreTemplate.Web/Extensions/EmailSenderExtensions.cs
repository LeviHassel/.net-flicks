using CoreTemplate.Managers.Interfaces;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace CoreTemplate.Web.Extensions
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
