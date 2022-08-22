using bugList.Models;
using System.Collections.Generic;

namespace bugList.Repositories
{
    public interface IBugTypeRepository
    {
        List<BugType> GetAllBugsType();
        public void createBugType(BugType bugType);
    }
}
