using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CI_Platform.DataAccess.Repository
{
    public class CommentRepository: Repository<Comment>, ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context; 
        }
        public async Task<IEnumerable<Comment>> GetAllWithIncludeAsync()
        {
            return await dbSet
                .Include(c => c.User)
                .Include(c => c.Mission)
                .ToListAsync();
        }
        public async Task<Comment?> GetFirstOrDefaultWithIncludeAsync(Expression<Func<Comment, bool>> filter)
        {
            return await dbSet
                .Include(c => c.User)
                .Include(c => c.Mission)
                .FirstOrDefaultAsync(filter);
        }
        public async Task UpdateStatusAsync(long id, byte value)
        {
            string query = "UPDATE comment SET approval_status = {0} WHERE comment_id = {1}";
            await _context.Database.ExecuteSqlRawAsync(query, value, id);
        }
    }
}
