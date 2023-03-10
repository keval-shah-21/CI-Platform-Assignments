using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;

namespace CI_Platform.Services.Service;

public class MissionThemeService: IMissionThemeService
{
    private readonly IUnitOfWork _unitOfWork;

    public MissionThemeService(IUnitOfWork unitOfWork)
    {   
        _unitOfWork = unitOfWork;
    }
    public List<MissionThemeVM> GetAll()
    {
        IEnumerable<MissionTheme> obj = _unitOfWork.MissionTheme.GetAll();
        if(obj == null) return null!;
        return obj.Select(mt => new MissionThemeVM(){
            MissionThemeId = mt.MissionThemeId,
            MissionThemeName = mt.MissionThemeName
        }
        ).ToList();
    }
}
