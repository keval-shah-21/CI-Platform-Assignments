namespace CI_Platform.Services.Service.Interface
{
    public interface IStoryInviteService
    {
        Task RecommendStory(long storyId, long userId, long[] toUsers, string url);
        void RemoveByStoryId(long id);
    }
}
