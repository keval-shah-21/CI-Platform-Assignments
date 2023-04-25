using CI_Platform.Entities.ViewModels;

namespace CI_Platform.Services.Service.Interface;

public interface ICountryService
{   
    List<CountryVM> GetAll();
    List<CountryVM> GetCountriesByMissions(List<IndexMissionVM> missions);
}
