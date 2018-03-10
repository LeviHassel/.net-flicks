using CoreTemplate.Accessors.Models.EF.Base;

namespace CoreTemplate.Accessors.Models.EF
{
    public class CrewMember : Entity
    {
        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }

        public int PersonId { get; set; }

        public virtual Person Person { get; set; }

        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }

        public string Position { get; set; }
    }
}
