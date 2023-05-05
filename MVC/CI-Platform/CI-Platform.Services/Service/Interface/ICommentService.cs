using CI_Platform.Entities.ViewModels;

namespace CI_Platform.Services.Service.Interface
{
    public interface ICommentService
    {
        List<CommentVM> GetAll();
        Task UpdateStatusAsync(long id, byte status);
        Task<IEnumerable<CommentVM>> GetAllAsync();
        void PostComment(long missionId, long userId, string comment);
        Task<CommentVM> GetByIdAsync(long id);
        Task<IEnumerable<CommentVM>> SearchAsync(string? query);
    }
}
