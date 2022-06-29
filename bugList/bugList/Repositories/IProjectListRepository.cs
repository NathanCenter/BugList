using bugList.Models;
using System.Collections.Generic;

namespace bugList.Repositories
{
    public interface IProjectListRepository
    {
         List<ProjectList> GetAllProjects();
        public ProjectList GetProjectById(int id);

        public void CreateProject(ProjectList project);

        public void Delete(int id);
        public void Edit(ProjectList project);
    }
}
