using CI_Platform.Entities.Constants;

namespace CI_Platform.Entities.ViewModels
{
    public class CommentVM
    {
        public long CommentId { get; set; }

        public long MissionId { get; set; }
        public string? MissionName { get; set; }

        public long UserId { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }

        public string? UserName { get; set; }

        public string? Avatar { get; set; }
        
        public string Text { get; set; } = null!;

        public DateTimeOffset CreatedAt { get; set; }
    }
}
