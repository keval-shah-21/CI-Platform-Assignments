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
        return obj.Select(c => new CityVM(){
            CityId = c.CityId,
            CityName = c.CityName
        }
        ).ToList();
    }
}
