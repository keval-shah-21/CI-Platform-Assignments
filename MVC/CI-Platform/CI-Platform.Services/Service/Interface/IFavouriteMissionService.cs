using CI_Platform.Entities.ViewModels;

namespace CI_Platform.Services.Service.Interface;

public interface IFavouriteMissionService
{
    List<FavouriteMissionVM> GetAll();

    void AddToFavourite(long missionId, long userId);

    void RemoveFromFavourite(long missionId, long userId);
}
