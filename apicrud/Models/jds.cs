using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace apicrud.Models
    {
        public class InfoViewModel
        {
            public info Info { get; set; }

        [NotMapped]
            public List<SelectListItem> NameList { get; set; }
        }
    }
namespace apicrud.Models
{
    public class jds
    {
        public int ID { get; set; }

        public string business { get; set; }


    }
}

//{
//    [Key]
//    public int ID { get; set; }
//    public string Name { get; set; }

//    public List<SelectListItem> NameList { get; set; }
//} }

