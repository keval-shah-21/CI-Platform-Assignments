using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;

namespace CI_Platform.Services.Service
{
    public class MissionFilterService : IMissionFilterService
    {
        public List<MissionVM> FilterData(int[]? country, int[]? city, int[]? theme, int[]? skill, string? search, int? sort, long? userId, IUnitOfWork _unitOfWork)
        {
            List<MissionVM> missionVM = new MissionService(_unitOfWork).GetAllMissions();
            List<CityVM> cityVM = new CityService(_unitOfWork).GetAll();
            List<CountryVM> countryVM = new CountryService(_unitOfWork).GetAll();
            List<MissionThemeVM> missionThemeVM = new MissionThemeService(_unitOfWork).GetAll();
            List<MissionSkillVM> missionSkillVM = new MissionSkillService(_unitOfWork).GetAll();
            List<SkillVM> skillVM = new SkillService(_unitOfWork).GetAll();

            if (!string.IsNullOrEmpty(search))
            {
                missionVM = missionVM.Where(mission => mission.Title.ToLower().Contains(search.ToLower())).ToList();
            }

            if (country?.Count() > 0)
            {
                missionVM = missionVM.Where(mission => country.Any(c => c == mission.CountryVM.CountryId)).ToList();
            }
            if (city?.Count() > 0)
            {
                missionVM = missionVM.Where(mission => city.Any(c => c == mission.CityVM.CityId)).ToList();
            }

            if (theme?.Count() > 0)
            {
                missionVM = missionVM.Where(mission => theme.Any(t => t == mission.MissionThemeVM.MissionThemeId)).ToList();
            }

            if (skill?.Count() > 0)
            {
                missionVM = missionVM.Where(mission => skill.Any(s =>
                mission.MissionSkillVM.Any(ms => ms.SkillId == s)
                )).ToList();
            }

            if (sort == 1)
            {
                missionVM = missionVM.OrderBy(m => m.SeatsLeft == null).ThenByDescending(mission => mission.StartDate).ToList();
            }
            else if (sort == 2)
            {
                missionVM = missionVM.OrderBy(m => m.SeatsLeft == null).ThenBy(mission => mission.StartDate).ToList();
            }
            else if (sort == 3)
            {
                missionVM = missionVM.OrderBy(m => m.SeatsLeft == null).ThenByDescending(mission => mission.SeatsLeft).ToList();
            }
            else if (sort == 4)
            {
                missionVM = missionVM.OrderBy(m => m.SeatsLeft == null).ThenBy(mission => mission.SeatsLeft).ToList();
            }
            else if (sort == 5)
            {
                missionVM = missionVM.Where(mission => {
                    if (mission?.FavouriteMissionVM?.LongCount() > 0)
                        return mission.FavouriteMissionVM.Any(fm => fm.UserId == userId);
                    else
                        return false;
                }).ToList();
            }
            else if (sort == 6)
            {
                missionVM = missionVM.OrderBy(m => m.SeatsLeft == null).ThenByDescending(mission => mission.RegistrationDeadline).ToList();
            }
            return missionVM;
        }

    }
}
