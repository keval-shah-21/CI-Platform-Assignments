namespace CI_Platform.Entities.ViewModels;

public class IndexMissionVM
{
    public List<MissionVM>? missionVM {get; set;}

    public HashSet<CityVM>? cityVM {get; set;}

    public HashSet<CountryVM>? countryVM {get; set;}

    public List<MissionApplicationVM>? missionApplicationVM {get; set;}

    public List<FavouriteMissionVM>? favouriteMissionVM {get; set;}

    public List<MissionSkillVM>? missionSkillVM {get; set;}

    public List<MissionThemeVM>? missionThemeVM {get; set;}

    public List<MissionGoalVM>? missionGoalVM {get; set;}

    public List<MissionMediaVM>? missionMediaVM {get; set;}
}
