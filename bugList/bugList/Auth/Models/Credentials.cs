using System.ComponentModel.DataAnnotations;

namespace bugList.Auth.Models
{
    public class Credentails
    {

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
