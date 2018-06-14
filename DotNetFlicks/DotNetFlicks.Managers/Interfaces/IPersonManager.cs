using DotNetFlicks.Common.Configuration;
using DotNetFlicks.ViewModels.Person;

namespace DotNetFlicks.Managers.Interfaces
{
    public interface IPersonManager
    {
        PersonViewModel Get(int? id);

        PaginatedList<PersonViewModel> GetQuery(IndexQuery query);

        PersonViewModel Save(PersonViewModel vm);

        PersonViewModel Delete(int id);
    }
}
