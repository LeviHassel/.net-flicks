using DotNetFlicks.ViewModels.Person;

namespace DotNetFlicks.Managers.Interfaces
{
    public interface IPersonManager
    {
        PersonViewModel Get(int? id);

        PeopleViewModel GetAll();

        int GetCount();

        PersonViewModel Save(PersonViewModel vm);

        PersonViewModel Delete(int id);
    }
}
