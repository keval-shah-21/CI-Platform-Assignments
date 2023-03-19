using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.ViewModels;

namespace CI_Platform.Services.Service.Interface
{
    public interface IMissionFilterService
    {
        List<MissionVM> FilterMissions(int[]? country, int[]? city, int[]? theme, int[]? skill, string? search, int? sort, long? userId, IUnitOfWork _unitOfWork);
    }
}