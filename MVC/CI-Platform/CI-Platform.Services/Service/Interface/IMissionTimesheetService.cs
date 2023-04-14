using CI_Platform.Entities.ViewModels;

namespace CI_Platform.Services.Service.Interface;

public interface IMissionTimesheetService
{
    MissionTimesheetGoalVM GetTimesheetGoalById(long timesheetId);
    MissionTimesheetHourVM GetTimesheetHourById(long timesheetId);
    void AddTimesheetHour(MissionTimesheetHourVM timesheet);
    void AddTimesheetGoal(MissionTimesheetGoalVM timesheet);
    List<MissionTimesheetVM> GetAllByUserId(long userId);
    void EditTimesheetHour(MissionTimesheetHourVM timesheet);
    void EditTimesheetGoal(MissionTimesheetGoalVM timesheet);
    void DeleteTimesheetById(long timesheetId);

    void UpdateStatus(long id, byte value);
    List<MissionTimesheetVM> GetHourTimesheetAdmin();
    List<MissionTimesheetVM> GetGoalTimesheetAdmin();
    List<MissionTimesheetVM> SearchHourTimehseet(string? query);
    List<MissionTimesheetVM> SearchGoalTimehseet(string? query);
}
