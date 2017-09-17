using CoreTemplate.Managers.ViewModels.Movie;

namespace CoreTemplate.Managers.Interfaces
{
    public interface IMovieManager
    {
        MovieViewModel SaveMovie(MovieViewModel viewModel);

        MoviesViewModel GetAllMovies();

        MovieViewModel GetMovie(int id);
    }
}
