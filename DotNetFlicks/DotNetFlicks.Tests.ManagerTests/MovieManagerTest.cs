using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using DotNetFlicks.Accessors.Interfaces;
using DotNetFlicks.Accessors.Models.DTO;
using DotNetFlicks.Engines.Interfaces;
using DotNetFlicks.Managers.Managers;
using DotNetFlicks.Tests.ManagerTests.Helpers;
using DotNetFlicks.ViewModels.Movie;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DotNetFlicks.Tests.ManagerTests
{
    [Collection("Managers")]
    public class MovieManagerTest
    {
        private IFixture _fixture;

        private MovieManager _movieManager;

        private Mock<ICastMemberAccessor> _castMemberAccessorMock;
        private Mock<ICrewMemberAccessor> _crewMemberAccessorMock;
        private Mock<IGenreAccessor> _genreAccessorMock;
        private Mock<IDepartmentAccessor> _departmentAccessorMock;
        private Mock<IMovieAccessor> _movieAccessorMock;
        private Mock<IMovieGenreAccessor> _movieGenreAccessorMock;
        private Mock<IMovieRoleUpdateEngine> _movieRoleUpdateEngineMock;
        private Mock<IMoviePurchaseEngine> _moviePurchaseEngineMock;
        private Mock<IPersonAccessor> _personAccessorMock;
        private Mock<IUserMovieAccessor> _userMovieAccessorMock;

        //This is method is called before the start of every test in this class
        public MovieManagerTest()
        {
            _castMemberAccessorMock = new Mock<ICastMemberAccessor>();
            _crewMemberAccessorMock = new Mock<ICrewMemberAccessor>();
            _genreAccessorMock = new Mock<IGenreAccessor>();
            _departmentAccessorMock = new Mock<IDepartmentAccessor>();
            _movieAccessorMock = new Mock<IMovieAccessor>();
            _movieGenreAccessorMock = new Mock<IMovieGenreAccessor>();
            _movieRoleUpdateEngineMock = new Mock<IMovieRoleUpdateEngine>();
            _moviePurchaseEngineMock = new Mock<IMoviePurchaseEngine>();
            _personAccessorMock = new Mock<IPersonAccessor>();
            _userMovieAccessorMock = new Mock<IUserMovieAccessor>();

            _movieManager = new MovieManager(_castMemberAccessorMock.Object,
                _crewMemberAccessorMock.Object,
                _departmentAccessorMock.Object,
                _genreAccessorMock.Object,
                _movieAccessorMock.Object,
                _movieGenreAccessorMock.Object,
                _moviePurchaseEngineMock.Object,
                _movieRoleUpdateEngineMock.Object,
                _personAccessorMock.Object,
                _userMovieAccessorMock.Object);

            _fixture = AutoFixtureHelper.CreateFixture();
        }

        [Fact]
        public void Get()
        {
            //Arrange
            var movieDto = _fixture.Freeze<MovieDTO>();
            var userMovieDto = _fixture.Create<UserMovieDTO>();
            var userId = _fixture.Create<string>();

            var expectedVm = Mapper.Map<MovieViewModel>(movieDto);
            expectedVm.Cast = expectedVm.Cast.OrderBy(x => x.Order).ToList();
            expectedVm.Crew = expectedVm.Crew.OrderBy(x => x.Category).ThenBy(x => x.PersonName).ToList();
            expectedVm.Genres = expectedVm.Genres.OrderBy(x => x.Name).ToList();
            expectedVm.PurchaseDate = userMovieDto.PurchaseDate;
            expectedVm.RentEndDate = userMovieDto.RentEndDate;

            _movieAccessorMock
                .Setup(x => x.Get(movieDto.Id))
                .Returns(movieDto);

            _userMovieAccessorMock
                .Setup(x => x.GetByMovieAndUser(movieDto.Id, userId))
                .Returns(userMovieDto);

            //Act
            var actualVm = _movieManager.Get(movieDto.Id, userId);

            //Assert
            actualVm.Should().BeEquivalentTo(expectedVm);
        }

        [Fact]
        public void GetAll()
        {
            //Arrange
            var expectedMovieDtos = _fixture.Create<List<MovieDTO>>();

            _movieAccessorMock
              .Setup(x => x.GetAll())
              .Returns(expectedMovieDtos);

            //Act
            var actualMoviesVm = _movieManager.GetAll();

            //Assert
            foreach (var actualMovieVm in actualMoviesVm.Movies)
            {
                var expectedMovieDto = expectedMovieDtos.Single(x => x.Id == actualMovieVm.Id);
                var expectedMovieVm = Mapper.Map<MovieViewModel>(expectedMovieDto);

                actualMovieVm.Should().BeEquivalentTo(expectedMovieVm);
            }
        }

        [Fact]
        public void Save()
        {
            //Arrange
            var expectedVm = _fixture.Create<EditMovieViewModel>();
            var dto = Mapper.Map<MovieDTO>(expectedVm);

            _movieAccessorMock
              .Setup(x => x.Save(It.IsAny<MovieDTO>()))
              .Returns(dto);

            //Act
            var actualVm = _movieManager.Save(expectedVm);

            //Assert
            actualVm.Should().BeEquivalentTo(expectedVm);
        }
    }
}
