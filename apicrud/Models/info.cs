using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apicrud.Models
{
    public class info
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public int business { get; set; }
    }
}


