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
        public int age { get; set; }

        [Required]
        public int business { get; set; }

        [NotMapped]
        public string BusinessName { get; set; }

        [Required]
        public string Gender { get; set; }
        public bool IsActive { get; set; } = true;

        public bool IsDeleted { get; set; } = false;  
        [Required]
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdateOn { get; set; }


    }
}


