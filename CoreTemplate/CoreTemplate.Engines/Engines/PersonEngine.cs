using AutoMapper;
using CoreTemplate.Accessors.Interfaces;
using CoreTemplate.Accessors.Models.DTO;
using CoreTemplate.Common.Helpers;
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
