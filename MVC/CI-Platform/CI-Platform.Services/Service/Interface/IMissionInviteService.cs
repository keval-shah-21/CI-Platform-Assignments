namespace CI_Platform.Services.Service.Interface
{
    public interface IMissionInviteService
    {
        Task RecommendMission(long missionId, long userId, long[] toUsers, string url);
    }
}
