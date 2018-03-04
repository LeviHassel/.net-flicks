using AutoFixture;
using AutoMapper;
using CoreTemplate.Accessors.Interfaces;
using CoreTemplate.Accessors.Models.DTO;
using CoreTemplate.Managers.Managers;
using CoreTemplate.ViewModels.Movie;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CoreTemplate.Tests.ManagerTests
{
    [Collection("Managers")]
    public class MovieManagerTest
    {
        private Fixture _fixture;

        private MovieManager _movieManager;

        private Mock<IGenreAccessor> _genreAccessorMock;
        private Mock<IJobAccessor> _jobAccessorMock;
        private Mock<IMovieAccessor> _movieAccessorMock;
        private Mock<IMovieGenreAccessor> _movieGenreAccessorMock;
        private Mock<IMoviePersonAccessor> _moviePersonAccessorMock;
        private Mock<IPersonAccessor> _personAccessorMock;

        //This is method is called before the start of every test in this class
        public MovieManagerTest()
        {
            _movieAccessorMock = new Mock<IMovieAccessor>();

            _movieManager = new MovieManager(_genreAccessorMock.Object,
                _jobAccessorMock.Object,
                _movieAccessorMock.Object,
                _movieGenreAccessorMock.Object,
                _moviePersonAccessorMock.Object,
                _personAccessorMock.Object);

            //Set up a Fixture to populate random data: https://github.com/AutoFixture/AutoFixture
            _fixture = new Fixture();
        }

        [Fact]
        public void Get()
        {
            //Arrange
            var expectedMovieDto = _fixture.Create<MovieDTO>();
            var expectedMovieVm = Mapper.Map<MovieViewModel>(expectedMovieDto);

            _movieAccessorMock
                .Setup(x => x.Get(expectedMovieDto.Id))
                .Returns(expectedMovieDto);

            //Act
            var actualMovieVm = _movieManager.Get(expectedMovieDto.Id);

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
            var expectedMovieVm = _fixture.Create<MovieViewModel>();
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
