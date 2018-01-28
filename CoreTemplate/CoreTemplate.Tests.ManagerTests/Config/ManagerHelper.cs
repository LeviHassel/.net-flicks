using AutoMapper;
using CoreTemplate.Accessors.Config;
using CoreTemplate.Accessors.Interfaces;
using CoreTemplate.Managers.Managers;
using Moq;
using Xunit;

namespace CoreTemplate.Tests.ManagerTests
{
    public class ManagerHelper
    {
        public MovieManager MovieManager;

        public Mock<IMovieAccessor> MovieAccessorMock;

        public ManagerHelper()
        {
            //Set up AutoMapper
            Mapper.Initialize(config =>
            {
                config.AddProfile<AccessorMapper>();
            });

            MovieAccessorMock = new Mock<IMovieAccessor>();

            MovieManager = new MovieManager(MovieAccessorMock.Object);
        }
    }
}