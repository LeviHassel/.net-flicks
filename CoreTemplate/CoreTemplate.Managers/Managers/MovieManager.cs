using AutoMapper;
using CoreTemplate.Accessors.Interfaces;
using CoreTemplate.Accessors.Models.DTO;
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
        private IMovieAccessor _movieAccessor;
        private IMovieGenreAccessor _movieGenreAccessor;

        public MovieManager(IGenreAccessor genreAccessor,
            IMovieAccessor movieAccessor,
            IMovieGenreAccessor movieGenreAccessor)
        {
            _genreAccessor = genreAccessor;
            _movieAccessor = movieAccessor;
            _movieGenreAccessor = movieGenreAccessor;
        }

        public MovieViewModel Get(int? id)
        {
            var movieDto = id.HasValue ? _movieAccessor.Get(id.Value) : new MovieDTO();
            var genreDtos = _genreAccessor.GetAll();
            var movieGenreDtos = _movieGenreAccessor.GetAllByMovie(movieDto.Id);

            var vm = Mapper.Map<MovieViewModel>(movieDto);

            vm.GenresSelectList = new MultiSelectList(genreDtos, "Id", "Name", movieGenreDtos.Select(x => x.GenreId).ToList());

            return vm;
        }

        public MoviesViewModel GetAll()
        {
            var dtos = _movieAccessor.GetAll();
            var vms = Mapper.Map<List<MovieViewModel>>(dtos);

            return new MoviesViewModel { Movies = vms };
        }

        public MovieViewModel Save(MovieViewModel vm)
        {
            var dto = Mapper.Map<MovieDTO>(vm);

            dto = _movieAccessor.Save(dto);

            _movieGenreAccessor.SaveAll(dto.Id, vm.GenreIds);

            vm = Mapper.Map<MovieViewModel>(dto);

            return vm;
        }

        public MovieViewModel Delete(int id)
        {
            _movieGenreAccessor.DeleteAllByMovie(id);

            var dto = _movieAccessor.Delete(id);
            var vm = Mapper.Map<MovieViewModel>(dto);

            return vm;
        }
    }
}
