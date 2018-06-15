namespace DotNetFlicks.ViewModels.Shared
{
    public class MovieRoleViewModel
    {
        public int PersonId { get; set; }

        public string PersonName { get; set; }

        public int MovieId { get; set; }

        public string MovieName { get; set; }

        public int MovieYear { get; set; }

        public string Role { get; set; }

        public string Category { get; set; }

        public int Order { get; set; }

        public bool IsDirector { get; set; }
    }
}
