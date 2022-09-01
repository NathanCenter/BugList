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
        private readonly IUserProfileRepository _userProfileRepository;
   
        public ProjectListController(IProjectListRepository projectListRepository,IBugRepository bugRepository,IUserProfileRepository userProfileRepository) {
            _projectListRepository = projectListRepository;
            _bugRepository = bugRepository;
            _userProfileRepository = userProfileRepository;
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
            List<ProjectList> projectlist = _projectListRepository.GetAllProjects();
            List<UserProfile> userList=_userProfileRepository.GetAll();

            UserProjectViewModel up = new UserProjectViewModel()
            {
                
                projectLists = projectlist,
                Names = userList
            };
            return View(up);
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
            ProjectList project = _projectListRepository.GetProjectById(id); 
            if(project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        // POST: ProjectListController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProjectList project)
        {
            try
            {
                _projectListRepository.Edit(project);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(project);
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
        public ActionResult Delete(int id, int userId, ProjectList project)
        {
            try
            {
                _projectListRepository.Delete(id,userId);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(project);
            }
        }
    }
}
