using CI_Platform.Entities.ViewModels;

namespace CI_Platform.Services.Service.Interface;

public interface IMissionService
{
    List<MissionVM> GetAllMissions();

    List<CountryVM> GetCountriesByMissions(List<MissionVM> missionVM);

    List<CityVM> GetCitiesByMissions(List<MissionVM> missionVM);

    List<MissionVM> FilterMissions(int[]? country, int[]? city, int[]? theme, int[]? skill, string? search, int? sort, long? userId);

    MissionVM GetMissionById(long? id);

    MissionVM UpdateMissionRating(long id);
}
