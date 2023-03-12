using CI_Platform.Services.Service.Interface;
using CI_Platform.DataAccess.Repository.Interface;

using CI_Platform.Entities.ViewModels;
using CI_Platform.Entities.DataModels;

namespace CI_Platform.Services.Service;

public class MissionService : IMissionService
{
    private readonly IUnitOfWork _unitOfWork;

    public MissionService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public List<MissionVM> GetAll()
    {
        IEnumerable<Mission> obj = _unitOfWork.Mission.GetAll();
        if (obj == null) return null!;
        // return obj.Select(mission => new MissionVM(){
        //     MissionId = mission.MissionId,
        //     MissionCity = mission.MissionCity,
        //     MissionCountry = mission.MissionCountry,
        //     Title = mission.Title,
        //     ShortDescription = mission.ShortDescription,
        //     MissionThemeId = mission.MissionThemeId,
        //     OrganizationName = mission.OrganizationName,
        //     StartDate = mission.StartDate,
        //     EndDate = mission.EndDate,
        //     TotalSeats = mission.TotalSeats,
        //     MissionType = mission.MissionType,
        //     MissionRating = mission.MissionRating,
        //     RegistrationDeadline = mission.RegistrationDeadline
        // }).ToList();
        return null;
    }

    public List<MissionVM> GetAllIndexMission()
    {
        IEnumerable<Mission> obj = _unitOfWork.Mission.GetAllMissions();
        if (obj == null) return null!;
        List<MissionVM> missionVMs = obj.Select(mission =>{
            List<MissionApplicationVM> maVM = GetIndexMissionApplication(mission);
            return new MissionVM(){
                MissionId = mission.MissionId,
                MissionCityId = mission.MissionCity,
                MissionCity = mission.MissionCityNavigation.CityName,
                MissionCountryId = mission.MissionCountry,
                MissionCountry = mission.MissionCountryNavigation.CountryName,
                Title = mission.Title,
                ShortDescription = mission.ShortDescription,
                MissionThemeId = mission.MissionThemeId,
                MissionThemeName = mission.MissionTheme.MissionThemeName!,
                OrganizationName = mission.OrganizationName,
                StartDate = mission.StartDate,
                EndDate = mission.EndDate,
                TotalSeats = mission.TotalSeats,
                SeatsLeft = (short?)(mission.TotalSeats - (maVM == null ? 0 : maVM.Where(ma => ma.ApprovalStatus == 1).LongCount())),
                MissionType = mission.MissionType,
                MissionRating = mission.MissionRating,
                RegistrationDeadline = mission.RegistrationDeadline,
                MissionMedia = GetIndexMissionMedia(mission),
                FavouriteMissionVM = GetIndexMissionFavourite(mission),
                MissionApplicationVM = GetIndexMissionApplication(mission),
                MissionGoalVM = GetIndexMissionGoal(mission),
                MissionSkillVM = GetIndexMissionSkill(mission)
            };
        }).ToList();
        return missionVMs;
    }

    internal string GetIndexMissionMedia(Mission mission)
    {
        MissionMedium mm = mission.MissionMedia?.FirstOrDefault(mm => mm.Default == true)!;
        return mm != null ? mm.MediaPath + mm.MediaName + mm.MediaType : "";
    }

    internal List<FavouriteMissionVM> GetIndexMissionFavourite(Mission mission)
    {
        return mission.FavouriteMissions.LongCount() > 0 ? mission.FavouriteMissions.Select(fm =>
            new FavouriteMissionVM()
            {
                FavouriteMissionId = fm.FavouriteMissionId,
                MissionId = fm.MissionId,
                UserId = fm.UserId
            }).ToList() : null!;
    }

    internal MissionGoalVM GetIndexMissionGoal(Mission mission){
        return mission.MissionGoals.LongCount() > 0 ? new MissionGoalVM()
            {
                GoalAchieved = mission.MissionGoals.First().GoalAchieved,
                GoalObjective = mission.MissionGoals.First().GoalObjective,
                GoalValue = mission.MissionGoals.First().GoalValue,
                MissionGoalId = mission.MissionGoals.First().MissionGoalId,
                MissionId = mission.MissionGoals.First().MissionId
            } : null!;
    }

