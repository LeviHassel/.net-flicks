using Microsoft.AspNetCore.Mvc;

namespace DotNetFlicks.ViewModels.Base
{
    public abstract class EntityViewModel
    {
        [HiddenInput]
        public int Id { get; set; }
    }
}
