using CoreTemplate.Accessors.Interfaces;
using Moq;

namespace CoreTemplate.Tests.Mocks
{
    public class MockMovieAccessor
    {
        private Mock<IMovieAccessor> _movieAccessorMock;

        public MockMovieAccessor()
        {
            _movieAccessorMock = new Mock<IMovieAccessor>();
        }

        public void Uh()
        {
            /*
            _movieAccessorMock
                .Setup(x => x.Get(dto.Id))
                .Returns(dto);
                */

        }
    }
}
