using DotNetFlicks.Common.Models;
using DotNetFlicks.ViewModels.Person;
using DotNetFlicks.ViewModels.Shared;

namespace DotNetFlicks.Managers.Interfaces
{
    public interface IPersonManager
    {
        PersonViewModel Get(int? id);

        PaginatedList<PersonViewModel> GetRequest(IndexRequest query);

        PersonViewModel Save(PersonViewModel vm);

        PersonViewModel Delete(int id);
    }
}
