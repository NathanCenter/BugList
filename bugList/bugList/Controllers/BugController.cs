using bugList.Models;
using bugList.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace bugList.Controllers
{
    public class BugController : Controller
    {
        // GET: ProjectListController
        private readonly IBugRepository _bugRepository;

        public BugController(IBugRepository bugRepository)
        {
            _bugRepository = bugRepository;
        }
        public IActionResult Index()
        {
            List<Bug> bug = _bugRepository.GetAllBugs();
            return View(bug);
        }
    }
}
