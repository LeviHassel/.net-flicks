using AutoMapper;
using CoreTemplate.Accessors.Interfaces;
using CoreTemplate.Accessors.Models.DTO;
using CoreTemplate.Managers.Interfaces;
using CoreTemplate.Managers.ViewModels.Movie;
using System.Collections.Generic;

/*
 * TODO: Make this my own
 */

namespace CoreTemplate.Managers.Managers
{
    public class MovieManager : IMovieManager
    {
        private IMovieAccessor _movieAccessor;

        public MovieManager(IMovieAccessor movieAccessor)
        {
            _movieAccessor = movieAccessor;
        }

        public MovieViewModel GetMovie(int id)
        {
            var dto = _movieAccessor.Get(id);
            var vm = Mapper.Map<MovieViewModel>(dto);

            return vm;
        }

        public MoviesViewModel GetAllMovies()
        {
            var dtos = _movieAccessor.GetAll();
            var movieVms = Mapper.Map<List<MovieViewModel>>(dtos);

            return new MoviesViewModel { Movies = movieVms };
        }

        public MovieViewModel SaveMovie(MovieViewModel viewModel)
        {
            var dto = Mapper.Map<MovieDTO>(viewModel);
            var dbDto = _movieAccessor.Save(dto);
            var vm = Mapper.Map<MovieViewModel>(dbDto);

            return vm;
        }

        public void DeleteMovie(int id)
        {
            _movieAccessor.Delete(id);
        }
    }
}
