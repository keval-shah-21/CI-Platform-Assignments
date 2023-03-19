using CI_Platform.Services.Service.Interface;
using CI_Platform.DataAccess.Repository.Interface;

using CI_Platform.Entities.ViewModels;
using CI_Platform.Entities.DataModels;
using CI_Platform.Entities.Constants;

namespace CI_Platform.Services.Service;

public class MissionService : IMissionService
{
    private readonly IUnitOfWork _unitOfWork;

    public MissionService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public MissionVM ConvertMissionToVM(Mission mission)
    {
        List<MissionApplicationVM> maVM = GetMissionApplication(mission);
        return new MissionVM()
        {
            MissionId = mission.MissionId,
            CityVM = CityService.ConvertCityToVM(mission.MissionCityNavigation),
            CountryVM = CountryService.ConvertCountryToVM(mission.MissionCountryNavigation),
            Title = mission.Title,
            ShortDescription = mission.ShortDescription,
            Description = mission.Description,
            MissionThemeVM = MissionThemeService.ConvertMissionThemeToVM(mission.MissionTheme),
            OrganizationName = mission.OrganizationName,
            OrganizationDetails = mission.OrganizationDetails,
            StartDate = mission.StartDate,
            EndDate = mission.EndDate,
            Availability = mission.Availability == 0 ? MissionAvailability.DAILY : 
                            mission.Availability == 1? MissionAvailability.WEEKLY :
                            mission.Availability == 2 ? MissionAvailability.WEEK_END : MissionAvailability.MONTHLY,
            TotalSeats = mission.TotalSeats,
            SeatsLeft = (short?)(mission.TotalSeats - (maVM == null ? 0 : maVM.Where(ma => ma.ApprovalStatus == ApprovalStatus.APPROVED).LongCount())),
            MissionType = mission.MissionType ? MissionType.GOAL : MissionType.TIME,
            Status = (bool)mission.Status! ? MissionStatus.ONGOING : MissionStatus.FINISHED,
            MissionRating = mission.MissionRating,
            RegistrationDeadline = mission.RegistrationDeadline,
            MissionThumbnail = GetMissionThumbnail(mission),
            MissionMediaVM = GetOtherMissionMedia(mission),
            MissionDocument = GetMissionDocumentByMission(mission),
            FavouriteMissionVM = GetFavouriteByMission(mission),
            MissionApplicationVM = maVM,
            MissionGoalVM = GetMissionGoal(mission),
            MissionSkillVM = GetMissionSkill(mission),
            SkillList = mission.MissionSkills.Select(ms => ms.Skill.SkillName).ToList(),
            MissionRatingVM = GetMissionRatingsByMission(mission),
            CommentVM = GetCommentsByMission(mission),
        };
    }
    public List<MissionVM> GetAllMissions()
    {
        List<Mission> obj = _unitOfWork.Mission.GetAll().ToList();
        if (obj == null) return null!;
        List<MissionVM> missionVMs = obj.Select(mission => {
            return ConvertMissionToVM(mission);
        }).ToList();
        return missionVMs;
    }

    public MissionVM GetMissionById(long? id)
    {
        Mission mission = _unitOfWork.Mission.GetFirstOrDefault(mission => mission.MissionId == id);
        return ConvertMissionToVM(mission);
    }

    public List<MissionVM> FilterMissions(int[]? country, int[]? city, int[]? theme, int[]? skill, string? search, int? sort, long? userId){
        return MissionFilterService.FilterMissions(country, city, theme, skill, search, sort, userId, _unitOfWork);
    }

    public List<MissionVM> GetRelatedMissions(long id){
        missionVM missionVM = GetMissionById(id);
        return GetAllMissions()
        .Where(mission => mission.MissionTheme.MissionThemeName == missionVM.MissionTheme.MissionThemeName);
    }

    public MissionVM UpdateMissionRating(long id){
        Mission mission = _unitOfWork.Mission.GetFirstOrDefault(mission => mission.MissionId == id);
        byte average = (byte)Math.Ceiling(mission.MissionRatingVM.Average(mr => mr.Rating));
        mission.MissionRating = average;
        _unitOfWork.Mission.Update(mission);
        return ConvertMissionToVM(mission);
    }

    public List<CountryVM> GetCountriesByMissions(List<MissionVM> missionVM){
        List<CountryVM> allCountries = new CountryService(_unitOfWork).GetAll();
        List<CountryVM> countryVM = new();
        missionVM.ForEach(mission => {
            CountryVM co = allCountries.Where(all => all.CountryName == mission.CountryVM.CountryName).First();
            if(!countryVM.Contains(co))
                countryVM.Add(co);
        });
        return countryVM;
    }

    public List<CityVM> GetCitiesByMissions(List<MissionVM> missionVM){
        List<CityVM> allCities = new CityService(_unitOfWork).GetAll();
        List<CityVM> cityVM = new();
        missionVM.ForEach(mission => {
            CityVM co = allCities.Where(all => all.CityName == mission.CityVM.CityName).First();
            if(!cityVM.Contains(co))
                cityVM.Add(co);
        });
        return cityVM;
    }

    internal List<MissionRatingVM> GetMissionRatingsByMission(Mission mission)
    {
        return mission.MissionRatings.Select(r =>
            MissionRatingService.ConvertMissionRatingToVM(r)
        ).ToList();
    }
    internal List<CommentVM> GetCommentsByMission(Mission mission)
    {
        return mission.Comments.Where(c => c.ApprovalStatus == ApprovalStatus.APPROVED).Select(c => 
        CommentService.ConvertCommentToVM(c)).OrderBy(c => c.CreatedAt).ToList();
    }
    internal string GetMissionThumbnail(Mission mission)
    {
        MissionMedium mm = mission.MissionMedia?.FirstOrDefault(mm => mm.Default == true)!;
        return mm != null ? mm.MediaPath + mm.MediaName + mm.MediaType : "";
    }
    internal List<FavouriteMissionVM> GetFavouriteByMission(Mission mission)
    {
        return mission.FavouriteMissions.LongCount() > 0 ? mission.FavouriteMissions.Select(fm =>
            FavouriteMissionService.ConvertFavouriteMissionToVM(fm)    
        ).ToList() : null!;
    }
    internal MissionGoalVM GetMissionGoal(Mission mission){
        return mission.MissionGoals.LongCount() > 0 ? MissionGoalService.ConvertMissionGoalToVM(mission.MissionGoals.First()) : new();
    }
    internal List<MissionDocumentVM> GetMissionDocumentByMission(Mission mission)
    {
        return mission.MissionDocuments.LongCount() > 0 ? mission.MissionDocuments.Select(md =>
        MissionDocumentService.ConvertMissionDocumentToVM(md)).ToList() : null!;
    }
    internal List<MissionApplicationVM> GetMissionApplication(Mission mission){
        return mission.MissionApplications.LongCount() > 0 ? mission.MissionApplications.Select(ma =>
            MissionApplicationService.ConvertMissionApplicationToVM(ma)
            ).ToList() : null!;
    }
    internal List<MissionSkillVM> GetMissionSkill(Mission mission){
        return mission.MissionSkills.LongCount() > 0 ? mission.MissionSkills.Select(ms =>
            MissionSkillService.ConvertMissionSkillToVM(ms)    
        ).ToList() : null!;
    }
    internal List<MissionMediaVM> GetOtherMissionMedia(Mission mission) {
        return mission.MissionMedia.Select(mm => MissionMediaService.ConvertMissionMediaToVM(mm)).
            Where(mm => mm.Default == false).ToList();
    }
}