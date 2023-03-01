using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Models;

public class Category
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public int OrderNumber { get; set; }
}