    internal List<MissionApplicationVM> GetIndexMissionApplication(Mission mission){
        return mission.MissionApplications.LongCount() > 0 ? mission.MissionApplications.Select(ma =>
            new MissionApplicationVM()
            {
                AppliedAt = ma.AppliedAt,
                ApprovalStatus = ma.ApprovalStatus,
                MissionApplicationId = ma.MissionApplicationId,
                MissionId = ma.MissionId,
                UserId = ma.UserId
            }).ToList() : null!;
    }

    internal List<MissionSkillVM> GetIndexMissionSkill(Mission mission){
        return mission.MissionSkills.LongCount() > 0 ? mission.MissionSkills.Select(ms =>
            new MissionSkillVM()
            {
                MissionId = ms.MissionId,
                MissionSkillId = ms.MissionSkillId,
                SkillId = ms.SkillId
            }).ToList() : null!;
    }

    public IndexMissionVM FilterData(int? country, int[]? city, int[]? theme, int[]? skill, string? search, int? sort, long? userId){
        List<MissionVM> missionVM = GetAllIndexMission();
        List<CityVM> cityVM = GetCitiesByMission(missionVM);
        List<CountryVM> countryVM = GetCountriesByMission(missionVM);
        List<MissionThemeVM> missionThemeVM = new MissionThemeService(_unitOfWork).GetAll();
        List<MissionSkillVM> missionSkillVM = new MissionSkillService(_unitOfWork).GetAll();
        List<SkillVM> skillVM = new SkillService(_unitOfWork).GetAll();

        if(!string.IsNullOrEmpty(search) ){
            missionVM = missionVM.Where(mission => mission.Title.ToLower().Contains(search.ToLower())).ToList();
        }

        if(country != 0){
            missionVM = missionVM.Where(mission => mission.MissionCountryId == country).ToList();
        }

        if(city?.Count() > 0){
            foreach (var item in city)
            {
                missionVM = missionVM.Where(mission => mission.MissionCityId == item).ToList();
            }
        }

        if(theme?.Count() > 0){
            foreach (var item in theme)
            {
                missionVM = missionVM.Where(mission => mission.MissionThemeId == item).ToList();
            }
        }

        if(skill?.Count() > 0){

            foreach (var item in skill)
            {
                missionVM = missionVM.Where(mission => 
                    mission.MissionSkillVM.Any(ms => ms.SkillId == item)
                ).ToList();
            }
        }    

        if(sort == 1){
            missionVM = missionVM.OrderBy(mission => mission.StartDate).ToList();
        }
        else if(sort == 2){
            missionVM = missionVM.OrderByDescending(mission => mission.StartDate).ToList();
        }
        else if(sort == 3){
            missionVM = missionVM.OrderByDescending(mission => mission.SeatsLeft).ToList();
        }
        else if(sort == 4){
            missionVM = missionVM.OrderBy(mission => mission.SeatsLeft).ToList();
        }
        else if(sort == 5){
            missionVM = missionVM.Where(mission => 
                mission.FavouriteMissionVM.Any(fm => fm.UserId == userId)
            ).ToList();
        }
        else if(sort == 6){
            missionVM = missionVM.OrderBy(mission => mission.RegistrationDeadline).ToList();
        }

        return new IndexMissionVM(){
            missionVM = missionVM,
            missionThemeVM = missionThemeVM,
            skillVM = skillVM,
            cityVM = cityVM,
            countryVM = countryVM
        };
    }

    public List<CountryVM> GetCountriesByMission(List<MissionVM> missionVM){
        List<CountryVM> allCountries = new CountryService(_unitOfWork).GetAll();
        List<CountryVM> countryVM = new();
        missionVM.ForEach(mission => {
            CountryVM co = allCountries.Where(all => all.CountryName == mission.MissionCountry).First();
            if(!countryVM.Contains(co))
                countryVM.Add(co);
        });
        return countryVM;
    }

    public List<CityVM> GetCitiesByMission(List<MissionVM> missionVM){
        List<CityVM> allCities = new CityService(_unitOfWork).GetAll();
        List<CityVM> cityVM = new();
        missionVM.ForEach(mission => {
            CityVM co = allCities.Where(all => all.CityName == mission.MissionCity).First();
            if(!cityVM.Contains(co))
                cityVM.Add(co);
        });
        return cityVM;
    }
}