using AutoMapper;
using CoreTemplate.Accessors.Interfaces;
using CoreTemplate.Accessors.Models.DTO;
using CoreTemplate.Managers.Interfaces;
using CoreTemplate.Managers.ViewModels.Home;
using System.Collections.Generic;

/*
 * TODO: Make this my own - Levi Hassel
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

        public MovieViewModel Get(int id)
        {
            var dto = _movieAccessor.Get(id);
            var vm = Mapper.Map<MovieViewModel>(dto);

            return vm;
        }

        public MoviesViewModel GetAll()
        {
            var dtos = _movieAccessor.GetAll();
            var movieVms = Mapper.Map<List<MovieViewModel>>(dtos);

            return new MoviesViewModel { Movies = movieVms };
        }

        public MovieViewModel Save(MovieViewModel viewModel)
        {
            var dto = Mapper.Map<MovieDTO>(viewModel);
            var dbDto = _movieAccessor.Save(dto);
            var vm = Mapper.Map<MovieViewModel>(dbDto);

            return vm;
        }
    }
}
