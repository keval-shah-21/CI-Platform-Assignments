namespace CI_Platform.Entities.ViewModels;

public class CityVM
{
    public long CityId{get; set;}

    public string CityName{get; set;} = string.Empty;

    public short CountryId { get; set; }
}
