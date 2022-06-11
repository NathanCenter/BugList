namespace bugList.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string UserType { get; set; }
        public string FirebaseId { get; set; }
        public string Email { get; set; }
        public bool IsProjectMangager { get; set; }

    }
}
