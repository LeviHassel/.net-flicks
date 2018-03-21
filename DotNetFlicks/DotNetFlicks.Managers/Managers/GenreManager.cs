using AutoMapper;
using DotNetFlicks.Accessors.Interfaces;
using DotNetFlicks.Accessors.Models.DTO;
using DotNetFlicks.Managers.Interfaces;
using DotNetFlicks.ViewModels.Genre;
using System.Collections.Generic;
using System.Linq;

namespace DotNetFlicks.Managers.Managers
{
    public class GenreManager : IGenreManager
    {
        private IGenreAccessor _genreAccessor;

        public GenreManager(IGenreAccessor genreAccessor)
        {
            _genreAccessor = genreAccessor;
        }

        public GenreViewModel Get(int? id)
        {
            var dto = id.HasValue ? _genreAccessor.Get(id.Value) : new GenreDTO();
            var vm = Mapper.Map<GenreViewModel>(dto);

            vm.Movies = vm.Movies.OrderByDescending(x => x.ReleaseDate).ToList();

            return vm;
        }

        public GenresViewModel GetAll()
        {
            var dtos = _genreAccessor.GetAll();
            var vms = Mapper.Map<List<GenreViewModel>>(dtos);

            foreach (var vm in vms)
            {
                vm.Movies = vm.Movies.OrderBy(x => x.Name).ToList();
            }

            return new GenresViewModel { Genres = vms.OrderBy(x => x.Name).ToList() };
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
