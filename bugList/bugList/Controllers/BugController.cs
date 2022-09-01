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
        private readonly IProjectListRepository _projectListRepository;
        private readonly IBugTypeRepository _bugTypeRepository;

        public BugController(IBugRepository bugRepository, IProjectListRepository projectList, IBugTypeRepository bugTypeRepository)
        {

            _bugRepository = bugRepository;
            _projectListRepository = projectList;
            _bugTypeRepository = bugTypeRepository;
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
            List<ProjectList> projectlist = _projectListRepository.GetAllProjects();
            List<BugType> bugTypesList = _bugTypeRepository.GetAllBugsType();

            BugProjectViewModel vm = new BugProjectViewModel()
            {
                bug = new Bug(),
                projectLists = projectlist,
                bugTypes = bugTypesList
            };

            return View(vm);



        }

        // POST: BugController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        // may have to refactore the code inorder to have both the view of the project  and the bug type 
        //https://github.com/nashville-software-school/bangazon-inc/blob/cohort-53/book-2-mvc/chapters/VIEW_MODELS.md
        public ActionResult Create(Bug bug, string action)
        {
            var projects = _projectListRepository.GetAllProjects();
            var bugTypes = _bugTypeRepository.GetAllBugsType();
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
            List<BugType> bugTypesList = _bugTypeRepository.GetAllBugsType();
            Bug bug = _bugRepository.GetBugById(id);
            BugProjectViewModel vm = new BugProjectViewModel()
            {

                bug = _bugRepository.GetBugById(id),
                bugTypes = bugTypesList
            };
            if (bug == null)
            {
                return NotFound();
            }
            return View(vm);
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
            Bug bug = _bugRepository.GetBugById(id);
            return View(bug);
        }

        // POST: BugController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Bug bug, string action)
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
