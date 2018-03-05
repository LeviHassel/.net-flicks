using System;

namespace CoreTemplate.Common.Helpers
{
    public static class AgeHelper
    {
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
