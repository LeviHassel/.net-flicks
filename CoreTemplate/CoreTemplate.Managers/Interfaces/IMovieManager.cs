using CoreTemplate.ViewModels.Movie;

namespace CoreTemplate.Managers.Interfaces
{
    public interface IMovieManager
    {
        MovieViewModel Get(int id);

        MoviesViewModel GetAll();

        MovieViewModel Save(MovieViewModel vm);

        MovieViewModel Delete(int id);
    }
}
