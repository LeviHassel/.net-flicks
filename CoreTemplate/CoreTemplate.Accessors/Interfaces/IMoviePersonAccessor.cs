using CoreTemplate.Accessors.Models.DTO;
using System.Collections.Generic;

namespace CoreTemplate.Accessors.Interfaces
{
    public interface IMoviePersonAccessor
    {
        List<MoviePersonDTO> GetAllByMovie(int movieId);

        List<MoviePersonDTO> SaveAll(List<MoviePersonDTO> dtos);

        List<MoviePersonDTO> DeleteAllByMovie(int movieId);
    }
}
