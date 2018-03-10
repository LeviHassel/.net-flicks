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
        private IGenreAccessor _genreAccessor;
        private IDepartmentAccessor _departmentAccessor;
        private IMovieAccessor _movieAccessor;
        private IMovieGenreAccessor _movieGenreAccessor;
        private ICrewMemberAccessor _crewMemberAccessor;
        private IPersonAccessor _personAccessor;

        public MovieManager(IGenreAccessor genreAccessor,
            IDepartmentAccessor departmentAccessor,
            IMovieAccessor movieAccessor,
            IMovieGenreAccessor movieGenreAccessor,
            ICrewMemberAccessor crewMemberAccessor,
            IPersonAccessor personAccessor)
        {
            _genreAccessor = genreAccessor;
            _departmentAccessor = departmentAccessor;
            _movieAccessor = movieAccessor;
            _movieGenreAccessor = movieGenreAccessor;
            _crewMemberAccessor = crewMemberAccessor;
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

                if (dto.Crew != null && dto.Crew.Any())
                {
                    vm.CrewCount = dto.Crew.Count();
                    vm.CrewTooltip = ListHelper.GetTooltipList(dto.Crew.Select(x => string.Format("{0} - {1}", x.Person.FullName, x.Department.Name)).OrderBy(y => y).ToList());
                }
            }

            return new MoviesViewModel { Movies = vms };
        }

        public CrewMemberViewModel GetNewPerson(int index)
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
            dto.Crew = null;

            dto = _movieAccessor.Save(dto);

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

            _movieGenreAccessor.SaveAll(dto.Id, vm.GenreIds);
            _crewMemberAccessor.SaveAll(dto.Id, crewMemberDtos);

            vm = Mapper.Map<MovieViewModel>(dto);

            return vm;
        }

        public MovieViewModel Delete(int id)
        {
            _movieGenreAccessor.DeleteAllByMovie(id);
            _crewMemberAccessor.DeleteAllByMovie(id);

            var dto = _movieAccessor.Delete(id);
            var vm = Mapper.Map<MovieViewModel>(dto);

            return vm;
        }
    }
}
