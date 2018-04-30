using System.Web;

namespace DotNetFlicks.Common.Helpers
{
    public static class StringHelper
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
        /// Preserve line breaks when displaying raw HTML
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string PreserveNewLines(this string message)
        {
            return HttpUtility.HtmlEncode(message).Replace("\n", "<br/>");
        }
    }
}
