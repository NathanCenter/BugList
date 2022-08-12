using System;
using System.Collections.Generic;
namespace bugList.Models
{
    public class BugProjectViewModel
    {
        public Bug bug { get; set; }
        public List<ProjectList> projectLists { get; set; }
        public List<BugType> bugTypes { get; set; }
        
    }
}
