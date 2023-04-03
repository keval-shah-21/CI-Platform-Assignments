using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Services.Service.Interface;

namespace CI_Platform.Services.Service;

public class MissionTimesheetService : IMissionTimesheetService
{
    private readonly IUnitOfWork _unitOfWork;

    public MissionTimesheetService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
}
