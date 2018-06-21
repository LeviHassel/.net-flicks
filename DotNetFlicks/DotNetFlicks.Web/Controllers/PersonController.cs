using DotNetFlicks.Common.Models;
using DotNetFlicks.Managers.Interfaces;
using DotNetFlicks.ViewModels.Person;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetFlicks.Web.Controllers
{
    [Authorize]
    public class PersonController : Controller
    {
        private IPersonManager _personManager;

        public PersonController(IPersonManager personManager)
        {
            _personManager = personManager;
        }

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, int? pageSize)
        {
            sortOrder = sortOrder ?? (string)TempData["PersonIndexRequest_SortOrder"];
            currentFilter = currentFilter ?? (string)TempData["PersonIndexRequest_CurrentFilter"];
            searchString = searchString ?? (string)TempData["PersonIndexRequest_SearchString"];
            page = page ?? (int?)TempData["PersonIndexRequest_Page"];
            pageSize = pageSize ?? (int?)TempData["PersonIndexRequest_PageSize"];

            TempData["PersonIndexRequest_SortOrder"] = sortOrder;
            TempData["PersonIndexRequest_CurrentFilter"] = currentFilter;
            TempData["PersonIndexRequest_SearchString"] = searchString;
            TempData["PersonIndexRequest_Page"] = page;
            TempData["PersonIndexRequest_PageSize"] = pageSize;

            TempData.Keep();

            var request = new DataTableRequest(sortOrder, currentFilter, searchString, page, pageSize);

            var vms = _personManager.GetAllByRequest(request);

            return View(vms);
        }

        public ActionResult View(int id)
        {
            var vm = _personManager.Get(id);

            return View(vm);
        }

        public ActionResult Edit(int? id)
        {
            var vm = _personManager.Get(id);

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PersonViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _personManager.Save(vm);
                return RedirectToAction("Index");
            }

            return View(vm);
        }

        public ActionResult Delete(int id)
        {
            var vm = _personManager.Get(id);

            return View(vm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _personManager.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
