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
            var vms = Mapper.Map<List<MovieViewModel>>(dtos);

            return new MoviesViewModel { Movies = vms };
        }

        public MovieViewModel SaveMovie(MovieViewModel vm)
        {
            var dto = Mapper.Map<MovieDTO>(vm);
            dto = _movieAccessor.Save(dto);
            vm = Mapper.Map<MovieViewModel>(dto);

            return vm;
        }

        public MovieViewModel DeleteMovie(int id)
        {
            var dto = _movieAccessor.Delete(id);
            var vm = Mapper.Map<MovieViewModel>(dto);

            return vm;
        }
    }
}
