using AutoMapper;
using DotNetFlicks.Accessors.Interfaces;
using DotNetFlicks.Accessors.Models.DTO;
using DotNetFlicks.Common.Models;
using DotNetFlicks.Engines.Interfaces;
using DotNetFlicks.Managers.Interfaces;
using DotNetFlicks.ViewModels.Movie;
using DotNetFlicks.ViewModels.Shared;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace DotNetFlicks.Managers.Managers
{
    public class MovieManager : IMovieManager
    {
        private ICastMemberAccessor _castMemberAccessor;
        private ICrewMemberAccessor _crewMemberAccessor;
        private IDepartmentAccessor _departmentAccessor;
        private IGenreAccessor _genreAccessor;
        private IMovieAccessor _movieAccessor;
        private IMovieGenreAccessor _movieGenreAccessor;
        private IMoviePurchaseEngine _moviePurchaseEngine;
        private IMovieRoleUpdateEngine _movieRoleUpdateEngine;
        private IPersonAccessor _personAccessor;
        private IUserMovieAccessor _userMovieAccessor;

        public MovieManager(ICastMemberAccessor castMemberAccessor,
            ICrewMemberAccessor crewMemberAccessor,
            IDepartmentAccessor departmentAccessor,
            IGenreAccessor genreAccessor,
            IMovieAccessor movieAccessor,
            IMovieGenreAccessor movieGenreAccessor,
            IMoviePurchaseEngine moviePurchaseEngine,
            IMovieRoleUpdateEngine movieRoleUpdateEngine,
            IPersonAccessor personAccessor,
            IUserMovieAccessor userMovieAccessor)
        {
            _castMemberAccessor = castMemberAccessor;
            _crewMemberAccessor = crewMemberAccessor;
            _departmentAccessor = departmentAccessor;
            _genreAccessor = genreAccessor;
            _movieAccessor = movieAccessor;
            _movieGenreAccessor = movieGenreAccessor;
            _moviePurchaseEngine = moviePurchaseEngine;
            _movieRoleUpdateEngine = movieRoleUpdateEngine;
            _personAccessor = personAccessor;
            _userMovieAccessor = userMovieAccessor;
        }

        #region Client
        public MovieViewModel Get(int id, string userId)
        {
            var dto = _movieAccessor.Get(id);

            var vm = Mapper.Map<MovieViewModel>(dto);

            vm.Cast = vm.Cast.OrderBy(x => x.Order).ToList();
            vm.Crew = vm.Crew.OrderBy(x => x.Category).ThenBy(x => x.PersonName).ToList();
            vm.Genres = vm.Genres.OrderBy(x => x.Name).ToList();

            var userMovieDto = _userMovieAccessor.GetByMovieAndUser(id, userId);

            if (userMovieDto != null)
            {
                vm.PurchaseDate = userMovieDto.PurchaseDate;
                vm.RentEndDate = userMovieDto.RentEndDate;
            }

            return vm;
        }

        public MoviesViewModel GetAll()
        {
            var dtos = _movieAccessor.GetAll();

            var vms = Mapper.Map<List<MovieViewModel>>(dtos);

            return new MoviesViewModel { Movies = vms.OrderByDescending(x => x.ReleaseDate).ToList() };
        }

        public MoviesViewModel GetAllForUser(string userId)
        {
            var userMovieDtos = _userMovieAccessor.GetAllByUser(userId);

            var vms = new List<MovieViewModel>();

            foreach (var userMovieDto in userMovieDtos)
            {
                var vm = Mapper.Map<MovieViewModel>(userMovieDto.Movie);

                vm.PurchaseDate = userMovieDto.PurchaseDate;
                vm.RentEndDate = userMovieDto.RentEndDate;

                vms.Add(vm);
            }

            return new MoviesViewModel { Movies = vms.OrderByDescending(x => x.PurchaseDateString).ToList() };
        }

        public void Purchase(int id, string userId)
        {
            var userMovieDto = _moviePurchaseEngine.GetUserMovie(id, userId);

            _moviePurchaseEngine.Purchase(userMovieDto);
        }

        public void Rent(int id, string userId)
        {
            var userMovieDto = _moviePurchaseEngine.GetUserMovie(id, userId);

            _moviePurchaseEngine.Rent(userMovieDto);
        }
        #endregion

        #region Administrator
        public EditMovieViewModel GetForEditing(int? id)
        {
            var movieDto = id.HasValue ? _movieAccessor.Get(id.Value) : new MovieDTO();
            var genreDtos = _genreAccessor.GetAll().OrderBy(x => x.Name);

            var vm = Mapper.Map<EditMovieViewModel>(movieDto);

            vm.Cast = vm.Cast.OrderBy(x => x.Order).ToList();
            vm.Crew = vm.Crew.OrderBy(x => x.PersonName).ToList();
            vm.GenresSelectList = new MultiSelectList(genreDtos, "Id", "Name", movieDto.Genres != null ? movieDto.Genres.Select(x => x.GenreId).ToList() : null);

            return vm;
        }

        public MoviesViewModel GetAllByRequest(DataTableRequest request)
        {
            var dtos = _movieAccessor.GetAllByRequest(request);
            var vms = Mapper.Map<List<MovieViewModel>>(dtos);

            vms.ForEach(x => x.Genres.OrderBy(y => y.Name));

            var filteredCount = _movieAccessor.GetCount(request.Search);
            var totalCount = _movieAccessor.GetCount();

            return new MoviesViewModel
            {
                Movies = vms,
                DataTable = new DataTableViewModel(request, filteredCount, totalCount)
            };
        }


        public EditMovieViewModel Save(EditMovieViewModel vm)
        {
            var dto = Mapper.Map<MovieDTO>(vm);

            dto = _movieAccessor.Save(dto);

            _movieGenreAccessor.SaveAll(dto.Id, vm.GenreIds);
            _movieRoleUpdateEngine.UpdateCast(vm.Cast, dto.Id);
            _movieRoleUpdateEngine.UpdateCrew(vm.Crew, dto.Id);

            vm = Mapper.Map<EditMovieViewModel>(dto);

            return vm;
        }

        public MovieViewModel Delete(int id)
        {
            _castMemberAccessor.DeleteAllByMovie(id);
            _crewMemberAccessor.DeleteAllByMovie(id);
            _movieGenreAccessor.DeleteAllByMovie(id);
            _userMovieAccessor.DeleteAllByMovie(id);

            var dto = _movieAccessor.Delete(id);
            var vm = Mapper.Map<MovieViewModel>(dto);

            return vm;
        }

        public string GetDepartmentSelectData(string query)
        {
            var departmentDtos = _departmentAccessor.GetAllByName(query).OrderBy(x => x.Name);

            return JsonConvert.SerializeObject(departmentDtos.Select(x => new { value = x.Id, text = x.Name }));
        }

        public string GetPersonSelectData(string query)
        {
            var personDtos = _personAccessor.GetAllByName(query).OrderBy(x => x.Name);

            return JsonConvert.SerializeObject(personDtos.Select(x => new { value = x.Id, text = x.Name }));
        }
        #endregion
    }
}
