using bugList.Models;
using bugList.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace bugList.Controllers
{
    public class ProjectListController : Controller
    {
        // GET: ProjectListController
        private readonly IProjectListRepository _projectListRepository;

        public ProjectListController(IProjectListRepository projectListRepository) {
            _projectListRepository = projectListRepository;
        }
        public ActionResult Index()
        {
            List<ProjectList> projects = _projectListRepository.GetAllProjects();
            return View(projects);
        }

        // GET: ProjectListController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProjectListController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProjectListController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: ProjectListController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProjectListController/Edit/5
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

        // GET: ProjectListController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProjectListController/Delete/5
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
