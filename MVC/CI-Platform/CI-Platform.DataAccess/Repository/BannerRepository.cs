using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform.DataAccess.Repository;

public class BannerRepository : Repository<Banner>, IBannerRepository
{
    private readonly ApplicationDbContext _context;
    public BannerRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public void RemoveById(long id)
    {
        var idParameter = new SqlParameter("@bannerId", id);
        _context.Database.ExecuteSqlRaw("DELETE FROM banner WHERE banner_id = @bannerId", idParameter);
    }
}
