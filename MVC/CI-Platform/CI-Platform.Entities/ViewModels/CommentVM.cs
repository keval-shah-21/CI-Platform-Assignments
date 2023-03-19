namespace CI_Platform.Entities.ViewModels
{
    public class CommentVM
    {
        public long CommentId { get; set; }

        public long MissionId { get; set; }

        public long UserId { get; set; }

        public byte ApprovalStatus { get; set; }

        public string? UserName { get; set; }

        public string? Avatar { get; set; }
        
        public string Text { get; set; } = null!;

        public DateTimeOffset CreatedAt { get; set; }
    }
}
