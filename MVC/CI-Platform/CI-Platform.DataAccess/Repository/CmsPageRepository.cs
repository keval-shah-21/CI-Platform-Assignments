using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;

namespace CI_Platform.DataAccess.Repository;

public class CmsPageRepository : Repository<CmsPage>, ICmsPageRepository
{
    public CmsPageRepository(ApplicationDbContext context) : base(context)
    {
    }
}
