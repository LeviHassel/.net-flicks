using CoreTemplate.Managers.Interfaces;
using CoreTemplate.ViewModels.Movie;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreTemplate.Web.Controllers
{
    [Authorize]
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
            var vm = _movieManager.Get(id);

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MovieViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _movieManager.Save(vm);
                return RedirectToAction("Index");
            }

            return View(vm);
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

        public ActionResult AddPerson(int index)
        {
            var vm = _movieManager.GetNewPerson(index);

            ViewData.TemplateInfo.HtmlFieldPrefix = string.Format("People[{0}]", index);

            return PartialView("../Movie/EditorTemplates/MoviePersonViewModel", vm);
        }
    }
}
