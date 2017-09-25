using AutoMapper;
using CoreTemplate.Accessors.Interfaces;
using CoreTemplate.Accessors.Models.DTO;
using CoreTemplate.Managers.Managers;
using CoreTemplate.Managers.ViewModels.Movie;
using CoreTemplate.Tests.Helpers;
using Moq;
using Ploeh.AutoFixture.Xunit2;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CoreTemplate.Tests.UnitTests.Managers
{
    public class MovieManagerTest
    {
        private MovieManager _movieManager;
        private Mock<IMovieAccessor> _movieAccessorMock;

        public MovieManagerTest()
        {
            _movieAccessorMock = new Mock<IMovieAccessor>();
            _movieManager = new MovieManager(_movieAccessorMock.Object);

            Mapper.Initialize(x =>
            {
                x.CreateMap<MovieDTO, MovieViewModel>().ReverseMap();
            });
        }

        [Theory, AutoData]
        public void MovieManager_GetMovie(MovieDTO dto)
        {
            //Arrange
            _movieAccessorMock
                .Setup(x => x.Get(dto.Id))
                .Returns(dto);

            //Act
            var vm = _movieManager.GetMovie(dto.Id);

            //Assert
            var dtoVm = Mapper.Map<MovieViewModel>(dto);

            Assert.True(PropertyComparer.Equal(dtoVm, vm));
        }

        [Theory, AutoData]
        public void MovieManager_GetAllMovies(MovieDTO dto1, MovieDTO dto2)
        {
            //Arrange
            var dtoList = new List<MovieDTO> { dto1, dto2 };

            _movieAccessorMock
              .Setup(x => x.GetAll())
              .Returns(dtoList);

            //Act
            var vms = _movieManager.GetAllMovies();

            //Assert
            foreach (var vm in vms.Movies)
            {
                var dto = dtoList.Single(x => x.Id == vm.Id);
                var dtoVm = Mapper.Map<MovieViewModel>(dto);

                Assert.True(PropertyComparer.Equal(dtoVm, vm));
            }
        }

        [Theory, AutoData]
        public void MovieManager_SaveMovie(MovieViewModel vm)
        {
            //Arrange
            var dto = Mapper.Map<MovieDTO>(vm);

            _movieAccessorMock
              .Setup(x => x.Save(It.IsAny<MovieDTO>()))
              .Returns(dto);

            //Act
            var vmNew = _movieManager.SaveMovie(vm);

            //Assert
            Assert.True(PropertyComparer.Equal(vm, vmNew));
        }
    }
}
