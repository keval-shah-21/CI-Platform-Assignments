using CI_Platform.Entities.Constants;

namespace CI_Platform.Services.Service;

public static class NotificationUtility
{
    public static string GetNotificationEmailSubject(NotificationSettingType settingType)
    {
        string subject = settingType switch
        {
            NotificationSettingType.NEWS => "CI Platform - News Notification",
            NotificationSettingType.VOLUNTEERING_GOAL => "CI Platform - Volunteering Goal Notification",
            NotificationSettingType.VOLUNTEERING_HOUR => "CI Platform - Volunteering Hour Notification",
            NotificationSettingType.COMMENT => "CI Platform - Comment Notification",
            NotificationSettingType.MY_STORY => "CI Platform - My Story Notification",
            NotificationSettingType.NEW_MISSION => "CI Platform - New Mission Notification",
            NotificationSettingType.MISSION_APPLICATION => "CI Platform - Mission Application Notification",
            _ => throw new NotImplementedException()
        };
        return subject;
    }
    public static string GetEmailBodyHtml(string message, string userName)
    {
        return $"<div style='font-size:1rem'>" +
            $"<p>Dear {userName},</p>" +
            $"<p>{message}</p>" +
            $"<p style='margin-top:0.75rem;'>Best Regards,</p>" +
            $"<p>CSR Team</p>" +
            $"</div>";
    }
    public static string GetEmailBodyWithLinkHtml(string message, string userName, string url)
    {
        return $"<div style='font-size:1rem; font-family:'Verdana';'>" +
            $"<p>Dear {userName},</p>" +
            $"<p>{message}</p>" +
            $"<p style='margin-bottom: .25rem;'>Click on the link below to check it out.</p>" +
            $"<a href='{url}' style='text-decoration:none;display:block;width:max-content;border:2px solid #f88634;border-radius:5rem;padding:0.25rem .75rem;color:#f88634;'>Click here</a>" +
            $"<p style='margin-top:0.75rem;'>Best Regards,</p>" +
            $"<p>CSR Team</p>" +
            $"</div>";
    }
}
