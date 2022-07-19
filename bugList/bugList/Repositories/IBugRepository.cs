using bugList.Models;
using System.Collections.Generic;

namespace bugList.Repositories
{
    public interface IBugRepository
    {
        List<Bug> GetAllBugs();
        List<Bug> GetBugsByProjectId(int id);
        public void CreateBug(Bug bug);


    }
}
