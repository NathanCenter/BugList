using bugList.Models;
using System.Collections.Generic;

namespace bugList.Repositories
{
    public interface IProjectListRepository
    {
         List<ProjectList> GetAllProjects();

    }
}
