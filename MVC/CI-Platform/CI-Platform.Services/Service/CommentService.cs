using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.Constants;
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
            return obj.Select(ConvertCommentToVM).ToList();
        }
        public async Task<IEnumerable<CommentVM>> GetAllAsync()
        {
            IEnumerable<Comment> obj = await _unitOfWork.Comment.GetAllWithIncludeAsync();
            return obj.Select(ConvertCommentToVM);
        }
        public async Task UpdateStatusAsync(long id, byte status)
        {
            await _unitOfWork.Comment.UpdateStatusAsync(id, status);
        }
        public async Task<CommentVM> GetByIdAsync(long id)
        {
            var comment = await _unitOfWork.Comment.GetFirstOrDefaultWithIncludeAsync(c => c.CommentId == id);
            if (comment == null)
                throw new Exception("Invalid comment Id");
            return ConvertCommentToVM(comment);
        }
        public static CommentVM ConvertCommentToVM(Comment c)
        {
            return new CommentVM()
            {
                CommentId = c.CommentId,
                ApprovalStatus = (ApprovalStatus)c.ApprovalStatus,
                MissionId = c.MissionId,
                MissionName = c.Mission.Title,
                UserId = c.UserId,
                UserName = c.User.FirstName + " " + c.User.LastName,
                Avatar = c.User.Avatar,
                Text = c.Text,
                CreatedAt = c.CreatedAt
            };
        }

        public void PostComment(long missionId, long userId, string comment)
        {
            _unitOfWork.Comment.Add(new Comment()
            {
                MissionId = missionId,
                UserId = userId,
                Text = comment,
                CreatedAt = DateTimeOffset.Now
            });
        }
        public async Task<IEnumerable<CommentVM>> SearchAsync(string? query)
        {
            IEnumerable<Comment> comments = await _unitOfWork.Comment.GetAllWithIncludeAsync();
            comments = comments.Where(c => c.ApprovalStatus == 0);
            return string.IsNullOrEmpty(query) ? comments.Select(c => ConvertCommentToVM(c))
                : comments
                    .Where(c => c.Mission.Title.ToLower().Contains(query.ToLower()) ||
                                (c.User.FirstName + ' ' + c.User.LastName).ToLower().Contains(query.ToLower()))
                    .Select(c => ConvertCommentToVM(c));
        }
    }
}
