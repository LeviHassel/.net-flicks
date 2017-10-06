using CoreTemplate.Managers.ViewModels.Movie;

namespace CoreTemplate.Managers.Interfaces
{
    public interface IMovieManager
    {
        MovieViewModel GetMovie(int id);

        MoviesViewModel GetAllMovies();

        MovieViewModel SaveMovie(MovieViewModel vm);

        void DeleteMovie(int id);
    }
}
