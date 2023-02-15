using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace BulkyBook.Models;

public class Category
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required(ErrorMessage="Display Order field is required.")]
    [DisplayName("Dispaly Order")]
    public int DisplayOrder { get; set; }

    public DateTime CreatedDateTime { get; set; } = DateTime.Now;
}
