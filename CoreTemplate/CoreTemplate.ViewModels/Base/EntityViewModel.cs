using Microsoft.AspNetCore.Mvc;

namespace CoreTemplate.ViewModels.Base
{
    public abstract class EntityViewModel
    {
        [HiddenInput]
        public int Id { get; set; }
    }
}
