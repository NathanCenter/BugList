using bugList.Models;
using System.Collections.Generic;

namespace bugList.Repositories
{
    public interface IBugRepository
    {
        List<Bug> GetAllBugs();
        public Bug GetBugsByProjectId(int id);
        
    }
}
