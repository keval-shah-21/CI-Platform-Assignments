using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;

namespace CI_Platform.Services.Service;

public class CountryService: ICountryService
{
    private readonly IUnitOfWork _unitOfWork;

    public CountryService(IUnitOfWork unitOfWork)
    {   
        _unitOfWork = unitOfWork;
    }
    public List<CountryVM> GetAll()
    {
        IEnumerable<Country> obj = _unitOfWork.Country.GetAll();
        if(obj == null) return null!;
        return obj.Select(c => ConvertCountryToVM(c)).ToList();
    }
    public List<CountryVM> GetCountriesByMissions(List<IndexMissionVM> missions)
    {
        var missionCountryNames = missions.Select(m => m.CountryVM.CountryName).Distinct();
        var countryVM = _unitOfWork.Country
            .GetAll()
            .Where(c => missionCountryNames.Contains(c.CountryName))
            .Select(ConvertCountryToVM)
            .Distinct()
            .ToList();

        return countryVM;
    }
    public static CountryVM ConvertCountryToVM(Country country)
    {
        return new CountryVM(){
            CountryId=country.CountryId,
            CountryName=country.CountryName
        };
    }
}
