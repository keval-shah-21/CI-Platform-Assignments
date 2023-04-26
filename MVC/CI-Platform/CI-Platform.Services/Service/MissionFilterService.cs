using CI_Platform.Entities.Constants;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;

namespace CI_Platform.Services.Service
{
    public class MissionFilterService : IMissionFilterService
    {
        public List<IndexMissionVM> FilterMissions(int[]? country, int[]? city, int[]? theme, int[]? skill, string? search, int? sort, long? userId, IEnumerable<IndexMissionVM> missionVM)
        {
            if (!string.IsNullOrEmpty(search))
            {
                missionVM = missionVM.Where(mission => mission.Title.ToLower().Contains(search.ToLower()));
            }

            if (country?.Count() > 0)
            {
                missionVM = missionVM.Where(mission => country.Any(c => c == mission.CountryVM.CountryId));
            }
            if (city?.Count() > 0)
            {
                missionVM = missionVM.Where(mission => city.Any(c => c == mission.CityVM.CityId));
            }

            if (theme?.Count() > 0)
            {
                missionVM = missionVM.Where(mission => theme.Any(t => t == mission.MissionThemeVM.MissionThemeId));
            }

            if (skill?.Count() > 0)
            {
                missionVM = missionVM.Where(mission => skill.Any(s =>
                mission.MissionSkillVM.Any(ms => ms.SkillId == s)
                ));
            }
            if (sort == 0 || sort == null)
            {
                missionVM = missionVM.OrderByDescending(m => m.CreatedAt);
            }
            if (sort == 1)
            {
                missionVM = missionVM.OrderBy(mission => mission.StartDate);
            }
            else if (sort == 2)
            {
                missionVM = missionVM.OrderByDescending(mission => mission.StartDate);
            }
            else if (sort == 3)
            {
                missionVM = missionVM.OrderBy(m => m.SeatsLeft == null || m.Status == MissionStatus.FINISHED).ThenByDescending(mission => mission.SeatsLeft);
            }
            else if (sort == 4)
            {
                missionVM = missionVM.OrderBy(m => m.SeatsLeft == null || m.Status == MissionStatus.FINISHED).ThenBy(mission => mission.SeatsLeft);
            }
            else if (sort == 5)
            {
                missionVM = missionVM.Where(mission =>
                {
                    if (mission?.FavouriteMissionVM?.LongCount() > 0)
                        return mission.FavouriteMissionVM.Any(fm => fm.UserId == userId);
                    else
                        return false;
                });
            }
            else if (sort == 6)
            {
                missionVM = missionVM.OrderBy(m => m.RegistrationDeadline == null).ThenByDescending(mission => mission.RegistrationDeadline);
            }
            else if (sort == 7)
            {
                missionVM = missionVM
                    .GroupBy(m => m.MissionThemeVM.MissionThemeId)
                    .OrderByDescending(g => g.Count())
                    .SelectMany(g => g);
            }
            else if (sort == 8)
            {
                missionVM = missionVM.OrderByDescending(m => m.MissionRating);
            }
            else if (sort == 9)
            {
                missionVM = missionVM.OrderByDescending(m => m.FavouriteMissionVM.Count());
            }
            else if (sort == 10)
            {
                Random rand = new Random();
                missionVM = missionVM.OrderBy(m => rand.Next());
            }
            return missionVM.ToList();
        }

    }
}
