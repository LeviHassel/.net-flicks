using CoreTemplate.Managers.Interfaces;
using CoreTemplate.ViewModels.Job;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreTemplate.Web.Controllers
{
    [Authorize]
    public class JobController : Controller
    {
        private IJobManager _jobManager;

        public JobController(IJobManager jobManager)
        {
            _jobManager = jobManager;
        }

        public ActionResult Index()
        {
            var vm = _jobManager.GetAll();

            return View(vm);
        }

        public ActionResult Edit(int? id)
        {
            var vm = _jobManager.Get(id);

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(JobViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _jobManager.Save(vm);
                return RedirectToAction("Index");
            }

            return View(vm);
        }

        public ActionResult Delete(int id)
        {
            var vm = _jobManager.Get(id);

            return View(vm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _jobManager.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
