using bugList.Models;
using bugList.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace bugList.Controllers
{
    public class ProjectListController : Controller
    {
        // GET: ProjectListController
        private readonly IProjectListRepository _projectListRepository;
        private readonly IBugRepository _bugRepository;

   
        public ProjectListController(IProjectListRepository projectListRepository,IBugRepository bugRepository) {
            _projectListRepository = projectListRepository;
            _bugRepository = bugRepository;
        }
       
        public ActionResult Index()
        {
            List<ProjectList> projects = _projectListRepository.GetAllProjects();
            return View(projects);
        }

        // GET: ProjectListController/Details/5
        public ActionResult Details(int id)
        {
            List<Bug> bugDetials=_bugRepository.GetBugsByProjectId(id);

            if (bugDetials == null)
            {
                return NotFound();
            }

            return View(bugDetials);
        }

        // GET: ProjectListController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProjectListController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProjectList project)
        {
            try
            {
                _projectListRepository.CreateProject(project);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(project);
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
            ProjectList projects = _projectListRepository.GetProjectById(id);
            return View(projects);
        }

        // POST: ProjectListController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, ProjectList project)
        {
            try
            {
                _projectListRepository.Delete(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(project);
            }
        }
    }
}
