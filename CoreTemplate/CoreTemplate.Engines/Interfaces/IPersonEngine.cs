using CoreTemplate.Accessors.Models.DTO;
using System.Collections.Generic;

namespace CoreTemplate.Engines.Interfaces
{
    public interface IPersonEngine
    {
        string GetMoviesTooltip(List<CastMemberDTO> castRoles, List<CrewMemberDTO> crewRoles, bool useBullets = false);
    }
}
