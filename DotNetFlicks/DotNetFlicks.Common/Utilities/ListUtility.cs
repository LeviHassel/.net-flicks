using System.Collections.Generic;
using System.Text;

namespace DotNetFlicks.Common.Utilities
{
    public static class ListUtility
    {
        /// <summary>
        /// Builds an HTML-formatted tooltip list from a list of lines
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        public static string ToTooltipList(this List<string> lines)
        {
            var sb = new StringBuilder();

            foreach (var line in lines)
            {
                sb.AppendFormat("{0}<br />", line);
            }

            if (sb.Length > 0)
            {
                //Trim off the last line break
                return sb.ToString().Substring(0, sb.Length - 6);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Builds an HTML-formatted bulleted list from a list of lines
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        public static string ToBulletedList(this List<string> lines)
        {
            var sb = new StringBuilder();

            sb.Append("<ul>");

            foreach (var line in lines)
            {
                sb.AppendFormat("<li>{0}</li>", line);
            }

            return sb.Append("</ul>").ToString();
        }
    }
}
