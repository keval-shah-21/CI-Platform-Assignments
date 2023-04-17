using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.Constants;
using CI_Platform.Entities.DataModels;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;

namespace CI_Platform.Services.Service;

public class MissionTimesheetService : IMissionTimesheetService
{
    private readonly IUnitOfWork _unitOfWork;

    public MissionTimesheetService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public MissionTimesheetGoalVM GetTimesheetGoalById(long timesheetId)
    {
        MissionTimesheet obj = _unitOfWork.MissionTimesheet.GetFirstOrDefaultWithInclude(mt => mt.TimesheetId == timesheetId);
        return obj == null ? null! : new MissionTimesheetGoalVM()
        {
            TimesheetId = obj.TimesheetId,
            Action = obj.Action,
            ApprovalStatus = obj.ApprovalStatus == 0 ? ApprovalStatus.PENDING :
                    obj.ApprovalStatus == 1 ? ApprovalStatus.APPROVED : ApprovalStatus.DECLINED,
            DateVolunteered = obj.DateVolunteered,
            MissionId = obj.MissionId,
            MissionName = obj.Mission.Title,
            Notes = obj.Notes,
            UserId = obj.UserId
        };
    }
    public MissionTimesheetHourVM GetTimesheetHourById(long timesheetId)
    {
        MissionTimesheet obj = _unitOfWork.MissionTimesheet.GetFirstOrDefaultWithInclude(mt => mt.TimesheetId == timesheetId);
        return obj == null ? null! : new MissionTimesheetHourVM()
        {
            TimesheetId = obj.TimesheetId,
            Hours = obj.TimeVolunteered.Value.Hours,
            Minutes = obj.TimeVolunteered.Value.Minutes,
            ApprovalStatus = obj.ApprovalStatus == 0 ? ApprovalStatus.PENDING :
                    obj.ApprovalStatus == 1 ? ApprovalStatus.APPROVED : ApprovalStatus.DECLINED,
            DateVolunteered = obj.DateVolunteered,
            MissionId = obj.MissionId,
            Notes = obj.Notes,
            MissionName = obj.Mission.Title,
            UserId = obj.UserId
        };
    }

    public void AddTimesheetHour(MissionTimesheetHourVM timesheet)
    {
        _unitOfWork.MissionTimesheet.Add(new MissionTimesheet()
        {
            ApprovalStatus = 0,
            CreatedAt = DateTimeOffset.Now,
            Notes = timesheet.Notes,
            DateVolunteered = timesheet.DateVolunteered,
            UserId = timesheet.UserId,
            MissionId = timesheet.MissionId,
            TimeVolunteered = new TimeSpan((int)(timesheet.Hours), (int)(timesheet.Minutes), 0),
        });
    }

    public void AddTimesheetGoal(MissionTimesheetGoalVM timesheet)
    {
        _unitOfWork.MissionTimesheet.Add(new MissionTimesheet()
        {
            ApprovalStatus = 0,
            CreatedAt = DateTimeOffset.Now,
            Notes = timesheet.Notes,
            DateVolunteered = timesheet.DateVolunteered,
            UserId = timesheet.UserId,
            MissionId = timesheet.MissionId,
            Action = timesheet.Action,
        });
    }

    public void EditTimesheetHour(MissionTimesheetHourVM timesheet)
    {
        MissionTimesheet mt = _unitOfWork.MissionTimesheet.GetFirstOrDefault(mt => mt.TimesheetId == timesheet.TimesheetId);
        if (mt == null) throw new Exception();
        mt.MissionId = timesheet.MissionId;
        mt.UpdatedAt = DateTimeOffset.Now;
        mt.DateVolunteered = timesheet.DateVolunteered;
        mt.TimeVolunteered = new TimeSpan((int)(timesheet.Hours), (int)(timesheet.Minutes), 0);
        mt.Notes = timesheet.Notes;
    }
    public void EditTimesheetGoal(MissionTimesheetGoalVM timesheet)
    {
        MissionTimesheet mt = _unitOfWork.MissionTimesheet.GetFirstOrDefault(mt => mt.TimesheetId == timesheet.TimesheetId);
        if (mt == null) throw new Exception();
        mt.MissionId = timesheet.MissionId;
        mt.UpdatedAt = DateTimeOffset.Now;
        mt.DateVolunteered = timesheet.DateVolunteered;
        mt.Action = timesheet.Action;
        mt.Notes = timesheet.Notes;
    }

