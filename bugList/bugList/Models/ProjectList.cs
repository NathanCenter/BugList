using System;

namespace bugList.Models
{
    public class ProjectList
    {
        public int Id { get; set; }
        public int UserProfileId { get; set; }
        public string ProgrammingLangueage { get; set; }
        public string ProjectName { get; set; }
        public string UserName { get; set; }


    }
}
