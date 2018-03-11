using CoreTemplate.ViewModels.Movie;
using System.Collections.Generic;

namespace CoreTemplate.Engines.Interfaces
{
    public interface IPersonEngine
    {
        void UpdateCast(List<CastMemberViewModel> castMemberVms, int movieId);

        void UpdateCrew(List<CrewMemberViewModel> crewMemberVms, int movieId);
    }
}
