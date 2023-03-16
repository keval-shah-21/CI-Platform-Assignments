using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;

namespace CI_Platform.Services.Service
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public List<CommentVM> GetAll()
        {
            IEnumerable<Comment> obj = _unitOfWork.Comment.GetAll();
            if (obj == null) return null!;
            return obj.Select(c => ConvertCommentToVM(c)).ToList();
        }

        public static CommentVM ConvertCommentToVM(Comment c)
        {
            return new CommentVM()
            {
                CommentId = c.CommentId,
                ApprovalStatus = c.ApprovalStatus,
                MissionId = c.MissionId,
                UserId = c.UserId,
            };
        }
    }
}
