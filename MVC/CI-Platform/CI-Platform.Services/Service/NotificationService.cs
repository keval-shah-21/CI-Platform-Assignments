using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.Constants;
using CI_Platform.Entities.DataModels;
using CI_Platform.Entities.ViewModels;
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
    public async Task SendNotificationToAllUsers(SendNotificationVM sendNotificationVM, List<long> toUsers)
    {
        IEnumerable<NotificationSettingVM> settings = toUsers?.Count != 0
            ? await _notificationSettingService.GetAllToSendRecommendNotification(toUsers, sendNotificationVM.SettingType.ToString())
            : await _notificationSettingService.GetAllToSendNotification(sendNotificationVM.SettingType.ToString());
        
        var notification = new Notification()
        {
            Message = sendNotificationVM.Message,
            NotificationType = (byte?)sendNotificationVM.NotificationType,
            SettingType = (byte?)sendNotificationVM.SettingType
        };
        await _unitOfWork.Notification.AddAsync(notification);
        await _unitOfWork.SaveAsync();

        IEnumerable<UserNotification> notifs = settings.Select(setting =>
        {
            if (setting.Email)
                _ = SendNotificationEmail(setting.UserEmail, setting.UserName, sendNotificationVM);
            return new UserNotification()
            {
                NotificationId = notification.NotificationId,
                UserId = setting.UserId,
                CreatedAt = DateTimeOffset.Now,
                FromUserAvatar = sendNotificationVM?.FromUserAvatar,
            };
        });
        await _unitOfWork.UserNotification.AddRangeAsync(notifs);
        await _unitOfWork.SaveAsync();
    }
    public async Task SendUserNotification(SendNotificationVM sendNotificationVM)
    {
        NotificationSettingVM setting = await _notificationSettingService.GetByUserIdToSendNotification(sendNotificationVM.UserId, sendNotificationVM.SettingType.ToString());

        if (setting == null) return;
        if (setting.Email)
            _ = SendNotificationEmail(setting.UserEmail, setting.UserName, sendNotificationVM);

        UserNotification userNotification = new UserNotification()
        {
            Notification = new Notification()
            {
                Message = sendNotificationVM.Message,
                NotificationType = (byte?)sendNotificationVM.NotificationType,
                SettingType = (byte?)sendNotificationVM.SettingType
            },
            UserId = setting.UserId,
            CreatedAt = DateTimeOffset.Now,
            FromUserAvatar = sendNotificationVM.FromUserAvatar,
        };
        await _unitOfWork.UserNotification.AddAsync(userNotification);
        await _unitOfWork.SaveAsync();
    }
    private async Task SendNotificationEmail(string userEmail, string userName, SendNotificationVM snVM)
    {
        if (snVM.SettingType != NotificationSettingType.RECOMMEND_MISSION && snVM.SettingType != NotificationSettingType.RECOMMEND_STORY && snVM.SettingType != NotificationSettingType.NEW_MESSAGE)
        {
            string subject = NotificationUtility.GetNotificationEmailSubject(snVM.SettingType);
            string body;
            if (snVM.SettingType == NotificationSettingType.NEW_MISSION || snVM.SettingType == NotificationSettingType.NEWS)
                body = NotificationUtility.GetEmailBodyWithLinkHtml(snVM.Message, userName, snVM.Url);
            else
                body = NotificationUtility.GetEmailBodyHtml(snVM.Message, userName);
            _ = _emailService.SendEmailAsync(userEmail, subject, body);
        }
    }
}
