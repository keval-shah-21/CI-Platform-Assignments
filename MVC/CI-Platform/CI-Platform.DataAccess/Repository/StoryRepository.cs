using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CI_Platform.DataAccess.Repository
{
    public class StoryRepository : Repository<Story>, IStoryRepository
    {
        private readonly ApplicationDbContext _context;
        public StoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Story> GetAllWithInclude()
        {
            return dbSet
                .Include(s => s.Mission)
                    .ThenInclude(m => m.MissionTheme)
                .Include(s => s.User)
                .Include(s => s.StoryMedia)
                .ToList();
        }

        public Story GetFirstOrDefaultWithInclude(Expression<Func<Story, bool>> filter)
        {
            return dbSet
                .Include(s => s.Mission)
                    .ThenInclude(m => m.MissionTheme)
                .Include(s => s.User)
                .Include(s => s.StoryMedia)
            .FirstOrDefault(filter)!;
        }

        public void UpdateTotalViews(long storyId, long totalViews)
        {
            var totalViewsParam = new SqlParameter("@storyView", totalViews);
            var storyIdParam = new SqlParameter("@storyId", storyId);

            _context.Database.ExecuteSqlRaw("UPDATE story SET total_views = @storyView WHERE story_id = @storyId", totalViewsParam, storyIdParam);
        }

        public void RemoveById(long id)
        {
            SqlParameter idParameter = new SqlParameter("@storyId", id);
            _context.Database.ExecuteSqlRaw("DELETE FROM story WHERE story_id = @storyId", idParameter);
        }

        public void UpdateStats(long id, byte value)
        {
            SqlParameter idParameter = new SqlParameter("@storyId", id);
            SqlParameter statusParameter = new SqlParameter("@status", value);

            _context.Database.ExecuteSqlRaw("UPDATE story SET approval_status = @status WHERE story_id = @storyId", statusParameter,  idParameter);
        }
        public async Task<(string, long)> GetDetailsToSendNotification(long id)
        {
            var result = await dbSet
                .FirstOrDefaultAsync(c => c.StoryId== id);
            return (result.Title, result.UserId);
        }
    }
}
