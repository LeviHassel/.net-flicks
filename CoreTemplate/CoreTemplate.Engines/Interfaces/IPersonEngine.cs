using CoreTemplate.Accessors.Models.DTO;
using CoreTemplate.ViewModels.Movie;
using System.Collections.Generic;

namespace CoreTemplate.Engines.Interfaces
{
    public interface IPersonEngine
    {
        string GetMoviesTooltip(List<CastMemberDTO> castRoles, List<CrewMemberDTO> crewRoles, bool useBullets = false);

        void UpdateCast(List<CastMemberViewModel> castMemberVms, int movieId);

        void UpdateCrew(List<CrewMemberViewModel> crewMemberVms, int movieId);
    }
}
