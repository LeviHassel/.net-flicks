using System;

namespace DotNetFlicks.Common.Utilities
{
    public static class DateUtility
    {
        /// <summary>
        /// Returns a long date string without the day of the week (example: January 1st, 2000)
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToCustomDateString(this DateTime date)
        {
            return date.ToLongDateString().Replace(date.DayOfWeek.ToString() + ", ", "");
        }

        /// <summary>
        /// Converts a TimeSpan into a string formatted like "3h 25m"
        /// Adapted from https://stackoverflow.com/questions/9292014/display-timespan-nicely
        /// </summary>
        /// <param name="timespan"></param>
        /// <returns></returns>
        public static string ToFormattedString(this TimeSpan timespan)
        {
            if (timespan.TotalHours < 1.0)
            {
                return String.Format("{0}m", timespan.Minutes);
            }
            else if (timespan.TotalDays < 1.0)
            {
                return String.Format("{0}h {1:D2}m", timespan.Hours, timespan.Minutes);
            }
            else //More than 1 day
            {
                return String.Format("{0}d {1:D2}h {2:D2}m", (int)timespan.Days, timespan.Hours, timespan.Minutes);
            }
        }

        /// <summary>
        /// Returns an int containing either current age or age before death
        /// </summary>
        /// <param name="birthDate"></param>
        /// <param name="deathDate"></param>
        /// <returns></returns>
        public static int GetAge(DateTime? birthDate, DateTime? deathDate)
        {
            var age = 0;

            if (birthDate.HasValue)
            {
                var lastDateAlive = deathDate.HasValue ? deathDate.Value : DateTime.Today;

                age = lastDateAlive.Year - birthDate.Value.Year;

                if (birthDate.Value > lastDateAlive.AddYears(-age))
                {
                    age--;
                }
            }

            return age;
        }
    }
}
