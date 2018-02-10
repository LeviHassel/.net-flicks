using CoreTemplate.Managers.Interfaces;
using CoreTemplate.ViewModels.Person;
using Microsoft.AspNetCore.Mvc;

namespace CoreTemplate.Web.Controllers
{
    public class PersonController : Controller
    {
        private IPersonManager _personManager;

        public PersonController(IPersonManager personManager)
        {
            _personManager = personManager;
        }

        public ActionResult Index()
        {
            var vm = _personManager.GetAll();

            return View(vm);
        }

        public ActionResult Edit(int? id)
        {
            var vm = _personManager.Get(id);

            return View(vm);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(PersonViewModel vm)
        {
            _personManager.Save(vm);

            return RedirectToAction("Index");
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
