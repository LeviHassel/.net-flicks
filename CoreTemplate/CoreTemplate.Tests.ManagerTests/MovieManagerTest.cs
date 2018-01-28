using AutoFixture;
using AutoFixture.AutoMoq;
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
        internal IFixture _fixture;

        public MovieManagerTest()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
        }

        [Fact]
        public void MovieManager_GetMovie()
        {
            //Arrange
            var expectedMovieDto = _fixture.Create<MovieDTO>();
            var expectedMovieVm = Mapper.Map<MovieViewModel>(expectedMovieDto);

            var movieAccessorMock = _fixture.Freeze<Mock<IMovieAccessor>>();
            var movieManager = _fixture.Create<MovieManager>();

            movieAccessorMock
                .Setup(x => x.Get(expectedMovieDto.Id))
                .Returns(expectedMovieDto);

            //Act
            var actualMovieVm = movieManager.GetMovie(expectedMovieDto.Id);

            //Assert
            actualMovieVm.ShouldBeEquivalentTo(expectedMovieVm);
        }

        [Fact]
        public void MovieManager_GetAllMovies()
        {
            //Arrange
            var expectedMovieDtos = _fixture.Create<List<MovieDTO>>();

            var movieAccessorMock = _fixture.Freeze<Mock<IMovieAccessor>>();
            var movieManager = _fixture.Create<MovieManager>();

            movieAccessorMock
              .Setup(x => x.GetAll())
              .Returns(expectedMovieDtos);

            //Act
            var actualMoviesVm = movieManager.GetAllMovies();

            //Assert
            foreach (var actualMovieVm in actualMoviesVm.Movies)
            {
                var expectedMovieDto = expectedMovieDtos.Single(x => x.Id == actualMovieVm.Id);
                var expectedMovieVm = Mapper.Map<MovieViewModel>(expectedMovieDto);

                actualMovieVm.ShouldBeEquivalentTo(expectedMovieVm);
            }
        }

        [Fact]
        public void MovieManager_SaveMovie()
        {
            //Arrange
            var expectedMovieVm = _fixture.Create<MovieViewModel>();
            var expectedMovieDto = Mapper.Map<MovieDTO>(expectedMovieVm);

            var movieAccessorMock = _fixture.Freeze<Mock<IMovieAccessor>>();
            var movieManager = _fixture.Create<MovieManager>();

            movieAccessorMock
              .Setup(x => x.Save(It.IsAny<MovieDTO>()))
              .Returns(expectedMovieDto);

            //Act
            var actualMovieVm = movieManager.SaveMovie(expectedMovieVm);

            //Assert
            actualMovieVm.ShouldBeEquivalentTo(expectedMovieVm);
        }
    }
}
