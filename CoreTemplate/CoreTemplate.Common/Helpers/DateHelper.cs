using System;

namespace CoreTemplate.Common.Helpers
{
    public static class DateHelper
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
        /// Return a string containing either current age or age before death (including years alive)
        /// </summary>
        /// <param name="birthDate"></param>
        /// <param name="deathDate"></param>
        /// <returns></returns>
        public static string GetAge(DateTime birthDate, DateTime? deathDate)
        {
            var lastDateAlive = deathDate.HasValue ? deathDate.Value : DateTime.Today;

            var age = lastDateAlive.Year - birthDate.Year;

            if (birthDate > lastDateAlive.AddYears(-age))
            {
                age--;
            }

            var ageString = age.ToString();

            if (deathDate.HasValue)
            {
                ageString += " (" + birthDate.Year + "-" + deathDate.Value.Year + ")";
            }

            return ageString;
        }
    }
}
