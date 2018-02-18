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
        private IJobAccessor _jobAccessor;
        private IMovieAccessor _movieAccessor;
        private IMovieGenreAccessor _movieGenreAccessor;
        private IMoviePersonAccessor _moviePersonAccessor;
        private IPersonAccessor _personAccessor;

        public MovieManager(IGenreAccessor genreAccessor,
            IJobAccessor jobAccessor,
            IMovieAccessor movieAccessor,
            IMovieGenreAccessor movieGenreAccessor,
            IMoviePersonAccessor moviePersonAccessor,
            IPersonAccessor personAccessor)
        {
            _genreAccessor = genreAccessor;
            _jobAccessor = jobAccessor;
            _movieAccessor = movieAccessor;
            _movieGenreAccessor = movieGenreAccessor;
            _moviePersonAccessor = moviePersonAccessor;
            _personAccessor = personAccessor;
        }

        public MovieViewModel Get(int? id)
        {
            var movieDto = id.HasValue ? _movieAccessor.Get(id.Value) : new MovieDTO();
            var jobDtos = _jobAccessor.GetAll().OrderBy(x => x.Name);
            var genreDtos = _genreAccessor.GetAll().OrderBy(x => x.Name);
            var personDtos = _personAccessor.GetAll().OrderBy(x => x.FirstName);

            var vm = Mapper.Map<MovieViewModel>(movieDto);

            vm.GenresSelectList = new MultiSelectList(genreDtos, "Id", "Name", movieDto.Genres != null ? movieDto.Genres.Select(x => x.GenreId).ToList() : null);

            if (movieDto.People != null && movieDto.People.Any())
            {
                vm.People = Mapper.Map<List<MoviePersonViewModel>>(movieDto.People)
                .OrderBy(x => personDtos.Single(y => y.Id == x.PersonId).FirstName)
                .ToList();
            }

            foreach (var personVm in vm.People)
            {
                personVm.People = new SelectList(personDtos, "Id", "FullName", personVm.PersonId);
                personVm.Jobs = new SelectList(jobDtos, "Id", "Name", personVm.JobId);
            }

            return vm;
        }

        public MoviesViewModel GetAll()
        {
            var dtos = _movieAccessor.GetAll();
            var vms = Mapper.Map<List<MovieViewModel>>(dtos);

            return new MoviesViewModel { Movies = vms };
        }

        public MoviePersonViewModel GetNewPerson(int index)
        {
            var personDtos = _personAccessor.GetAll().OrderBy(x => x.FirstName);
            var jobDtos = _jobAccessor.GetAll().OrderBy(x => x.Name);

            var vm = new MoviePersonViewModel
            {
                Index = index,
                People = new SelectList(personDtos, "Id", "FullName"),
                Jobs = new SelectList(jobDtos, "Id", "Name")
            };

            return vm;
        }

        public MovieViewModel Save(MovieViewModel vm)
        {
            var dto = Mapper.Map<MovieDTO>(vm);
            dto.People = null;

            dto = _movieAccessor.Save(dto);

            var moviePersonDtos = new List<MoviePersonDTO>();

            if (vm.People != null && vm.People.Any())
            {
                moviePersonDtos.AddRange(vm.People
                    .Where(x => !x.IsDeleted && x.PersonId != 0 && x.JobId != 0)
                    .Select(x => new MoviePersonDTO
                    {
                        Id = x.Id,
                        MovieId = dto.Id,
                        PersonId = x.PersonId,
                        JobId = x.JobId
                    }));
            }

            _movieGenreAccessor.SaveAll(dto.Id, vm.GenreIds);
            _moviePersonAccessor.SaveAll(dto.Id, moviePersonDtos);

            vm = Mapper.Map<MovieViewModel>(dto);

            return vm;
        }

        public MovieViewModel Delete(int id)
        {
            _movieGenreAccessor.DeleteAllByMovie(id);
            _moviePersonAccessor.DeleteAllByMovie(id);

            var dto = _movieAccessor.Delete(id);
            var vm = Mapper.Map<MovieViewModel>(dto);

            return vm;
        }
    }
}
