using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;

namespace CI_Platform.Services.Service;

public class CityService : ICityService
{
    private readonly IUnitOfWork _unitOfWork;

    public CityService(IUnitOfWork unitOfWork)
    {   
        _unitOfWork = unitOfWork;
    }
    public List<CityVM> GetAll()
    {
        IEnumerable<City> obj = _unitOfWork.City.GetAll();
        if(obj == null) return null!;
        return obj.Select(c => ConvertCityToVM(c)).ToList();
    }
    public List<CityVM> GetCitiesByMissions(List<IndexMissionVM> missions)
    {
        var missionCityNames = missions.Select(m => m.CityVM.CityName).Distinct();
        var cityVM = _unitOfWork.City
            .GetAll()
            .Where(c => missionCityNames.Contains(c.CityName))
            .Select(ConvertCityToVM)
            .Distinct()
            .ToList();

        return cityVM;
    }
    public static CityVM ConvertCityToVM(City city)
    {
        return new CityVM()
        {
            CityId = city.CityId,
            CityName = city.CityName,
            CountryId = city.CountryId
        };
    }
}
