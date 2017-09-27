using CoreTemplate.Managers.Interfaces;
using CoreTemplate.Managers.ViewModels.Movie;
using Microsoft.AspNetCore.Mvc;

namespace CoreTemplate.Web.Controllers
{
    public class MovieController : Controller
    {
        private IMovieManager _movieManager;

        public MovieController(IMovieManager movieManager)
        {
            _movieManager = movieManager;
        }

        public ActionResult Index()
        {
            var vm = _movieManager.GetAllMovies();

            return View(vm);
        }

        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                var vm = new MovieViewModel();

                return View(vm);
            }
            else
            {
                var vm = _movieManager.GetMovie(id.Value);

                return View(vm);
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(MovieViewModel model)
        {
            _movieManager.SaveMovie(model);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var vm = _movieManager.GetMovie(id);

            return View(vm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _movieManager.DeleteMovie(id);

            return RedirectToAction("Index");
        }
    }
}
