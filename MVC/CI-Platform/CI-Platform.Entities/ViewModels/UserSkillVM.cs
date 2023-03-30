namespace CI_Platform.Entities.ViewModels
{
    public class UserSkillVM
    {
        public long UserSkillId { get; set; }

        public long UserId { get; set; }

        public short SkillId { get; set; }

        public SkillVM Skill { get; set; } = null!;
    }
}
