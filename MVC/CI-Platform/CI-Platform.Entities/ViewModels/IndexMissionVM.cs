namespace CI_Platform.Entities.ViewModels;

public class IndexMissionVM
{
    public List<MissionVM>? missionVM {get; set;}

    public HashSet<CityVM>? cityVM {get; set;}

    public HashSet<CountryVM>? countryVM {get; set;}

    public List<SkillVM>? skillVM {get; set;}

    public List<MissionThemeVM>? missionThemeVM {get; set;}
}
