using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.Constants;
using CI_Platform.Entities.DataModels;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;
using System.Reflection;

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
    public async Task SendNotificationToAllUsers(string message, NotificationType type, string settingType)
    {
        IEnumerable<NotificationSettingVM> settings = await _notificationSettingService.GetAllWithIncludeAsync();
        List<UserNotification> userNotifications = new();
        foreach (var setting in settings)
        {
            SendNotificationEmail(setting, message, settingType);
            if ((bool)setting.GetType().GetProperty(settingType).GetValue(setting))
            {
                UserNotification userNotification = new UserNotification
                {
                    Notification = new Notification
                    {
                        Message = message,
                        NotificationType = (byte?)type,
                    },
                    UserId = setting.UserId,
                    CreatedAt = DateTimeOffset.Now,
                };
                userNotifications.Add(userNotification);
            }
        }
        try
        {

        userNotifications.ForEach(un => _unitOfWork.UserNotification.Add(un));
        }catch (Exception ex)
        {
            Console.WriteLine("error bro: "+ ex.StackTrace);
        }

        //await _unitOfWork.UserNotification.AddRangeAsync(userNotifications);
        await _unitOfWork.SaveAsync();
    }
    public async Task SendUserNotification(string message, NotificationType type, long userId, string settingType, string? avatar)
    {
        NotificationSettingVM setting = await _notificationSettingService.GetNotificationSettingByUserIdWithInclude(userId);

        if (setting == null) return;
        SendNotificationEmail(setting, message, settingType);

        if ((bool)setting.GetType().GetProperty(settingType)?.GetValue(setting))
        {
            UserNotification userNotification = new UserNotification()
            {
                Notification = new Notification
                {
                    Message = message,
                    NotificationType = (byte?)type,
                },
                UserId = userId,
                CreatedAt = DateTimeOffset.Now,
                FromUserAvatar = avatar,
            };
            await _unitOfWork.UserNotification.AddAsync(userNotification);
            await _unitOfWork.SaveAsync();
        }
    }
    private async Task SendNotificationEmail(NotificationSettingVM setting, string message, string settingType)
    {
        if (setting.Email && settingType != "RecommendMission" && settingType != "RecommendStory" && settingType != "NewMessage")
        {
            PropertyInfo propInfo = typeof(EmailNotificationSubject).GetProperty(settingType);
            string subject = propInfo.GetValue(null).ToString();

            string body = $"<div style='font-size:1rem'><p>{message}</p><p style='margin-top:0.5rem;'>Best Regards,</p><p>CSR Team</p></div>";
            await _emailService.SendEmailAsync(setting.UserEmail, subject, body);
        }
    }
}
