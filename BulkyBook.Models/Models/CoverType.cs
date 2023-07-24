using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models.Models
{
    public class CoverType
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Display(Name ="Cover Type")]
        [MaxLength(50)]
        public String Name { get; set; }
    }
}
