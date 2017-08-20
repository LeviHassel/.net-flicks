using CoreTemplate.Accessors;
using CoreTemplate.Accessors.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MvcIndividualAuth.Data.Identity
{
  public class ApplicationUserStore : UserStore<ApplicationUser>
  {
    public ApplicationUserStore(CoreTemplateContext context)
        : base(context)
    {
    }
  }
}
