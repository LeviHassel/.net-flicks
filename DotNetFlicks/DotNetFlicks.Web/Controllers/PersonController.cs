using DataTablesParser;
using DotNetFlicks.Managers.Interfaces;
using DotNetFlicks.ViewModels.Person;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadData()
        {
            var parser = new Parser<PersonViewModel>(Request.Form, _personManager.GetAll().People.AsQueryable());

            return Json(parser.Parse());
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
