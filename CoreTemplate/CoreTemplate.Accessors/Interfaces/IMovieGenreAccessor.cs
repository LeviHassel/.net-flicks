using CoreTemplate.Accessors.Models.DTO;
using System.Collections.Generic;

namespace CoreTemplate.Accessors.Interfaces
{
    public interface IMovieGenreAccessor
    {
        List<MovieGenreDTO> GetAll();

        List<MovieGenreDTO> GetAllByMovie(int movieId);

        List<MovieGenreDTO> GetAllByGenre(int genreId);

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
