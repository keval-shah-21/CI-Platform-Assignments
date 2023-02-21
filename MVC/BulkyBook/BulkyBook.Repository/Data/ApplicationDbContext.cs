using Microsoft.EntityFrameworkCore;
using BulkyBook.Models;
namespace BulkyBook.Repository;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {            
    }

    public DbSet<Category> Categories { get; set; }
}