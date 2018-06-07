using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace DotNetFlicks.Web.Config
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            DbInitializer.Run(host);
            
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}