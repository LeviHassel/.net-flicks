using DotNetFlicks.ViewModels.Shared;
using System.Collections.Generic;

namespace DotNetFlicks.ViewModels.Genre
{
    public class GenresViewModel
    {
        public List<GenreViewModel> Genres { get; set; }

        public DataTableViewModel DataTable { get; set; }
    }
}
