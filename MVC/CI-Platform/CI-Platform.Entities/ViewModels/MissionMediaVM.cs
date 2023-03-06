namespace CI_Platform.Entities.ViewModels;

public class MissionMediaVM
{
    public long MissionMediaId { get; set; }

    public long MissionId { get; set; }

    public string? MediaName { get; set; }

    public string? MediaType { get; set; }

    public string? MediaPath { get; set; }

    public bool? Default { get; set; }
}
