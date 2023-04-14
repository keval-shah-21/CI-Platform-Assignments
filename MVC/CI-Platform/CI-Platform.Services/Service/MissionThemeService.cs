using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;
using System.Linq.Expressions;

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
        return obj.Select(mt => ConvertMissionThemeToVM(mt)).ToList();
    }
    public MissionThemeVM GetThemeById(long id)
    {
        return ConvertMissionThemeToVM(_unitOfWork.MissionTheme.GetFirstOrDefault(mt => mt.MissionThemeId == id));
    }
    public void SaveTheme(MissionThemeVM mt)
    {
        _unitOfWork.MissionTheme.Add(new MissionTheme()
        {
            MissionThemeName = mt.MissionThemeName,
            CreatedAt = DateTimeOffset.Now,
            Status = mt.Status
        });
    }
    public void UpdateTheme(MissionThemeVM mt)
    {
        MissionTheme theme = _unitOfWork.MissionTheme.GetFirstOrDefault(m => m.MissionThemeId == mt.MissionThemeId);
        theme.Status = mt.Status;
        theme.MissionThemeName = mt.MissionThemeName;
        theme.UpdatedAt = DateTimeOffset.Now;
    }
    public void UpdateStatusByid(long id, int value)
    {
        MissionTheme theme = _unitOfWork.MissionTheme.GetFirstOrDefault(mt => mt.MissionThemeId == id);
        theme.Status = value == 1;
        theme.UpdatedAt = DateTimeOffset.Now;
    }
    public void DeleteTheme(long id)
    {
        _unitOfWork.MissionTheme.RemoveById(id);
    }

    public List<MissionThemeVM> Search(string? query) {
        IEnumerable<MissionTheme> themes = _unitOfWork.MissionTheme.GetAll();

        return string.IsNullOrEmpty(query) ? themes.Select(t => ConvertMissionThemeToVM(t)).ToList()
            : themes
                .Where(t => t.MissionThemeName.ToLower().Contains(query.ToLower()))
                .Select(t => ConvertMissionThemeToVM(t))
                .ToList();
    }
    public bool IsAlreadyUsed(long id)
    {
        return _unitOfWork.MissionTheme.IsAlreadyUsed(id);
    }
    public bool IsThemeUnique(string themeName, long? id)
    {
        Expression<Func<MissionTheme, bool>> filter;
        if (id != null)
            filter = theme => theme.MissionThemeName == themeName && theme.MissionThemeId != id;
        else
            filter = theme => theme.MissionThemeName == themeName;
        return _unitOfWork.MissionTheme.GetFirstOrDefault(filter) == null;
    }
    public static MissionThemeVM ConvertMissionThemeToVM(MissionTheme missionTheme)
    {
        return new MissionThemeVM() { 
            MissionThemeId = missionTheme.MissionThemeId,
            MissionThemeName = missionTheme.MissionThemeName,
            Status = missionTheme.Status,
            CreatedAt = missionTheme.CreatedAt,
            UpdatedAt = missionTheme.UpdatedAt
        };
    }
}