    public void DeleteTimesheetById(long timesheetId)
    {
        _unitOfWork.MissionTimesheet.DeleteById(timesheetId);
    }

    public List<MissionTimesheetVM> GetAllByUserId(long userId)
    {
        IEnumerable<MissionTimesheet> missionTimesheets = _unitOfWork.MissionTimesheet.GetAllWithInclude();
        if (missionTimesheets.Any())
        {
            return missionTimesheets.Where(mt => mt.UserId == userId)?.Select(mt => ConvertTimesheetToVM(mt)).ToList();
        }
        return new();
    }

    public List<MissionTimesheetVM> GetGoalTimesheetAdmin()
    {
        return _unitOfWork.MissionTimesheet.GetAllWithInclude()
            .Where(mt => mt.Mission.MissionType == true)
            .Select(mt => ConvertTimesheetToVM(mt))
            .ToList();
    }
    public List<MissionTimesheetVM> GetHourTimesheetAdmin()
    {
        return _unitOfWork.MissionTimesheet.GetAllWithInclude()
            .Where(mt => mt.Mission.MissionType == false)
            .Select(mt => ConvertTimesheetToVM(mt))
            .ToList();
    }

    public List<MissionTimesheetVM> SearchGoalTimehseet(string? query)
    {
        IEnumerable<MissionTimesheet> mt = _unitOfWork.MissionTimesheet.GetAllWithInclude()
                                            .Where(m => m.Mission.MissionType == true);

        return string.IsNullOrEmpty(query) ? mt.Select(m => ConvertTimesheetToVM(m)).ToList()
            : mt
                .Where(m => m.User.FirstName.ToLower().Contains(query.ToLower()) ||
                            m.User.LastName.ToLower().Contains(query.ToLower()) ||
                            m.Mission.Title.ToLower().Contains(query.ToLower()) ||
                            (m.User.FirstName.ToLower() + ' ' + m.User.LastName.ToLower()).Contains(query.ToLower()))
                .Select(m => ConvertTimesheetToVM(m))
                .ToList();
    }

    public List<MissionTimesheetVM> SearchHourTimehseet(string? query)
    {
        IEnumerable<MissionTimesheet> mt = _unitOfWork.MissionTimesheet.GetAllWithInclude()
                                            .Where(m => m.Mission.MissionType == false);

        return string.IsNullOrEmpty(query) ? mt.Select(m => ConvertTimesheetToVM(m)).ToList()
            : mt
                .Where(m => m.User.FirstName.ToLower().Contains(query.ToLower()) ||
                            m.User.LastName.ToLower().Contains(query.ToLower()) ||
                            m.Mission.Title.ToLower().Contains(query.ToLower()) ||
                            (m.User.FirstName.ToLower() + ' ' + m.User.LastName.ToLower()).Contains(query.ToLower()))
                .Select(m => ConvertTimesheetToVM(m))
                .ToList();
    }
    public void UpdateStatus(long id, byte value)
    {
        _unitOfWork.MissionTimesheet.UpdateStatus(id, value);
    }
    public static MissionTimesheetVM ConvertTimesheetToVM(MissionTimesheet mt)
    {
        return new MissionTimesheetVM()
        {
            TimesheetId = mt.TimesheetId,
            MissionId = mt.MissionId,
            MissionType = mt.Mission.MissionType ? MissionType.GOAL : MissionType.TIME,
            MissionName = mt.Mission.Title,
            UserId = mt.UserId,
            User = UserService.ConvertUserToVM(mt.User),
            Action = mt?.Action,
            DateVolunteered = mt.DateVolunteered,
            Notes = mt.Notes,
            TimeVolunteered = mt.TimeVolunteered,
            ApprovalStatus = mt.ApprovalStatus == 0 ? ApprovalStatus.PENDING :
                    mt.ApprovalStatus == 1 ? ApprovalStatus.APPROVED : ApprovalStatus.DECLINED,
        };
    }
}
