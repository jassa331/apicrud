using System.ComponentModel.DataAnnotations;

namespace apicrud.Models
{
    public class Forgetpassword
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Newpassword  { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string Confirmpassword { get; set; }


        public string Token { get; set; }



    }
}
