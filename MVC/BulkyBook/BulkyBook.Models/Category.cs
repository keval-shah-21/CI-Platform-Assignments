using System.ComponentModel.DataAnnotaions;
namespace BulkyBook.Models;

public class Category
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public int OrderNumber { get; set; }
}
