using CoreTemplate.Managers.Interfaces;
using CoreTemplate.ViewModels.Job;
using Microsoft.AspNetCore.Mvc;

namespace CoreTemplate.Web.Controllers
{
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

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(JobViewModel vm)
        {
            _jobManager.Save(vm);

            return RedirectToAction("Index");
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
