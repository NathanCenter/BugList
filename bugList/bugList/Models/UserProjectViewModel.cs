using System;
using System.Collections.Generic;
namespace bugList.Models
{
    public class UserProjectViewModel
    {
        public List<UserProfile> Names { get; set; }

        public List<ProjectList> projectLists { get; set; }

        public ProjectList project { get; set; }

        public UserProfile Name { get; set; }
    }
}
