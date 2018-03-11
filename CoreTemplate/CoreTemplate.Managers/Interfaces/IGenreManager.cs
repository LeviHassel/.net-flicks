using CoreTemplate.ViewModels.Genre;

namespace CoreTemplate.Managers.Interfaces
{
    public interface IGenreManager
    {
        GenreViewModel Get(int? id);

        GenresViewModel GetAll();

        GenreViewModel Save(GenreViewModel vm);

        GenreViewModel Delete(int id);
    }
}
