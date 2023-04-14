using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform.DataAccess.Repository;

public class CmsPageRepository : Repository<CmsPage>, ICmsPageRepository
{
    private readonly ApplicationDbContext _context;

    public CmsPageRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public void RemoveById(long cmsPageId)
    {
        var idParameter = new SqlParameter("@cmsPageId", cmsPageId);
        _context.Database.ExecuteSqlRaw("DELETE FROM cms_page WHERE cms_page_id = @cmsPageId", idParameter);
    }
}
