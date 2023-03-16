using CI_Platform.Entities.ViewModels;

namespace CI_Platform.Services.Service.Interface
{
    public interface ICommentService
    {
        List<CommentVM> GetAll();

    }
}
