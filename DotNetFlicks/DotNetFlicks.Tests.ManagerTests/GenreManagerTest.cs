using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using DotNetFlicks.Accessors.Interfaces;
using DotNetFlicks.Accessors.Models.DTO;
using DotNetFlicks.Managers.Managers;
using DotNetFlicks.Tests.ManagerTests.Helpers;
using DotNetFlicks.ViewModels.Genre;
using DotNetFlicks.ViewModels.Movie;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DotNetFlicks.Tests.ManagerTests
{
    [Collection("Managers")]
    public class GenreManagerTest
    {
        private IFixture _fixture;

        private GenreManager _genreManager;

        private Mock<IGenreAccessor> _genreAccessorMock;

        //This is method is called before the start of every test in this class
        public GenreManagerTest()
        {
            _genreAccessorMock = new Mock<IGenreAccessor>();

            _genreManager = new GenreManager(_genreAccessorMock.Object);

            _fixture = AutoFixtureHelper.CreateFixture();
        }

        /*
        [Fact]
        public void Get()
        {
            //Arrange
            var expectedMovieDto = _fixture.Create<MovieDTO>();
            var expectedMovieVm = Mapper.Map<MovieViewModel>(expectedMovieDto);
            var userId = _fixture.Create<string>();

            _genreAccessorMock
                .Setup(x => x.Get(expectedMovieDto.Id))
                .Returns(expectedMovieDto);

            //Act
            var actualMovieVm = _genreManager.Get(expectedMovieDto.Id, userId);

            //Assert
            actualMovieVm.Should().BeEquivalentTo(expectedMovieVm);
        }

        [Fact]
        public void GetAllByRequest()
        {
            //Arrange
            var expectedDtos = _fixture.Create<List<MovieDTO>>();

            _genreAccessorMock
              .Setup(x => x.GetAllByRequest())
              .Returns(expectedDtos);

            //Act
            var actualVm = _genreManager.GetAllByRequest();

            //Assert
            foreach (var actualMovieVm in actualVm.Movies)
            {
                var expectedDto = expectedDtos.Single(x => x.Id == actualVm.Id);
                var expectedVm = Mapper.Map<MovieViewModel>(expectedDto);

                actualVm.Should().BeEquivalentTo(expectedVm);
            }
        }
        */

        [Fact]
        public void Save()
        {
            //Arrange
            var expectedVm = _fixture.Build<GenreViewModel>()
                .Without(x => x.Movies)
                .Create();

            var expectedDto = Mapper.Map<GenreDTO>(expectedVm);

            _genreAccessorMock
              .Setup(x => x.Save(It.IsAny<GenreDTO>()))
              .Returns(expectedDto);

            //Act
            var actualVm = _genreManager.Save(expectedVm);

            //Assert
            actualVm.Should().BeEquivalentTo(expectedVm);
        }
    }
}
