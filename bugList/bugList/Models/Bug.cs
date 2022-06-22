namespace bugList.Models
{
    public class Bug
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Line { get; set; }
        public string Solved { get; set; }
        public int projectId { get; set; }

    }
}
