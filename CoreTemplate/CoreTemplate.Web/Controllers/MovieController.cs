using CoreTemplate.Managers.Interfaces;
using CoreTemplate.ViewModels.Movie;
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
            var vm = _movieManager.GetAll();

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
                var vm = _movieManager.Get(id.Value);

                return View(vm);
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(MovieViewModel vm)
        {
            _movieManager.Save(vm);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var vm = _movieManager.Get(id);

            return View(vm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _movieManager.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
