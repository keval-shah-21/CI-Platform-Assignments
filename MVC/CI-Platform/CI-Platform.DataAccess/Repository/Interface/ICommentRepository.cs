using CI_Platform.Entities.DataModels;
using System.Linq.Expressions;

namespace CI_Platform.DataAccess.Repository.Interface
{
    public interface ICommentRepository: IRepository<Comment>
    {
        Task UpdateStatusAsync(long id, byte value);
        Task<Comment?> GetFirstOrDefaultWithIncludeAsync(Expression<Func<Comment, bool>> filter);
        Task<IEnumerable<Comment>> GetAllWithIncludeAsync();
    }
}
