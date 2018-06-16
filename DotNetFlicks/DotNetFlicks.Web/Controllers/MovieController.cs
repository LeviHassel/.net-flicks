using DotNetFlicks.Common.Models;
using DotNetFlicks.Managers.Interfaces;
using DotNetFlicks.ViewModels.Movie;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetFlicks.Web.Controllers
{
    [Authorize]
    public class MovieController : Controller
    {
        private IAccountManager _accountManager;
        private IMovieManager _movieManager;

        public MovieController(IAccountManager accountManager,
            IMovieManager movieManager)
        {
            _accountManager = accountManager;
            _movieManager = movieManager;
        }

        #region Client
        public ActionResult ViewAll()
        {
            var vm = _movieManager.GetAll();
            
            return View(vm);
        }

        public ActionResult ViewAllForUser()
        {
            var user = _accountManager.GetApplicationUser(HttpContext.User).Result;

            var vm = _movieManager.GetAllForUser(user.Id);

            return View(vm);
        }

        public ActionResult View(int id)
        {
            var user = _accountManager.GetApplicationUser(HttpContext.User).Result;

            var vm = _movieManager.Get(id, user.Id);

            return View(vm);
        }

        public ActionResult Purchase(int id)
        {
            var user = _accountManager.GetApplicationUser(HttpContext.User).Result;

            _movieManager.Purchase(id, user.Id);

            return RedirectToAction("View", new { id });
        }

        public ActionResult Rent(int id)
        {
            var user = _accountManager.GetApplicationUser(HttpContext.User).Result;

            _movieManager.Rent(id, user.Id);

            return RedirectToAction("View", new { id });
        }
        #endregion

        #region Administrator
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, int? pageSize)
        {
            var request = new DataTableRequest(sortOrder, currentFilter, searchString, page, pageSize);

            var vms = _movieManager.GetAllByRequest(request);

            return View(vms);
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
            var user = _accountManager.GetApplicationUser(HttpContext.User).Result;

            var vm = _movieManager.Get(id, user.Id);

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
            var vm = new CastMemberViewModel { Index = index, Order = index };

            ViewData.TemplateInfo.HtmlFieldPrefix = string.Format("Cast[{0}]", index);

            return PartialView("../Movie/EditorTemplates/CastMemberViewModel", vm);
        }

        public ActionResult AddCrewMember(int index)
        {
            var vm = new CrewMemberViewModel { Index = index };

            ViewData.TemplateInfo.HtmlFieldPrefix = string.Format("Crew[{0}]", index);

            return PartialView("../Movie/EditorTemplates/CrewMemberViewModel", vm);
        }

        public string GetPersonSelectData(string query)
        {
            return _movieManager.GetPersonSelectData(query);
        }

        public string GetDepartmentSelectData(string query)
        {
            return _movieManager.GetDepartmentSelectData(query);
        }
        #endregion
    }
}
