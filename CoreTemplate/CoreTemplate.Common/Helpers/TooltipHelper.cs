using System.Collections.Generic;
using System.Text;

namespace CoreTemplate.Common.Helpers
{
    public static class TooltipHelper
    {
        /// <summary>
        /// Builds a multi-line string to be used in a tooltip from a list of each line
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        public static string GetTooltipFormattedList(List<string> lines)
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
    }
}
