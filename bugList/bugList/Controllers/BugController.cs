using bugList.Models;
using bugList.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
        //https://www.davepaquette.com/archive/2015/05/18/mvc6-select-tag-helper.aspx
        public ActionResult Create(Bug bug,string action)
        {
            if (action == "Cancel")
            {
                return RedirectToAction("Index", "ProjectList");
            }
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
            Bug bug = _bugRepository.GetBugById(id);
            if (bug == null)
            {
                return NotFound();
            }
            return View(bug);
        }

        // POST: BugController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Bug bug)
        {
            try
            {
                _bugRepository.EditBug(bug);
                return RedirectToAction("Index", "ProjectList");
            }
            catch
            {
                return View(bug);
            }
        }

        // GET: BugController/Delete/5
        public ActionResult Delete(int id)
        {
            Bug bug= _bugRepository.GetBugById(id);
            return View(bug);
        }

        // POST: BugController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Bug bug,string action)
        {
            if (action == "Cancel")
            {
                return RedirectToAction("Index", "ProjectList");
            }
            try
            {
                _bugRepository.DeleteBug(id);
                return RedirectToAction("Index", "ProjectList");
            }
            catch
            {
                return View(bug);
            }
           
        }
    }
}
