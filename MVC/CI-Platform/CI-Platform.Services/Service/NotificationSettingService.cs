using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;

namespace CI_Platform.Services.Service;

public class NotificationSettingService : INotificationSettingService
{
    private readonly IUnitOfWork _unitOfWork;

    public NotificationSettingService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<NotificationSettingVM> GetByUserId(long userId)
    {
        var setting = await _unitOfWork.NotificationSetting.GetFirstOrDefaultAsync(x => x.UserId == userId);
        return ConvertSettingToVM(setting);
    }
    public async Task<NotificationSettingVM> GetByUserIdToSendNotification(long? id, string settingType)
    {
        var setting = await _unitOfWork.NotificationSetting.GetByUserIdToSendNotification(id, settingType);

        if (setting == null) return null!;
        return ConvertSettingToVM(setting);
    }
    
    public async Task<IEnumerable<NotificationSettingVM>> GetAllToSendNotification(string settingType)
    {
        var settings = await _unitOfWork.NotificationSetting.GetAllToSendNotification(settingType);
        return settings.Select(ConvertSettingToVM);
    }

    public async Task<IEnumerable<NotificationSettingVM>> GetAllToSendRecommendNotification(List<long> toUsers, string settingType)
    {
        var settings = await _unitOfWork.NotificationSetting.GetAllToSendRecommendNotification(toUsers, settingType);
        return settings.Select(ConvertSettingToVM);
    }

    public async Task Add(long userId)
    {
        await _unitOfWork.NotificationSetting.AddAsync(new NotificationSetting()
        {
            UserId = userId,
        });
    }
    public async Task UpdateNotificationSetting(NotificationSettingVM setting)
    {
        NotificationSetting notset = await _unitOfWork.NotificationSetting.GetFirstOrDefaultAsync(n => n.UserId == setting.UserId);
        notset.News = setting.News;
        notset.RecommendMission = setting.RecommendMission;
        notset.RecommendStory = setting.RecommendStory;
        notset.VolunteeringHour = setting.VolunteeringHour;
        notset.VolunteeringGoal = setting.VolunteeringGoal;
        notset.Comment = setting.Comment;
        notset.Email = setting.Email;
        notset.MissionApplication = setting.MissionApplication;
        notset.NewMission = setting.NewMission;
        notset.NewMessage = setting.NewMessage;
        notset.MyStory = setting.MyStory;

        await _unitOfWork.SaveAsync();
    }
    public static NotificationSettingVM ConvertSettingToVM(NotificationSetting setting)
    {
        return new NotificationSettingVM()
        {
            UserId = setting.UserId,
            UserEmail = setting.User?.Email,
            UserName = setting.User?.FirstName + " " + setting.User?.LastName,
            Comment = setting.Comment,
            Email = setting.Email,
            MissionApplication = setting.MissionApplication,
            MyStory = setting.MyStory,
            NewMessage = setting.NewMessage,
            NewMission = setting.NewMission,
            News = setting.News,
            RecommendMission = setting.RecommendMission,
            RecommendStory = setting.RecommendStory,
            VolunteeringGoal = setting.VolunteeringGoal,
            VolunteeringHour = setting.VolunteeringHour,
        };
    }
}
