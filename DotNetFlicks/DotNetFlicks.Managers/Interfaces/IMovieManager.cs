using DotNetFlicks.ViewModels.Movie;

namespace DotNetFlicks.Managers.Interfaces
{
    public interface IMovieManager
    {
        MovieViewModel Get(int id);

        EditMovieViewModel GetForEditing(int? id);

        MoviesViewModel GetAll();

        string GetDepartmentSelectData(string query);

        string GetPersonSelectData(string query);

        EditMovieViewModel Save(EditMovieViewModel vm);

        MovieViewModel Delete(int id);
    }
}
