﻿using CI_Platform.Entities.DataModels;

namespace CI_Platform.DataAccess.Repository.Interface;

public interface INotificationCheckRepository : IRepository<NotificationCheck>
{
    Task UpdateLastCheck(long userId);
}
