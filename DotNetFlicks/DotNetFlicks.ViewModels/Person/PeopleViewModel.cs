using DotNetFlicks.ViewModels.Shared;
using System.Collections.Generic;

namespace DotNetFlicks.ViewModels.Person
{
    public class PeopleViewModel
    {
        public List<PersonViewModel> People { get; set; }

        public DataTableViewModel DataTable { get; set; }
    }
}
