using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;

namespace CI_Platform.Services;

public class MissionGoalService : IMissionGoalService
{
    private readonly IUnitOfWork _unitOfWork;

    public MissionGoalService(IUnitOfWork unitOfWork)
    {   
        _unitOfWork = unitOfWork;
    }
    public List<MissionGoalVM> GetAll()
    {
        IEnumerable<MissionGoal> obj = _unitOfWork.MissionGoal.GetAll();
        return obj.Select(mg => ConvertMissionGoalToVM(mg)
        ).ToList();
    }
    public void AddMissionGoal(MissionGoalVM mg)
    {
        _unitOfWork.MissionGoal.Add(new MissionGoal()
        {
            GoalObjective = mg.GoalObjective,
            GoalAchieved = 0,
            GoalValue = mg.GoalValue,
            CreatedAt = DateTimeOffset.Now,
            MissionId = mg.MissionId,
        });
    }
    public void UpdateMissionGoal(MissionGoalVM missionGoal)
    {
        MissionGoal mg = _unitOfWork.MissionGoal.GetFirstOrDefault(m => m.MissionGoalId == missionGoal.MissionGoalId);
        mg.GoalObjective = missionGoal.GoalObjective;
        mg.GoalValue = missionGoal.GoalValue;
        mg.UpdatedAt = DateTimeOffset.Now;
    }
    public void UpdateGoalAchieved(long id, int? value)
    {
        MissionGoal mg = _unitOfWork.MissionGoal.GetFirstOrDefault(m => m.MissionId == id);
        mg.UpdatedAt = DateTimeOffset.Now;
        mg.GoalAchieved = mg.GoalAchieved + value;
    }
    public static MissionGoalVM ConvertMissionGoalToVM(MissionGoal mg)
    {
        return new MissionGoalVM()
        {
            GoalAchieved = mg.GoalAchieved,
            GoalObjective = mg.GoalObjective,
            GoalValue = mg.GoalValue,
            MissionGoalId = mg.MissionGoalId,
            MissionId = mg.MissionId
        };
    }
}
