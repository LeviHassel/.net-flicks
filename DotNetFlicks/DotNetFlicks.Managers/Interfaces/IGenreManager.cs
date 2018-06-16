using DotNetFlicks.Common.Models;
using DotNetFlicks.ViewModels.Genre;

namespace DotNetFlicks.Managers.Interfaces
{
    public interface IGenreManager
    {
        GenreViewModel Get(int? id);

        GenresViewModel GetAllByRequest(DataTableRequest query);

        GenreViewModel Save(GenreViewModel vm);

        GenreViewModel Delete(int id);
    }
}
