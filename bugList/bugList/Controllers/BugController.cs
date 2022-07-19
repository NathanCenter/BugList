using bugList.Models;
using bugList.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bugList.Controllers
{
    public class BugController : Controller
    {
        // GET: BugController
        private readonly IBugRepository _bugRepository;
        public BugController(IBugRepository bugRepository)
        {
            
            _bugRepository = bugRepository;
        }
        public ActionResult Index()
        {
            return View();
        }

        // GET: BugController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Bug/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BugController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Bug bug)
        {
            try
            {
                _bugRepository.CreateBug(bug);
                return RedirectToAction("Index", "ProjectList");
            }
            catch
            {
                return View(bug);
            }
        }

        // GET: BugController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BugController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BugController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BugController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
