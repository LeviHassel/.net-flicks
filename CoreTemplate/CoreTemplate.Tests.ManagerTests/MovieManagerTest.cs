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

        private Mock<IMovieAccessor> _movieAccessorMock;

        //This is method is called before the start of every test in this class
        public MovieManagerTest()
        {
            _fixture = new Fixture();

            _movieAccessorMock = new Mock<IMovieAccessor>();

            _movieManager = new MovieManager(
                _movieAccessorMock.Object
            );
        }

        [Fact]
        public void GetMovie()
        {
            //Arrange
            var expectedMovieDto = _fixture.Create<MovieDTO>();
            var expectedMovieVm = Mapper.Map<MovieViewModel>(expectedMovieDto);

            _movieAccessorMock
                .Setup(x => x.Get(expectedMovieDto.Id))
                .Returns(expectedMovieDto);

            //Act
            var actualMovieVm = _movieManager.GetMovie(expectedMovieDto.Id);

            //Assert
            actualMovieVm.ShouldBeEquivalentTo(expectedMovieVm);
        }

        [Fact]
        public void GetAllMovies()
        {
            //Arrange
            var expectedMovieDtos = _fixture.Create<List<MovieDTO>>();

            _movieAccessorMock
              .Setup(x => x.GetAll())
              .Returns(expectedMovieDtos);

            //Act
            var actualMoviesVm = _movieManager.GetAllMovies();

            //Assert
            foreach (var actualMovieVm in actualMoviesVm.Movies)
            {
                var expectedMovieDto = expectedMovieDtos.Single(x => x.Id == actualMovieVm.Id);
                var expectedMovieVm = Mapper.Map<MovieViewModel>(expectedMovieDto);

                actualMovieVm.ShouldBeEquivalentTo(expectedMovieVm);
            }
        }

        [Fact]
        public void SaveMovie()
        {
            //Arrange
            var expectedMovieVm = _fixture.Create<MovieViewModel>();
            var expectedMovieDto = Mapper.Map<MovieDTO>(expectedMovieVm);

            _movieAccessorMock
              .Setup(x => x.Save(It.IsAny<MovieDTO>()))
              .Returns(expectedMovieDto);

            //Act
            var actualMovieVm = _movieManager.SaveMovie(expectedMovieVm);

            //Assert
            actualMovieVm.ShouldBeEquivalentTo(expectedMovieVm);
        }
    }
}
