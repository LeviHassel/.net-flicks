using AutoMapper;
using CoreTemplate.Accessors.Interfaces;
using CoreTemplate.Accessors.Models.DTO;
using CoreTemplate.Engines.Interfaces;
using CoreTemplate.ViewModels.Movie;
using System.Collections.Generic;
using System.Linq;

namespace CoreTemplate.Engines.Engines
{
    public class PersonEngine : IPersonEngine
    {
        private ICastMemberAccessor _castMemberAccessor;
        private ICrewMemberAccessor _crewMemberAccessor;

        public PersonEngine(ICastMemberAccessor castMemberAccessor,
            ICrewMemberAccessor crewMemberAccessor)
        {
            _castMemberAccessor = castMemberAccessor;
            _crewMemberAccessor = crewMemberAccessor;
        }

        public void UpdateCast(List<CastMemberViewModel> castMemberVms, int movieId)
        {
            //Filter out deleted or incomplete cast members
            castMemberVms = castMemberVms.Where(x => !x.IsDeleted && x.PersonId != 0 && !string.IsNullOrWhiteSpace(x.Role)).ToList();

            var castMemberDtos = Mapper.Map<List<CastMemberDTO>>(castMemberVms);

            foreach (var castMemberDto in castMemberDtos)
            {
                castMemberDto.MovieId = movieId;
            }

            _castMemberAccessor.SaveAll(movieId, castMemberDtos);
        }

        public void UpdateCrew(List<CrewMemberViewModel> crewMemberVms, int movieId)
        {
            //Filter out deleted or incomplete crew members
            crewMemberVms = crewMemberVms.Where(x => !x.IsDeleted && x.PersonId != 0 && x.DepartmentId != 0 && !string.IsNullOrWhiteSpace(x.Position)).ToList();

            var crewMemberDtos = Mapper.Map<List<CrewMemberDTO>>(crewMemberVms);

            foreach (var crewMemberDto in crewMemberDtos)
            {
                crewMemberDto.MovieId = movieId;
            }

            _crewMemberAccessor.SaveAll(movieId, crewMemberDtos);
        }
    }
}
