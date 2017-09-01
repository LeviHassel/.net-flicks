using CoreTemplate.Managers.ViewModels.Movie;

namespace CoreTemplate.Managers.Interfaces
{
    public interface IMovieManager
    {
        MovieViewModel Save(MovieViewModel viewModel);

        MoviesViewModel GetAll();

        MovieViewModel Get(int id);
    }
}
