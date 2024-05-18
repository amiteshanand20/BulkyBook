using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Models
{
    public class Category
    {
        [Key] //tells Entity framework that ID is Primary key
        public int ID { get; set; }
        [Required] //tells Entity framework that Name column will be non-nullable
        [Display(Name="Category Name")]
        public string Name { get; set; }
        [Display(Name ="Display Order")]
        [Range(1,100,ErrorMessage ="Display order must be between 0 to 100 !!!")]
        public int DisplayOrder { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
