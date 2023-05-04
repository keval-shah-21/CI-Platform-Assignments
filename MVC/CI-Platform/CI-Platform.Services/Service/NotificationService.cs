using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.Constants;
using CI_Platform.Entities.DataModels;
using CI_Platform.Services.Service.Interface;

namespace CI_Platform.Services.Service;

public class NotificationService : INotificationService
{
    private readonly INotificationSettingService _notificationSettingService;
    private readonly IEmailService _emailService;
    private readonly IUnitOfWork _unitOfWork;
    public NotificationService(INotificationSettingService notificationSettingService,
        IEmailService emailService, IUnitOfWork unitOfWork)
    {
        _emailService = emailService;
        _notificationSettingService = notificationSettingService;
        _unitOfWork = unitOfWork;
    }
    public async Task SendNotification(string message, NotificationType type)
    {
        var settings = await _unitOfWork.NotificationSetting.GetAllAsync();
        foreach(var setting in settings)
        {
            
        }
    }
    public async Task SendUserNotification(string message, NotificationType type, long userId, string avatar)
    {
        
    }
}
