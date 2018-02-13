using CoreTemplate.Accessors.Models.DTO;
using System.Collections.Generic;

namespace CoreTemplate.Accessors.Interfaces
{
    public interface IMovieGenreAccessor
    {
        List<MovieGenreDTO> GetAllByMovie(int movieId);

        List<MovieGenreDTO> SaveAll(int movieId, List<int> genreIds);

        List<MovieGenreDTO> DeleteAllByMovie(int movieId);
    }
}
