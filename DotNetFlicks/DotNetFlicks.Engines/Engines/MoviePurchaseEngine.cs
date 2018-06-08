using DotNetFlicks.Accessors.Interfaces;
using DotNetFlicks.Accessors.Models.DTO;
using DotNetFlicks.Engines.Interfaces;
using System;

namespace DotNetFlicks.Engines.Engines
{
    public class MoviePurchaseEngine : IMoviePurchaseEngine
    {
        private IUserMovieAccessor _userMovieAccessor;

        public MoviePurchaseEngine(IUserMovieAccessor userMovieAccessor)
        {
            _userMovieAccessor = userMovieAccessor;
        }

        public UserMovieDTO GetUserMovie(int id, string userId)
        {
            var userMovieDto = _userMovieAccessor.GetByMovieAndUser(id, userId);

            if (userMovieDto == null)
            {
                userMovieDto = new UserMovieDTO
                {
                    MovieId = id,
                    UserId = userId
                };
            }

            return userMovieDto;
        }

        public UserMovieDTO Purchase(UserMovieDTO userMovieDto)
        {
            userMovieDto.PurchaseDate = DateTime.UtcNow;
            userMovieDto.RentEndDate = null;

            userMovieDto = _userMovieAccessor.Save(userMovieDto);

            return userMovieDto;
        }

        public UserMovieDTO Rent(UserMovieDTO userMovieDto)
        {
            userMovieDto.RentEndDate = DateTime.UtcNow.AddDays(2);

            userMovieDto = _userMovieAccessor.Save(userMovieDto);

            return userMovieDto;
        }
    }
}
