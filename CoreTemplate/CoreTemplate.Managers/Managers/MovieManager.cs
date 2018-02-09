using AutoMapper;
using CoreTemplate.Accessors.Interfaces;
using CoreTemplate.Accessors.Models.DTO;
using CoreTemplate.Managers.Interfaces;
using CoreTemplate.ViewModels.Movie;
using System.Collections.Generic;

namespace CoreTemplate.Managers.Managers
{
    public class MovieManager : IMovieManager
    {
        private IMovieAccessor _movieAccessor;

        public MovieManager(IMovieAccessor movieAccessor)
        {
            _movieAccessor = movieAccessor;
        }

        public MovieViewModel Get(int? id)
        {
            var dto = id.HasValue ? _movieAccessor.Get(id.Value) : new MovieDTO();
            var vm = Mapper.Map<MovieViewModel>(dto);

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
            vm = Mapper.Map<MovieViewModel>(dto);

            return vm;
        }

        public MovieViewModel Delete(int id)
        {
            var dto = _movieAccessor.Delete(id);
            var vm = Mapper.Map<MovieViewModel>(dto);

            return vm;
        }
    }
}
