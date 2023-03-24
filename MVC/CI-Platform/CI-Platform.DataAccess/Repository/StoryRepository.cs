using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform.DataAccess.Repository
{
    public class StoryRepository : Repository<Story>, IStoryRepository
    {
        private readonly ApplicationDbContext _context;
        public StoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }


        public override IEnumerable<Story> GetAll()
        {
            return dbSet
                .Include(s => s.Mission)
                .Include(s => s.User)
                .Include(s => s.StoryMedia)
                .ToList();
        }
    }
}
