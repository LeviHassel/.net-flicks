using AutoMapper;
using CoreTemplate.Accessors.Interfaces;
using CoreTemplate.Accessors.Models.DTO;
using CoreTemplate.Common.Helpers;
using CoreTemplate.Managers.Interfaces;
using CoreTemplate.ViewModels.Genre;
using System.Collections.Generic;
using System.Linq;

namespace CoreTemplate.Managers.Managers
{
    public class GenreManager : IGenreManager
    {
        private IGenreAccessor _genreAccessor;
        private IMovieGenreAccessor _movieGenreAccessor;

        public GenreManager(IGenreAccessor genreAccessor,
            IMovieGenreAccessor movieGenreAccessor)
        {
            _genreAccessor = genreAccessor;
            _movieGenreAccessor = movieGenreAccessor;
        }

        public GenreViewModel Get(int? id)
        {
            var genreDto = id.HasValue ? _genreAccessor.Get(id.Value) : new GenreDTO();
            var movieGenreDtos = id.HasValue ? _movieGenreAccessor.GetAllByGenre(genreDto.Id) : new List<MovieGenreDTO>();

            var vm = Mapper.Map<GenreViewModel>(genreDto);

            if (movieGenreDtos != null && movieGenreDtos.Any())
            {
                vm.MoviesCount = movieGenreDtos.Count();
                vm.MoviesTooltip = ListHelper.GetBulletedList(movieGenreDtos.Select(x => x.Movie.Name).ToList());
            }

            return vm;
        }

        public GenresViewModel GetAll()
        {
            var genreDtos = _genreAccessor.GetAll();
            var movieGenreDtos = _movieGenreAccessor.GetAll().OrderBy(x => x.Movie.Name);

            var vms = Mapper.Map<List<GenreViewModel>>(genreDtos);

            foreach (var vm in vms)
            {
                var movies = movieGenreDtos.Where(x => x.GenreId == vm.Id);

                if (movies != null && movies.Any())
                {
                    vm.MoviesCount = movies.Count();
                    vm.MoviesTooltip = ListHelper.GetTooltipList(movies.Select(x => x.Movie.Name).ToList());
                }
            }

            return new GenresViewModel { Genres = vms };
        }

        public GenreViewModel Save(GenreViewModel vm)
        {
            var dto = Mapper.Map<GenreDTO>(vm);
            dto = _genreAccessor.Save(dto);
            vm = Mapper.Map<GenreViewModel>(dto);

            return vm;
        }

        public GenreViewModel Delete(int id)
        {
            var dto = _genreAccessor.Delete(id);
            var vm = Mapper.Map<GenreViewModel>(dto);

            return vm;
        }
    }
}
