using CoreTemplate.ViewModels.Movie;

namespace CoreTemplate.Managers.Interfaces
{
    public interface IMovieManager
    {
        MovieViewModel Get(int id);

        EditMovieViewModel GetForEditing(int? id);

        MoviesViewModel GetAll();

        CastMemberViewModel GetNewCastMember(int index);

        CrewMemberViewModel GetNewCrewMember(int index);

        EditMovieViewModel Save(EditMovieViewModel vm);

        MovieViewModel Delete(int id);
    }
}
