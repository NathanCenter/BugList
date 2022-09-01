using bugList.Models;
using bugList.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace bugList.Controllers
{
    public class BugTypeController : Controller
    {
        // GET: BugTypeController
        private readonly IBugTypeRepository _bugTypeRepository;

        public BugTypeController(IBugTypeRepository bugTypeRepository)
        {
            _bugTypeRepository = bugTypeRepository;
        }
        public ActionResult Index()
        {
            return View();
        }

        // GET: BugTypeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BugTypeController/Create
        public ActionResult Create()
        {



            return View();
        }

        // POST: BugTypeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BugType Type)
        {
            try
            {
                _bugTypeRepository.createBugType(Type);
                return RedirectToAction("Index", "ProjectList");
            }
            catch
            {
                return RedirectToAction("Index", "ProjectList");
            }
        }

        // GET: BugTypeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BugTypeController/Edit/5
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

        // GET: BugTypeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BugTypeController/Delete/5
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
