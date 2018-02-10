using CoreTemplate.ViewModels.Person;

namespace CoreTemplate.Managers.Interfaces
{
    public interface IPersonManager
    {
        PersonViewModel Get(int? id);

        PeopleViewModel GetAll();

        PersonViewModel Save(PersonViewModel vm);

        PersonViewModel Delete(int id);
    }
}
