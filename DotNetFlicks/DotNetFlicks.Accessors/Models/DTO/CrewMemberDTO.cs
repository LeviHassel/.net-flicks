using DotNetFlicks.Accessors.Models.DTO.Base;

namespace DotNetFlicks.Accessors.Models.DTO
{
    public class CrewMemberDTO : EntityDTO
    {
        public int MovieId { get; set; }

        public MovieDTO Movie { get; set; }

        public int PersonId { get; set; }

        public PersonDTO Person { get; set; }

        public int DepartmentId { get; set; }

        public DepartmentDTO Department { get; set; }

        public string Position { get; set; }
    }
}
