using DotNetFlicks.ViewModels.Movie;

namespace DotNetFlicks.Managers.Interfaces
{
    public interface IMovieManager
    {
        MovieViewModel Get(int id, string userId);

        EditMovieViewModel GetForEditing(int? id);

        MoviesViewModel GetAll();

        MoviesViewModel GetAllForUser(string userId);

        void Purchase(int id, string userId);

        void Rent(int id, string userId);

        string GetDepartmentSelectData(string query);

        string GetPersonSelectData(string query);

        EditMovieViewModel Save(EditMovieViewModel vm);

        MovieViewModel Delete(int id);
    }
}
