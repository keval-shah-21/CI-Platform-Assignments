using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.ViewModels;

namespace CI_Platform.Services.Service.Interface
{
    public interface IMissionFilterService
    {
        List<IndexMissionVM> FilterMissions(int[]? country, int[]? city, int[]? theme, int[]? skill, string? search, int? sort, long? userId, List<IndexMissionVM> missionVM);
    }
}