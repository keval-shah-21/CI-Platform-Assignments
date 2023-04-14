using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform.DataAccess.Repository;

public class AdminRepository : Repository<Admin>, IAdminRepository
{
    private readonly ApplicationDbContext _context;
    public AdminRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    public void UpdatePassword(string email, string password)
    {
        var emailParameter = new SqlParameter("@email", email);
        var passwordParameter = new SqlParameter("@password", password);

        _context.Database.ExecuteSqlRaw("UPDATE admin SET password = @password WHERE email = @email", passwordParameter, emailParameter);
    }
}
