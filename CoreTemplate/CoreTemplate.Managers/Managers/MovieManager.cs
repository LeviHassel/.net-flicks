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
            var jobDtos = _jobAccessor.GetAll();
            var genreDtos = _genreAccessor.GetAll();
            var movieGenreDtos = _movieGenreAccessor.GetAllByMovie(movieDto.Id);
            var moviePersonDtos = _moviePersonAccessor.GetAllByMovie(movieDto.Id);
            var personDtos = _personAccessor.GetAll();

            var vm = Mapper.Map<MovieViewModel>(movieDto);

            vm.GenresSelectList = new MultiSelectList(genreDtos.OrderBy(x => x.Name), "Id", "Name", movieGenreDtos.Select(x => x.GenreId).ToList());

            //TODO: Better name?
            var personValues = personDtos.Select(x => new { Id = x.Id, Name = x.FirstName + " " + x.LastName });

            vm.People = Mapper.Map<List<MoviePersonViewModel>>(moviePersonDtos)
                .OrderBy(x => personValues.Single(y => y.Id == x.PersonId).Name)
                .ToList();

            foreach (var personVm in vm.People)
            {
                personVm.People = new SelectList(personValues.OrderBy(x => x.Name), "Id", "Name", personVm.PersonId);
                personVm.Jobs = new SelectList(jobDtos.OrderBy(x => x.Name), "Id", "Name", personVm.JobId);
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
            var personDtos = _personAccessor.GetAll();
            var jobDtos = _jobAccessor.GetAll();

            //TODO: Better name?
            var personValues = personDtos.Select(x => new { Id = x.Id, Name = x.FirstName + " " + x.LastName });

            var vm = new MoviePersonViewModel
            {
                Index = index,
                People = new SelectList(personValues, "Id", "Name"),
                Jobs = new SelectList(jobDtos, "Id", "Name")
            };

            return vm;
        }

        public MovieViewModel Save(MovieViewModel vm)
        {
            var dto = Mapper.Map<MovieDTO>(vm);

            dto = _movieAccessor.Save(dto);

            _movieGenreAccessor.SaveAll(dto.Id, vm.GenreIds);

            var moviePersonDtos = vm.People
                .Where(x => !x.IsDeleted)
                .Select(x => new MoviePersonDTO
                {
                    Id = x.Id,
                    MovieId = vm.Id,
                    PersonId = x.PersonId,
                    JobId = x.JobId
                })
                .ToList();

            _moviePersonAccessor.SaveAll(moviePersonDtos);

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
