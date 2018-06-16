using DotNetFlicks.Common.Models;
using DotNetFlicks.ViewModels.Person;

namespace DotNetFlicks.Managers.Interfaces
{
    public interface IPersonManager
    {
        PersonViewModel Get(int? id);

        PeopleViewModel GetAllByRequest(DataTableRequest query);

        PersonViewModel Save(PersonViewModel vm);

        PersonViewModel Delete(int id);
    }
}
