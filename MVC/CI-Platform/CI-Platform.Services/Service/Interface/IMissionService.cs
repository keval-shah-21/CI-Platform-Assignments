using CI_Platform.Entities.ViewModels;

namespace CI_Platform.Services.Service.Interface;

public interface IMissionService
{
    List<MissionVM> GetAll();

    List<MissionVM> GetAllIndexMission();

    List<CountryVM> GetCountriesByMission(List<MissionVM> missionVM);

    List<CityVM> GetCitiesByMission(List<MissionVM> missionVM);

    IndexMissionVM FilterData(int? country, int[]? city, int[]? theme, int[]? skill, string? search, string? sort);
}
