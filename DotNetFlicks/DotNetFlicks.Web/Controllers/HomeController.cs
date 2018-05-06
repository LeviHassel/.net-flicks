using DotNetFlicks.Managers.Interfaces;
using DotNetFlicks.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace DotNetFlicks.Web.Controllers
{
    public class HomeController : Controller
    {
        private IMovieManager _movieManager;

        public HomeController(IMovieManager movieManager)
        {
            _movieManager = movieManager;
        }

        public IActionResult Index()
        {
            var vm = _movieManager.GetAll();
            vm.Movies = vm.Movies.OrderByDescending(x => x.ReleaseDate).Take(5).ToList();

            return View(vm);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
