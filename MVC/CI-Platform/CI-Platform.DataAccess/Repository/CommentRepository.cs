using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
namespace CI_Platform.DataAccess.Repository
{
    public class CommentRepository: Repository<Comment>, ICommentRepository
    {
        public CommentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
