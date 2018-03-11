using CoreTemplate.Accessors.Models.DTO;
using CoreTemplate.Common.Helpers;
using CoreTemplate.Engines.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CoreTemplate.Engines.Engines
{
    public class PersonEngine : IPersonEngine
    {

        public PersonEngine()
        {
        }

        public string GetMoviesTooltip(List<CastMemberDTO> castRoles, List<CrewMemberDTO> crewRoles, bool useBullets = false)
        {
            var tooltipLines = new List<string>();

            if (castRoles.Any())
            {
                tooltipLines.AddRange(castRoles.Select(x => string.Format("{0} ({1})", x.Movie.Name, x.Role)));
            }

            if (crewRoles.Any())
            {
                tooltipLines.AddRange(crewRoles.Select(x => string.Format("{0} ({1})", x.Movie.Name, x.Position)));
            }

            tooltipLines.OrderBy(x => x);

            if (useBullets)
            {
                return ListHelper.GetBulletedList(tooltipLines);
            }
            else
            {
                return ListHelper.GetTooltipList(tooltipLines);
            }
        }
    }
}
