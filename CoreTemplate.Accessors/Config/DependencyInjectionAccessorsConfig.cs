using CoreTemplate.Accessors.Accessors;
using CoreTemplate.Accessors.Interfaces;

namespace CoreTemplate.Accessors.Config
{
  public class DependencyInjectionAccessorsConfig
  {
    public DependencyInjectionAccessorsConfig()
    {
      // The code below will work in Startup.cs
      // Decide between Scoped, Transient and Singleton
      services.AddScoped<IMovieAccessor, MovieAccessor>();

      // You should only Build the provider once in your code, to do so:
      var provider = services.BuilderServiceProvider(); //to protect against runtime injection, which is an anti-pattern

      // to Get an Actual ICommandHandler
      //var commandHandler = services.GetService<ICommandHandler<MyT>>();

      //Equivalent Code in Ninject:
      //Kernel.Bind<IMyRepository>().To<MyRepository>();
      //Kernel.Bind<DbContext>().To<MvcIndividualAuthContext>().InRequestScope();
      //Kernel.Bind<MvcIndividualAuthContext>().ToSelf().InRequestScope();
      //Kernel.Bind<IUserStore<ApplicationUser, string>>().To<ApplicationUserStore>();
    }
  }
}
