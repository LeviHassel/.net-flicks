using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CoreTemplate.Accessors.Identity
{
  public class ApplicationUserStore : UserStore<ApplicationUser>
  {
    public ApplicationUserStore(CoreTemplateContext context)
        : base(context)
    {
    }
  }
}
