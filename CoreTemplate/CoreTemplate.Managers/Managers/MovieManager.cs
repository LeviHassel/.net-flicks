using AutoMapper;
using CoreTemplate.Accessors.Interfaces;
using CoreTemplate.Accessors.Models.DTO;
using CoreTemplate.Common.Helpers;
using CoreTemplate.Managers.Interfaces;
using CoreTemplate.ViewModels.Movie;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace CoreTemplate.Managers.Managers
{
    public class MovieManager : IMovieManager
    {
        private ICastMemberAccessor _castMemberAccessor;
        private ICrewMemberAccessor _crewMemberAccessor;
        private IDepartmentAccessor _departmentAccessor;
        private IGenreAccessor _genreAccessor;
        private IMovieAccessor _movieAccessor;
        private IMovieGenreAccessor _movieGenreAccessor;
        private IPersonAccessor _personAccessor;

        public MovieManager(ICastMemberAccessor castMemberAccessor,
            ICrewMemberAccessor crewMemberAccessor,
            IDepartmentAccessor departmentAccessor,
            IGenreAccessor genreAccessor,
            IMovieAccessor movieAccessor,
            IMovieGenreAccessor movieGenreAccessor,
            IPersonAccessor personAccessor)
        {
            _castMemberAccessor = castMemberAccessor;
            _crewMemberAccessor = crewMemberAccessor;
            _departmentAccessor = departmentAccessor;
            _genreAccessor = genreAccessor;
            _movieAccessor = movieAccessor;
            _movieGenreAccessor = movieGenreAccessor;
            _personAccessor = personAccessor;
        }

        public MovieViewModel Get(int? id)
        {
            var movieDto = id.HasValue ? _movieAccessor.Get(id.Value) : new MovieDTO();
            var departmentDtos = _departmentAccessor.GetAll().OrderBy(x => x.Name);
            var genreDtos = _genreAccessor.GetAll().OrderBy(x => x.Name);
            var personDtos = _personAccessor.GetAll().OrderBy(x => x.FirstName);

            var vm = Mapper.Map<MovieViewModel>(movieDto);

            vm.GenresSelectList = new MultiSelectList(genreDtos, "Id", "Name", movieDto.Genres != null ? movieDto.Genres.Select(x => x.GenreId).ToList() : null);

            if (movieDto.Cast != null && movieDto.Cast.Any())
            {
                vm.Cast = Mapper.Map<List<CastMemberViewModel>>(movieDto.Cast)
                    .OrderBy(x => personDtos.Single(y => y.Id == x.PersonId).FirstName)
                    .ToList();
            }

            foreach (var castMemberVm in vm.Cast)
            {
                castMemberVm.People = new SelectList(personDtos, "Id", "FullName", castMemberVm.PersonId);
            }

            if (movieDto.Crew != null && movieDto.Crew.Any())
            {
                vm.Crew = Mapper.Map<List<CrewMemberViewModel>>(movieDto.Crew)
                    .OrderBy(x => personDtos.Single(y => y.Id == x.PersonId).FirstName)
                    .ToList();
            }

            foreach (var crewMemberVm in vm.Crew)
            {
                crewMemberVm.People = new SelectList(personDtos, "Id", "FullName", crewMemberVm.PersonId);
                crewMemberVm.Departments = new SelectList(departmentDtos, "Id", "Name", crewMemberVm.DepartmentId);
            }

            return vm;
        }

        public MoviesViewModel GetAll()
        {
            var dtos = _movieAccessor.GetAll();
            var vms = Mapper.Map<List<MovieViewModel>>(dtos);

            foreach (var vm in vms)
            {
                var dto = dtos.Single(x => x.Id == vm.Id);

                if (dto.Genres != null && dto.Genres.Any())
                {
                    vm.GenresCount = dto.Genres.Count();
                    vm.GenresTooltip = ListHelper.GetTooltipList(dto.Genres.Select(x => x.Genre.Name).OrderBy(y => y).ToList());
                }
            }

            return new MoviesViewModel { Movies = vms };
        }

        public CastMemberViewModel GetNewCastMember(int index)
        {
            var personDtos = _personAccessor.GetAll().OrderBy(x => x.FirstName);
 
            var vm = new CastMemberViewModel
            {
                Index = index,
                People = new SelectList(personDtos, "Id", "FullName")
            };

            return vm;
        }

        public CrewMemberViewModel GetNewCrewMember(int index)
        {
            var personDtos = _personAccessor.GetAll().OrderBy(x => x.FirstName);
            var departmentDtos = _departmentAccessor.GetAll().OrderBy(x => x.Name);

            var vm = new CrewMemberViewModel
            {
                Index = index,
                People = new SelectList(personDtos, "Id", "FullName"),
                Departments = new SelectList(departmentDtos, "Id", "Name")
            };

            return vm;
        }

        public MovieViewModel Save(MovieViewModel vm)
        {
            var dto = Mapper.Map<MovieDTO>(vm);
            dto.Cast = null;
            dto.Crew = null;

            dto = _movieAccessor.Save(dto);

            _movieGenreAccessor.SaveAll(dto.Id, vm.GenreIds);

            var castMemberDtos = new List<CastMemberDTO>();
            
            if (vm.Cast != null && vm.Cast.Any())
            {
                castMemberDtos.AddRange(vm.Cast
                    .Where(x => !x.IsDeleted && x.PersonId != 0 && !string.IsNullOrWhiteSpace(x.Role))
                    .Select(x => new CastMemberDTO
                    {
                        Id = x.Id,
                        MovieId = dto.Id,
                        PersonId = x.PersonId,
                        Role = x.Role,
                        ScreenTime = x.ScreenTime
                    }));
            }

            _castMemberAccessor.SaveAll(dto.Id, castMemberDtos);

            var crewMemberDtos = new List<CrewMemberDTO>();

            if (vm.Crew != null && vm.Crew.Any())
            {
                crewMemberDtos.AddRange(vm.Crew
                    .Where(x => !x.IsDeleted && x.PersonId != 0 && x.DepartmentId != 0 && !string.IsNullOrWhiteSpace(x.Position))
                    .Select(x => new CrewMemberDTO
                    {
                        Id = x.Id,
                        MovieId = dto.Id,
                        PersonId = x.PersonId,
                        DepartmentId = x.DepartmentId,
                        Position = x.Position
                    }));
            }

            _crewMemberAccessor.SaveAll(dto.Id, crewMemberDtos);
            
            vm = Mapper.Map<MovieViewModel>(dto);

            return vm;
        }

        public MovieViewModel Delete(int id)
        {
            _castMemberAccessor.DeleteAllByMovie(id);
            _crewMemberAccessor.DeleteAllByMovie(id);
            _movieGenreAccessor.DeleteAllByMovie(id);

            var dto = _movieAccessor.Delete(id);
            var vm = Mapper.Map<MovieViewModel>(dto);

            return vm;
        }
    }
}
