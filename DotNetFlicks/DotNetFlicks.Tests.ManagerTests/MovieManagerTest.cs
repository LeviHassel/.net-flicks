using AutoFixture;
using AutoMapper;
using DotNetFlicks.Accessors.Interfaces;
using DotNetFlicks.Accessors.Models.DTO;
using DotNetFlicks.Engines.Interfaces;
using DotNetFlicks.Managers.Managers;
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
        private Fixture _fixture;

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
            _movieAccessorMock = new Mock<IMovieAccessor>();

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

            //Set up a Fixture to populate random data: https://github.com/AutoFixture/AutoFixture
            _fixture = new Fixture();
        }

        [Fact]
        public void Get()
        {
            //Arrange
            var expectedMovieDto = _fixture.Create<MovieDTO>();
            var expectedMovieVm = Mapper.Map<MovieViewModel>(expectedMovieDto);
            var userId = _fixture.Create<string>();

            _movieAccessorMock
                .Setup(x => x.Get(expectedMovieDto.Id))
                .Returns(expectedMovieDto);

            //Act
            var actualMovieVm = _movieManager.Get(expectedMovieDto.Id, userId);

            //Assert
            actualMovieVm.Should().BeEquivalentTo(expectedMovieVm);
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
            var expectedMovieVm = _fixture.Create<EditMovieViewModel>();
            var expectedMovieDto = Mapper.Map<MovieDTO>(expectedMovieVm);

            _movieAccessorMock
              .Setup(x => x.Save(It.IsAny<MovieDTO>()))
              .Returns(expectedMovieDto);

            //Act
            var actualMovieVm = _movieManager.Save(expectedMovieVm);

            //Assert
            actualMovieVm.Should().BeEquivalentTo(expectedMovieVm);
        }
    }
}
