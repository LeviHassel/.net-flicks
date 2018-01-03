using CoreTemplate.ViewModels.Movie;

namespace CoreTemplate.Managers.Interfaces
{
    public interface IMovieManager
    {
        MovieViewModel GetMovie(int id);

        MoviesViewModel GetAllMovies();

        MovieViewModel SaveMovie(MovieViewModel vm);

        MovieViewModel DeleteMovie(int id);
    }
}
