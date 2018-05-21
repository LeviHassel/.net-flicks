using DotNetFlicks.ViewModels.Movie;
using System.Collections.Generic;

namespace DotNetFlicks.Engines.Interfaces
{
    public interface IMovieRoleUpdateEngine
    {
        void UpdateCast(List<CastMemberViewModel> castMemberVms, int movieId);

        void UpdateCrew(List<CrewMemberViewModel> crewMemberVms, int movieId);
    }
}
