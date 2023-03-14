using CI_Platform.Entities.ViewModels;

namespace CI_Platform.Services.Service.Interface;

public interface IMissionService
{
    List<MissionVM> GetAllIndexMission();

    List<CountryVM> GetCountriesByMissions(List<MissionVM> missionVM);

    List<CityVM> GetCitiesByMissions(List<MissionVM> missionVM);

    List<MissionVM> FilterData(int[]? country, int[]? city, int[]? theme, int[]? skill, string? search, int? sort, long? userId);
}
