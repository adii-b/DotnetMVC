using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyWeb.Models
{
   public class Category
   {
      [Key] // denotes Id is a primary key
      public int Id { get; set; }

      [Required] // Not null constraint
      [MaxLength(20, ErrorMessage = "Length of name has lie between 1 and 20 characters")]
      [DisplayName("Category Name")] // When we use asp-for in respective view the name in this annotation is displayed

      public string? Name { get; set; }
      [DisplayName("Display Order")]
      [Range(1, 100, ErrorMessage = "Display Order has to lie between 1 and 100")]
      public int DisplayOrder { get; set; }
   }
}