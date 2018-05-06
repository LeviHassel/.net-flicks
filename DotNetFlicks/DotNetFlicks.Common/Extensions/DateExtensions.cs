using System;

namespace DotNetFlicks.Common.Extensions
{
    public static class DateExtensions
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
        /// Return an int containing either current age or age before death
        /// </summary>
        /// <param name="birthDate"></param>
        /// <param name="deathDate"></param>
        /// <returns></returns>
        public static int ToAge(this DateTime? birthDate, DateTime? deathDate)
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
