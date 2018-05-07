using AutoMapper;
using DotNetFlicks.Accessors.Interfaces;
using DotNetFlicks.Accessors.Models.DTO;
using DotNetFlicks.Engines.Interfaces;
using DotNetFlicks.Managers.Interfaces;
using DotNetFlicks.ViewModels.Movie;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace DotNetFlicks.Managers.Managers
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
        private IPersonEngine _personEngine;

        public MovieManager(ICastMemberAccessor castMemberAccessor,
            ICrewMemberAccessor crewMemberAccessor,
            IDepartmentAccessor departmentAccessor,
            IGenreAccessor genreAccessor,
            IMovieAccessor movieAccessor,
            IMovieGenreAccessor movieGenreAccessor,
            IPersonAccessor personAccessor,
            IPersonEngine personEngine)
        {
            _castMemberAccessor = castMemberAccessor;
            _crewMemberAccessor = crewMemberAccessor;
            _departmentAccessor = departmentAccessor;
            _genreAccessor = genreAccessor;
            _movieAccessor = movieAccessor;
            _movieGenreAccessor = movieGenreAccessor;
            _personAccessor = personAccessor;
            _personEngine = personEngine;
        }

        public MovieViewModel Get(int id)
        {
            var dto = _movieAccessor.Get(id);
            var vm = Mapper.Map<MovieViewModel>(dto);

            vm.Cast = vm.Cast.OrderBy(x => x.Order).ToList();
            vm.Crew = vm.Crew.OrderBy(x => x.Category).ThenBy(x => x.PersonName).ToList();
            vm.Genres = vm.Genres.OrderBy(x => x.Name).ToList();

            return vm;
        }

        public EditMovieViewModel GetForEditing(int? id)
        {
            var movieDto = id.HasValue ? _movieAccessor.Get(id.Value) : new MovieDTO();
            var genreDtos = _genreAccessor.GetAll().OrderBy(x => x.Name);

            var vm = Mapper.Map<EditMovieViewModel>(movieDto);

            vm.Cast = vm.Cast.OrderBy(x => x.PersonName).ToList();
            vm.Crew = vm.Crew.OrderBy(x => x.PersonName).ToList();
            vm.GenresSelectList = new MultiSelectList(genreDtos, "Id", "Name", movieDto.Genres != null ? movieDto.Genres.Select(x => x.GenreId).ToList() : null);

            return vm;
        }

        public MoviesViewModel GetAll()
        {
            var dtos = _movieAccessor.GetAll();
            var vms = Mapper.Map<List<MovieViewModel>>(dtos);

            foreach (var vm in vms)
            {
                vm.Genres = vm.Genres.OrderBy(x => x.Name).ToList();
            }

            return new MoviesViewModel { Movies = vms.OrderBy(x => x.Name).ToList() };
        }

        public string GetDepartmentSelectData(string query)
        {
            var departmentDtos = _departmentAccessor.GetByName(query).OrderBy(x => x.Name);

            return JsonConvert.SerializeObject(departmentDtos.Select(x => new { value = x.Id, text = x.Name }));
        }

        public string GetPersonSelectData(string query)
        {
            var personDtos = _personAccessor.GetByName(query).OrderBy(x => x.Name);

            return JsonConvert.SerializeObject(personDtos.Select(x => new { value = x.Id, text = x.Name }));
        }

        public EditMovieViewModel Save(EditMovieViewModel vm)
        {
            var dto = Mapper.Map<MovieDTO>(vm);

            dto = _movieAccessor.Save(dto);

            _movieGenreAccessor.SaveAll(dto.Id, vm.GenreIds);
            _personEngine.UpdateCast(vm.Cast, dto.Id);
            _personEngine.UpdateCrew(vm.Crew, dto.Id);

            vm = Mapper.Map<EditMovieViewModel>(dto);

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
