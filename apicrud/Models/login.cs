using System.ComponentModel.DataAnnotations;

namespace apicrud.Models
{
    public class login
    {
        public int ID { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
