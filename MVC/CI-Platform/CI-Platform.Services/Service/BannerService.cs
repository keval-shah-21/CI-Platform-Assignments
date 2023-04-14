using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;

namespace CI_Platform.Services.Service;

public class BannerService : IBannerService
{
    private readonly IUnitOfWork _unitOfWork;

    public BannerService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public List<BannerVM> GetAll()
    {
        IEnumerable<Banner> banners = _unitOfWork.Banner.GetAll();
        return banners.Select(b => ConvertBannerToVM(b)).ToList();
    }
    public BannerVM GetById(long id)
    {
        return ConvertBannerToVM(_unitOfWork.Banner.GetFirstOrDefault(b => b.BannerId == id));
    }
    public void AddBanner(BannerVM banner)
    {
        _unitOfWork.Banner.Add(new Banner()
        {
            MediaName = banner.MediaName,
            Title = banner.Title,
            Description = banner.Description,
            MediaPath = banner.MediaPath,
            MediaType = banner.MediaType,
            CreatedAt = DateTimeOffset.Now,
            SortOrder = banner.SortOrder,
        });
    }
    public void UpdateBanner(BannerVM bannerVM)
    {
        Banner banner = _unitOfWork.Banner.GetFirstOrDefault(b => b.BannerId == bannerVM.BannerId);
        banner.Title = bannerVM.Title;
        banner.Description = bannerVM.Description;
        banner.MediaName = bannerVM.MediaName;
        banner.MediaType = bannerVM.MediaType;
        banner.MediaPath = bannerVM.MediaPath;
        banner.SortOrder = bannerVM.SortOrder;
        banner.UpdatedAt = DateTimeOffset.Now;
    }

    public void RemoveById(long id)
    {
        _unitOfWork.Banner.RemoveById(id);
    }
    public List<BannerVM> Search(string? query)
    {
        IEnumerable<Banner> banners = _unitOfWork.Banner.GetAll();

        return string.IsNullOrEmpty(query) ? banners.Select(b => ConvertBannerToVM(b)).ToList()
            : banners
                .Where(b => b.Title.ToLower().Contains(query.ToLower()))
                .Select(b => ConvertBannerToVM(b))
                .ToList();
    }
    public static BannerVM ConvertBannerToVM(Banner banner) {
        return new BannerVM()
        {
            BannerId = banner.BannerId,
            MediaName = banner.MediaName,  
            MediaPath = banner.MediaPath,   
            MediaType = banner.MediaType,
            Title = banner.Title,
            Description = banner.Description,
            CreatedAt = banner.CreatedAt,
            SortOrder = banner.SortOrder,
            UpdatedAt = banner.UpdatedAt
        };
    }
}
