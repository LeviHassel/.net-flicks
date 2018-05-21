using AutoMapper;
using DotNetFlicks.Accessors.Interfaces;
using DotNetFlicks.Accessors.Models.DTO;
using DotNetFlicks.Engines.Interfaces;
using DotNetFlicks.ViewModels.Movie;
using System.Collections.Generic;
using System.Linq;

namespace DotNetFlicks.Engines.Engines
{
    public class MovieRoleUpdateEngine : IMovieRoleUpdateEngine
    {
        private ICastMemberAccessor _castMemberAccessor;
        private ICrewMemberAccessor _crewMemberAccessor;

        public MovieRoleUpdateEngine(ICastMemberAccessor castMemberAccessor,
            ICrewMemberAccessor crewMemberAccessor)
        {
            _castMemberAccessor = castMemberAccessor;
            _crewMemberAccessor = crewMemberAccessor;
        }

        public void UpdateCast(List<CastMemberViewModel> castMemberVms, int movieId)
        {
            if (castMemberVms == null)
            {
                castMemberVms = new List<CastMemberViewModel>();
            }
            else
            {
                //Filter out deleted or incomplete cast members
                castMemberVms = castMemberVms.Where(x => !x.IsDeleted && x.PersonId != 0 && !string.IsNullOrWhiteSpace(x.Role)).ToList();
            }

            var castMemberDtos = Mapper.Map<List<CastMemberDTO>>(castMemberVms);

            foreach (var castMemberDto in castMemberDtos)
            {
                castMemberDto.MovieId = movieId;
                castMemberDto.Person = null;
            }

            _castMemberAccessor.SaveAll(movieId, castMemberDtos);
        }

        public void UpdateCrew(List<CrewMemberViewModel> crewMemberVms, int movieId)
        {
            if (crewMemberVms == null)
            {
                crewMemberVms = new List<CrewMemberViewModel>();
            }
            else
            {
                //Filter out deleted or incomplete crew members
                crewMemberVms = crewMemberVms.Where(x => !x.IsDeleted && x.PersonId != 0 && x.DepartmentId != 0 && !string.IsNullOrWhiteSpace(x.Position)).ToList();
            }

            var crewMemberDtos = Mapper.Map<List<CrewMemberDTO>>(crewMemberVms);

            foreach (var crewMemberDto in crewMemberDtos)
            {
                crewMemberDto.MovieId = movieId;
                crewMemberDto.Person = null;
                crewMemberDto.Department = null;
            }

            _crewMemberAccessor.SaveAll(movieId, crewMemberDtos);
        }
    }
}
