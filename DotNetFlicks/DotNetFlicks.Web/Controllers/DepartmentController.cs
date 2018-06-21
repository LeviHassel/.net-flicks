using DotNetFlicks.Common.Models;
using DotNetFlicks.Managers.Interfaces;
using DotNetFlicks.ViewModels.Department;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetFlicks.Web.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private IDepartmentManager _departmentManager;

        public DepartmentController(IDepartmentManager departmentManager)
        {
            _departmentManager = departmentManager;
        }

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, int? pageSize)
        {
            sortOrder = sortOrder ?? (string)TempData["DepartmentIndexRequest_SortOrder"];
            currentFilter = currentFilter ?? (string)TempData["DepartmentIndexRequest_CurrentFilter"];
            searchString = searchString ?? (string)TempData["DepartmentIndexRequest_SearchString"];
            page = page ?? (int?)TempData["DepartmentIndexRequest_Page"];
            pageSize = pageSize ?? (int?)TempData["DepartmentIndexRequest_PageSize"];

            TempData["DepartmentIndexRequest_SortOrder"] = sortOrder;
            TempData["DepartmentIndexRequest_CurrentFilter"] = currentFilter;
            TempData["DepartmentIndexRequest_SearchString"] = searchString;
            TempData["DepartmentIndexRequest_Page"] = page;
            TempData["DepartmentIndexRequest_PageSize"] = pageSize;

            TempData.Keep();

            var request = new DataTableRequest(sortOrder, currentFilter, searchString, page, pageSize);

            var vms = _departmentManager.GetAllByRequest(request);

            return View(vms);
        }

        public ActionResult Edit(int? id)
        {
            var vm = _departmentManager.Get(id);

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DepartmentViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _departmentManager.Save(vm);
                return RedirectToAction("Index");
            }

            return View(vm);
        }

        public ActionResult Delete(int id)
        {
            var vm = _departmentManager.Get(id);

            return View(vm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _departmentManager.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
