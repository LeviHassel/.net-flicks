using DotNetFlicks.Accessors.Models.DTO;
using System.Collections.Generic;

namespace DotNetFlicks.Accessors.Interfaces
{
    public interface IMovieGenreAccessor
    {
        /// <summary>
        /// For the given Movie, create all new MovieGenres in list and delete all MovieGenres not in list
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="genreIds"></param>
        /// <returns></returns>
        List<MovieGenreDTO> SaveAll(int movieId, List<int> genreIds);

        List<MovieGenreDTO> DeleteAllByMovie(int movieId);
    }
}
