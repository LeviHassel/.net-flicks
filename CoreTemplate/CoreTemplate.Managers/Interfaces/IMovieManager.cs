using CoreTemplate.ViewModels.Movie;

namespace CoreTemplate.Managers.Interfaces
{
    public interface IMovieManager
    {
        MovieViewModel Get(int? id);

        MoviesViewModel GetAll();

        CastMemberViewModel GetNewCastMember(int index);

        CrewMemberViewModel GetNewCrewMember(int index);

        MovieViewModel Save(MovieViewModel vm);

        MovieViewModel Delete(int id);
    }
}
