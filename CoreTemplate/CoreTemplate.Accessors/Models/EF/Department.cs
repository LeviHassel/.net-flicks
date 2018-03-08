using CoreTemplate.Accessors.Models.EF.Base;

namespace CoreTemplate.Accessors.Models.EF
{
    public class Department : Entity
    {
        public string Name { get; set; }

        public bool IsDirecting { get; set; }
    }
}
