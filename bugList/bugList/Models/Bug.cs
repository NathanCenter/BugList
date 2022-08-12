using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bugList.Models
{
    public class Bug
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Line { get; set; }
        public string Solved { get; set; }
        public int projectId { get; set; }
        public int bugTypeId { get; set; }
        public BugType BugType { get; set; }
        public ProjectList ProjectList { get; set; }

      
       

    }
   
}
