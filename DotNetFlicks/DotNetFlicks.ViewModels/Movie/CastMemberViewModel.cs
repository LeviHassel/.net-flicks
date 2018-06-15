using DotNetFlicks.ViewModels.Base;

namespace DotNetFlicks.ViewModels.Movie
{
    public class CastMemberViewModel : EntityViewModel
    {
        public int Index { get; set; }

        public bool IsDeleted { get; set; }

        public int PersonId { get; set; }

        public string PersonName { get; set; }

        public string Role { get; set; }

        public int Order { get; set; }
    }
}
