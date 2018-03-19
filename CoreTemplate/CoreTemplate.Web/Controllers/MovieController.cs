using CoreTemplate.Managers.Interfaces;
using CoreTemplate.ViewModels.Movie;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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

        public ActionResult ViewAll()
        {
            var vm = _movieManager.GetAll();
            vm.Movies = vm.Movies.OrderByDescending(x => x.ReleaseDate).ToList();

            return View(vm);
        }

        public ActionResult View(int id)
        {
            var vm = _movieManager.Get(id);

            return View(vm);
        }

        public ActionResult Edit(int? id)
        {
            var vm = _movieManager.GetForEditing(id);

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditMovieViewModel vm)
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

        public ActionResult AddCastMember(int index)
        {
            var vm = _movieManager.GetNewCastMember(index);

            ViewData.TemplateInfo.HtmlFieldPrefix = string.Format("Cast[{0}]", index);

            return PartialView("../Movie/EditorTemplates/CastMemberViewModel", vm);
        }

        public ActionResult AddCrewMember(int index)
        {
            var vm = _movieManager.GetNewCrewMember(index);

            ViewData.TemplateInfo.HtmlFieldPrefix = string.Format("Crew[{0}]", index);

            return PartialView("../Movie/EditorTemplates/CrewMemberViewModel", vm);
        }
    }
}
