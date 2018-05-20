using System.Web;

namespace DotNetFlicks.Common.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Get a specified number of characters from a string, starting at 0
        /// </summary>
        /// <param name="message"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string ToSubString(this string message, int count)
        {
            return message != null && message.Length > count ? message.Substring(0, count) + "..." : message;
        }

        /// <summary>
        /// HTML encodes a string, ensuring that text is displayed correctly and not interpreted as HTML by the browser
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string HtmlEncode(this string message)
        {
            return HttpUtility.HtmlEncode(message).Replace("\n", "<br/>");
        }
    }
}
