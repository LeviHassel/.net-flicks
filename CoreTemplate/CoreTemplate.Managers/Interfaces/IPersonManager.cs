using CoreTemplate.ViewModels.Person;

namespace CoreTemplate.Managers.Interfaces
{
    public interface IPersonManager
    {
        PersonViewModel Get(int? id, bool includeMovies = false);

        PeopleViewModel GetAll();

        PersonViewModel Save(PersonViewModel vm);

        PersonViewModel Delete(int id);
    }
}
