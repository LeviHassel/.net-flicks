using AutoMapper;
using CoreTemplate.Accessors.Interfaces;
using CoreTemplate.Accessors.Models.DTO;
using CoreTemplate.Managers.Interfaces;
using CoreTemplate.ViewModels.Genre;
using System.Collections.Generic;

namespace CoreTemplate.Managers.Managers
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
            var genreDto = id.HasValue ? _genreAccessor.Get(id.Value) : new GenreDTO();

            //TODO: Sort the Genre list?
            var vm = Mapper.Map<GenreViewModel>(genreDto);

            return vm;
        }

        public GenresViewModel GetAll()
        {
            var genreDtos = _genreAccessor.GetAll();

            //TODO: Order Movie lists by Movie Name?
            var vms = Mapper.Map<List<GenreViewModel>>(genreDtos);

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
