using CI_Platform.Entities.ViewModels;

namespace CI_Platform.Services.Service.Interface;

public interface IBannerService
{
    List<BannerVM> GetAll();
    BannerVM GetById(long id);
    void AddBanner(BannerVM banner);
    void UpdateBanner(BannerVM bannerVM);
    void RemoveById(long id);
    List<BannerVM> Search(string? query);
}
