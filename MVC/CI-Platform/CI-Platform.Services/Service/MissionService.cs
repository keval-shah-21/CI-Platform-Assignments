using CI_Platform.Services.Service.Interface;
using CI_Platform.DataAccess.Repository.Interface;

using CI_Platform.Entities.ViewModels;
using CI_Platform.Entities.DataModels;
using CI_Platform.Entities.Constants;
using Microsoft.AspNetCore.Http;

namespace CI_Platform.Services.Service;

public class MissionService : IMissionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMissionSkillService _missionSkillService;
    private readonly IMissionDocumentService _missionDocumentService;
    private readonly IMissionGoalService _missionGoalService;
    private readonly IMissionMediaService _missionMediaService;

    public MissionService(IUnitOfWork unitOfWork, IMissionMediaService missionMediaService, IMissionSkillService missionSkillService, IMissionGoalService missionGoalService, IMissionDocumentService missionDocumentService)
    {
        _unitOfWork = unitOfWork;
        _missionSkillService = missionSkillService;
        _missionDocumentService = missionDocumentService;
        _missionGoalService = missionGoalService;
        _missionMediaService = missionMediaService;
    }
    public static MissionVM ConvertMissionToVM(Mission mission)
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
            Availability = (Availability)mission.Availability,
            TotalSeats = mission.TotalSeats,
            SeatsLeft = (short?)(mission.TotalSeats - (maVM == null ? 0 : maVM.Where(ma => ma.ApprovalStatus == ApprovalStatus.APPROVED).LongCount())),
            MissionType = mission.MissionType ? MissionType.GOAL : MissionType.TIME,
            Status = (bool)mission.Status! ? MissionStatus.ONGOING : MissionStatus.FINISHED,
            MissionRating = mission.MissionRating,
            RegistrationDeadline = mission.RegistrationDeadline,
            MissionThumbnail = GetMissionThumbnail(mission),
            MissionMediaVM = GetMissionMedia(mission),
            MissionDocumentVM = GetMissionDocumentsByMission(mission),
            FavouriteMissionVM = GetFavouriteByMission(mission),
            MissionApplicationVM = maVM,
            MissionGoalVM = GetMissionGoal(mission),
            MissionSkillVM = GetMissionSkill(mission),
            SkillList = mission.MissionSkills.Select(ms => ms.Skill.SkillName).ToList(),
            MissionRatingVM = GetMissionRatingsByMission(mission),
            CommentVM = GetCommentsByMission(mission)
        };
    }
    public static IndexMissionVM ConvertIndexMissionToVM(Mission mission)
    {
        List<MissionApplicationVM> maVM = GetMissionApplication(mission);
        return new IndexMissionVM()
        {
            MissionId = mission.MissionId,
            CityVM = CityService.ConvertCityToVM(mission.MissionCityNavigation),
            CountryVM = CountryService.ConvertCountryToVM(mission.MissionCountryNavigation),
            Title = mission.Title,
            ShortDescription = mission.ShortDescription,
            Description = mission.Description,
            MissionThemeVM = MissionThemeService.ConvertMissionThemeToVM(mission.MissionTheme),
            OrganizationName = mission.OrganizationName,
            StartDate = mission.StartDate,
            EndDate = mission.EndDate,
            IsActive = mission.IsActive,
            Availability = (Availability)mission.Availability,
            TotalSeats = mission.TotalSeats,
            SeatsLeft = (short?)(mission.TotalSeats - (maVM == null ? 0 : maVM.Where(ma => ma.ApprovalStatus == ApprovalStatus.APPROVED).LongCount())),
            MissionType = mission.MissionType ? MissionType.GOAL : MissionType.TIME,
            Status = (bool)mission.Status! ? MissionStatus.ONGOING : MissionStatus.FINISHED,
            MissionRating = mission.MissionRating,
            RegistrationDeadline = mission.RegistrationDeadline,
            MissionThumbnail = GetMissionThumbnail(mission),
            FavouriteMissionVM = GetFavouriteByMission(mission),
            MissionApplicationVM = maVM,
            MissionGoalVM = GetMissionGoal(mission),
            MissionSkillVM = GetMissionSkill(mission)
        };
    }
    public IEnumerable<AdminMissionVM> GetAllAdminMission()
    {
        IEnumerable<Mission> obj = _unitOfWork.Mission.GetAll();
        return obj.Select(mission => {
            return new AdminMissionVM()
            {
                MissionId = mission.MissionId,
                CityName = mission.MissionCityNavigation.CityName,
                CountryName = mission.MissionCountryNavigation.CountryName,
                StartDate = mission.StartDate,
                EndDate = mission.EndDate,
                Title = mission.Title,
                MissionType = mission.MissionType ? MissionType.GOAL : MissionType.TIME,
                IsActive = mission.IsActive,
                CreatedAt = mission.CreatedAt,
            };
        });
    }
    public List<IndexMissionVM> GetAllIndexMissions()
    {
        IEnumerable<Mission> obj = _unitOfWork.Mission.GetAll();
        return obj.Select(mission => {
            return ConvertIndexMissionToVM(mission);
        }).ToList();
    }
    public void UpdateStatus(long id, int value)
    {
        _unitOfWork.Mission.UpdateStatus(id, value);
    }
    public MissionVM GetMissionById(long? id)
    {
        Mission mission = _unitOfWork.Mission.GetFirstOrDefaultWithInclude(mission => mission.MissionId == id);
        if (mission == null) return null!;
        return ConvertMissionToVM(mission);
    }
    public TimeMissionVM GetTimeMissionById(long id)
    {
        Mission mission = _unitOfWork.Mission.GetFirstOrDefaultWithInclude(mission => mission.MissionId == id);
        return new TimeMissionVM()
        {
            MissionId = mission.MissionId,
            Title = mission.Title,
            ShortDescription = mission.ShortDescription,
            Description = mission.Description,
            OrganizationName = mission.OrganizationName,
            OrganizationDetails = mission.OrganizationDetails,
            StartDate = mission.StartDate,
            EndDate = mission.EndDate,
            Availability = (Availability)mission.Availability,
            TotalSeats = mission.TotalSeats,
            RegistrationDeadline = mission.RegistrationDeadline,
            MissionMediaVM = GetMissionMedia(mission),
            MissionDocumentVM = GetMissionDocumentsByMission(mission),
            MissionSkillVM = GetMissionSkill(mission),
            IsActive = mission.IsActive,
            MissionCity = mission.MissionCity,
            MissionCountry = mission.MissionCountry,
            MissionThemeId = mission.MissionThemeId,
        };
    }
    public GoalMissionVM GetGoalMissionById(long id)
    {
        Mission mission = _unitOfWork.Mission.GetFirstOrDefaultWithInclude(mission => mission.MissionId == id);
        MissionGoalVM mg = GetMissionGoal(mission);
        return new GoalMissionVM()
        {
            MissionId = mission.MissionId,
            Title = mission.Title,
            ShortDescription = mission.ShortDescription,
            Description = mission.Description,
            OrganizationName = mission.OrganizationName,
            OrganizationDetails = mission.OrganizationDetails,
            StartDate = mission.StartDate,
            EndDate = mission.EndDate,
            Availability = (Availability)mission.Availability,
            TotalSeats = mission.TotalSeats,
            RegistrationDeadline = mission.RegistrationDeadline,
            MissionMediaVM = GetMissionMedia(mission),
            MissionDocumentVM = GetMissionDocumentsByMission(mission),
            MissionSkillVM = GetMissionSkill(mission),
            MissionGoalId = mg.MissionGoalId,
            GoalObjective = mg.GoalObjective,
            GoalValue = mg.GoalValue,
            IsActive = mission.IsActive,
            MissionCity = mission.MissionCity,
            MissionCountry = mission.MissionCountry,
            MissionThemeId = mission.MissionThemeId,
        };
    }
    public async Task UpdateTimeMission(TimeMissionVM time, List<IFormFile> ImagesInput, List<IFormFile> DocumentsInput, List<string> MissionSkills, List<string> preLoadedImages, List<string> preLoadedDocs, List<string> preLoadedSkills, string wwwRootPath)
    {
        Mission mission = _unitOfWork.Mission.GetFirstOrDefault(m => m.MissionId == time.MissionId);
        mission.Title = time.Title;
        mission.ShortDescription = time.ShortDescription;
        mission.Description = time.Description;
        mission.UpdatedAt = DateTimeOffset.Now;
        mission.TotalSeats = time.TotalSeats;
        mission.RegistrationDeadline = time.RegistrationDeadline;
        mission.StartDate = time.StartDate;
        mission.EndDate = time.EndDate;
        mission.OrganizationName = time.OrganizationName;
        mission.OrganizationDetails = time.OrganizationDetails;
        mission.MissionCity = time.MissionCity;
        mission.MissionCountry = time.MissionCountry;
        mission.Availability = (byte)time.Availability;
        mission.IsActive = time.IsActive;
        mission.MissionThemeId = time.MissionThemeId;
        using (var transaction = await _unitOfWork.BeginTransactionAsync())
        {
            try
            {
                List<Task> tasks = new List<Task>();
                tasks.Add(Task.Run(() => EditMedia(wwwRootPath, ImagesInput, preLoadedImages, mission.MissionId)));
                tasks.Add(Task.Run(() => EditDocuments(wwwRootPath, DocumentsInput, preLoadedDocs, mission.MissionId)));
                if (!MissionSkills.SequenceEqual(preLoadedSkills))
                    tasks.Add(Task.Run(() => EditMissionSkill(MissionSkills, preLoadedSkills, mission.MissionId)));
                await Task.WhenAll(tasks);
                await _unitOfWork.SaveAsync();
                transaction.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
    public async Task UpdateGoalMission(GoalMissionVM goal, List<IFormFile> ImagesInput, List<IFormFile> DocumentsInput, List<string> MissionSkills, List<string> preLoadedImages, List<string> preLoadedDocs, List<string> preLoadedSkills, string wwwRootPath){
        Mission mission = _unitOfWork.Mission.GetFirstOrDefault(m => m.MissionId == goal.MissionId);
        mission.Title = goal.Title;
        mission.ShortDescription = goal.ShortDescription;
        mission.Description = goal.Description;
        mission.UpdatedAt = DateTimeOffset.Now;
        mission.TotalSeats = goal.TotalSeats;
        mission.RegistrationDeadline = goal.RegistrationDeadline;
        mission.StartDate = goal.StartDate;
        mission.EndDate = goal.EndDate;
        mission.OrganizationName = goal.OrganizationName;
        mission.OrganizationDetails = goal.OrganizationDetails;
        mission.MissionCity = goal.MissionCity;
        mission.MissionCountry = goal.MissionCountry;
        mission.Availability = (byte)goal.Availability;
        mission.IsActive = goal.IsActive;
        mission.MissionThemeId = goal.MissionThemeId;
        using (var transaction = await _unitOfWork.BeginTransactionAsync())
        {
            try
            {
                List<Task> tasks = new List<Task>();
                _missionGoalService.UpdateMissionGoal(new MissionGoalVM()
                {
                    GoalObjective = goal.GoalObjective,
                    GoalValue = goal.GoalValue,
                    MissionGoalId = goal.MissionGoalId
                });
                tasks.Add(Task.Run(() => EditMedia(wwwRootPath, ImagesInput, preLoadedImages, mission.MissionId)));
                tasks.Add(Task.Run(() => EditDocuments(wwwRootPath, DocumentsInput, preLoadedDocs, mission.MissionId)));
                if (!MissionSkills.SequenceEqual(preLoadedSkills))
                    tasks.Add(Task.Run(() => EditMissionSkill(MissionSkills, preLoadedSkills, mission.MissionId)));
                await Task.WhenAll(tasks);
                await _unitOfWork.SaveAsync();
                transaction.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
    public List<IndexMissionVM> FilterMissions(int[]? country, int[]? city, int[]? theme, int[]? skill, string? search, int? sort, long? userId) {
        return new MissionFilterService().FilterMissions(country, city, theme, skill, search, sort, userId, GetAllIndexMissions());
    }
    public List<IndexMissionVM> GetRelatedMissions(long id) {
        MissionVM missionVM = GetMissionById(id);
        return GetAllIndexMissions()
        .Where(mission =>
        mission.MissionThemeVM.MissionThemeName == missionVM.MissionThemeVM.MissionThemeName
        && mission.MissionId != missionVM.MissionId).ToList();
    }

    public MissionVM UpdateMissionRating(long id) {
        Mission mission = _unitOfWork.Mission.GetFirstOrDefaultWithInclude(mission => mission.MissionId == id);
        byte average = (byte)Math.Ceiling(mission.MissionRatings.Average(mr => mr.Rating));
        mission.MissionRating = average;
        _unitOfWork.Mission.Update(mission);
        return ConvertMissionToVM(mission);
    }

    public async Task AddTimeMission(TimeMissionVM time, List<IFormFile> ImagesInput, List<IFormFile> DocumentsInput, List<string> MissionSkills, string wwwRootPath)
    {
        Mission mission = new Mission()
        {
            Availability = (byte)time.Availability,
            CreatedAt = DateTimeOffset.Now,
            Description = time.Description,
            Title = time.Title,
            EndDate = time.EndDate,
            Status = true,
            IsActive = time.IsActive,
            StartDate = time.StartDate,
            MissionCity = time.MissionCity,
            MissionCountry = time.MissionCountry,
            RegistrationDeadline = time.RegistrationDeadline,
            TotalSeats = time.TotalSeats,
            OrganizationName = time.OrganizationName,
            OrganizationDetails = time.OrganizationDetails,
            MissionThemeId = time.MissionThemeId,
            ShortDescription = time.ShortDescription,
            MissionType = false,
        };
        using(var transaction = await _unitOfWork.BeginTransactionAsync())
        {
            try {
                _unitOfWork.Mission.Add(mission);
                _unitOfWork.Save();
                long id = mission.MissionId;
                List<Task> tasks = new List<Task>();
                tasks.Add(Task.Run(() => AddMedia(wwwRootPath, ImagesInput, id)));
                tasks.Add(Task.Run(() => AddDocuments(wwwRootPath, DocumentsInput, id)));
                tasks.Add(Task.Run(() => AddMissionSkill(MissionSkills, id)));
                await Task.WhenAll(tasks);
                await _unitOfWork.SaveAsync();
                transaction.Commit();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
    public async Task AddGoalMission(GoalMissionVM goal, List<IFormFile> ImagesInput, List<IFormFile> DocumentsInput, List<string> MissionSkills, string wwwRootPath)
    {
        Mission mission = new Mission()
        {
            Availability = (byte)goal.Availability,
            CreatedAt = DateTimeOffset.Now,
            Description = goal.Description,
            Title = goal.Title,
            EndDate = goal.EndDate,
            Status = true,
            IsActive = goal.IsActive,
            StartDate = goal.StartDate,
            MissionCity = goal.MissionCity,
            MissionCountry = goal.MissionCountry,
            RegistrationDeadline = goal.RegistrationDeadline,
            TotalSeats = goal.TotalSeats,
            OrganizationName = goal.OrganizationName,
            OrganizationDetails = goal.OrganizationDetails,
            MissionThemeId = goal.MissionThemeId,
            ShortDescription = goal.ShortDescription,
            MissionType = true,
        };
        using (var transaction = await _unitOfWork.BeginTransactionAsync())
        {
            try
            {
                _unitOfWork.Mission.Add(mission);
                await _unitOfWork.SaveAsync();
                long id = mission.MissionId;
                _missionGoalService.AddMissionGoal(new MissionGoalVM()
                {
                    MissionId = id,
                    GoalObjective = goal.GoalObjective, 
                    GoalValue = goal.GoalValue,
                });
                List<Task> tasks = new List<Task>();
                tasks.Add(Task.Run(() => AddMedia(wwwRootPath, ImagesInput, id)));
                tasks.Add(Task.Run(() => AddDocuments(wwwRootPath, DocumentsInput, id)));
                tasks.Add(Task.Run(() => AddMissionSkill(MissionSkills, id)));
                await Task.WhenAll(tasks);
                await _unitOfWork.SaveAsync();
                transaction.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
    public List<AdminMissionVM> Search(string? query)
    {
        IEnumerable<AdminMissionVM> missions = GetAllAdminMission();

        return string.IsNullOrEmpty(query) ? missions.ToList()
            : missions
                .Where(m => m.Title.ToLower().Contains(query.ToLower()) ||
                    m.CityName.ToLower().Contains(query.ToLower()) ||
                    m.CountryName.ToLower().Contains(query.ToLower()) 
                ).ToList();
    }

    public List<CountryVM> GetCountriesByMissions(List<IndexMissionVM> missionVM){
        List<CountryVM> allCountries = new CountryService(_unitOfWork).GetAll();
        List<CountryVM> countryVM = new();
        missionVM.ForEach(mission => {
            CountryVM co = allCountries.Where(all => all.CountryName == mission.CountryVM.CountryName).First();
            if(!countryVM.Contains(co))
                countryVM.Add(co);
        });
        return countryVM;
    }
    public List<CityVM> GetCitiesByMissions(List<IndexMissionVM> missionVM){
        List<CityVM> allCities = new CityService(_unitOfWork).GetAll();
        List<CityVM> cityVM = new();
        missionVM.ForEach(mission => {
            CityVM co = allCities.Where(all => all.CityName == mission.CityVM.CityName).First();
            if(!cityVM.Contains(co))
                cityVM.Add(co);
        });
        return cityVM;
    }

    internal static List<MissionRatingVM> GetMissionRatingsByMission(Mission mission)
    {
        return mission.MissionRatings.LongCount() > 0 ? mission.MissionRatings.Select(r =>
            MissionRatingService.ConvertMissionRatingToVM(r)
        ).ToList() : new();
    }
    internal static List<CommentVM> GetCommentsByMission(Mission mission)
    {
        return mission.Comments.Select(c => CommentService.ConvertCommentToVM(c))
            .Where(c => c.ApprovalStatus == ApprovalStatus.APPROVED || c.ApprovalStatus == ApprovalStatus.PENDING)
            .OrderByDescending(c => c.CreatedAt).ToList();
    }
    internal static string GetMissionThumbnail(Mission mission)
    {
        MissionMedium mm = mission.MissionMedia?.FirstOrDefault()!;
        return mm != null ? mm.MediaPath + mm.MediaName + mm.MediaType : "";
    }
    internal static List<FavouriteMissionVM> GetFavouriteByMission(Mission mission)
    {
        return mission.FavouriteMissions.LongCount() > 0 ? mission.FavouriteMissions.Select(fm =>
            FavouriteMissionService.ConvertFavouriteMissionToVM(fm)    
        ).ToList() : new();
    }
    internal static MissionGoalVM GetMissionGoal(Mission mission){
        return mission.MissionGoals.LongCount() > 0 ? MissionGoalService.ConvertMissionGoalToVM(mission.MissionGoals.First()) : new();
    }
    internal static List<MissionDocumentVM> GetMissionDocumentsByMission(Mission mission)
    {
        return mission.MissionDocuments.LongCount() > 0 ? mission.MissionDocuments.Select(md =>
        MissionDocumentService.ConvertMissionDocumentToVM(md)).ToList() : new();
    }
    internal static List<MissionApplicationVM>? GetMissionApplication(Mission mission){
        return mission?.MissionApplications?.Select(ma =>
            MissionApplicationService.ConvertMissionApplicationToVM(ma)
            ).ToList();
    }
    internal static List<MissionSkillVM> GetMissionSkill(Mission mission){
        return mission.MissionSkills.LongCount() > 0 ? mission.MissionSkills.Select(ms =>
            MissionSkillService.ConvertMissionSkillToVM(ms)    
        ).ToList() : new();
    }
    internal static List<MissionMediaVM> GetMissionMedia(Mission mission) {
        return mission.MissionMedia.Select(mm => MissionMediaService.ConvertMissionMediaToVM(mm)).ToList();
    }

    private void AddMedia(string wwwRootPath, List<IFormFile> images, long missionId)
    {
        _missionMediaService.AddMissionMedia(wwwRootPath, images, missionId);
    }
    private void AddDocuments(string wwwRootPath, List<IFormFile> docs, long missionId)
    {
        _missionDocumentService.AddMissionDocuments(wwwRootPath, docs, missionId);
    }
    private void AddMissionSkill(List<string> skills, long missionId)
    {
        _missionSkillService.AddMissionSkill(skills, missionId);
    }
    private void EditMedia(string wwwRootPath, List<IFormFile> images, List<string> preLoaded, long missionId)
    {
        _missionMediaService.EditMissionMedia(wwwRootPath, images, missionId, preLoaded);
    }
    private void EditDocuments(string wwwRootPath, List<IFormFile> docs, List<string> preLoaded, long missionId)
    {
        _missionDocumentService.EditMissionDocuments(wwwRootPath, docs, missionId, preLoaded);
    }
    private void EditMissionSkill(List<string> skills, List<string> preLoaded, long missionId)
    {
        _missionSkillService.EditMissionSkill(skills, preLoaded, missionId);
    }
}