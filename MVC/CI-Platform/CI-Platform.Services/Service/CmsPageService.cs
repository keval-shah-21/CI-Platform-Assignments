using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;
using System.Linq.Expressions;

namespace CI_Platform.Services.Service;

public class CmsPageService : ICmsPageService
{
    private readonly IUnitOfWork _unitOfWork;

    public CmsPageService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public List<CmsPageVM> GetAll()
    {
        IEnumerable<CmsPage> obj = _unitOfWork.CmsPage.GetAll();
        return obj.Select(cms => ConvertCmsToVM(cms)).ToList();
    }

    public CmsPageVM GetCmsPageById(long id)
    {
        return ConvertCmsToVM(_unitOfWork.CmsPage.GetFirstOrDefault(cms => cms.CmsPageId == id));
    }

    public void SaveCmsPage(CmsPageVM cms)
    {
        _unitOfWork.CmsPage.Add(new CmsPage()
        {
            CreatedAt = DateTimeOffset.Now,
            Title = cms.Title,
            Description = cms.Description,
            Slug = cms.Slug,
            Status = cms.Status
        });
    }

    public void UpdateCmsPage(CmsPageVM cms)
    {
        CmsPage cmsPage = _unitOfWork.CmsPage.GetFirstOrDefault(c => c.CmsPageId == cms.CmsPageId);

        cmsPage.Title = cms.Title;
        cmsPage.Description = cms.Description;
        cmsPage.Slug = cms.Slug;
        cmsPage.Status = cms.Status;
        cmsPage.UpdatedAt = DateTimeOffset.Now;
    }
    public void DeleteCmsPage(long id)
    {
        _unitOfWork.CmsPage.RemoveById(id);
    }

    public void DeactivateCmsPage(long id)
    {
        CmsPage cmsPage = _unitOfWork.CmsPage.GetFirstOrDefault(c => c.CmsPageId == id);
        cmsPage.Status = false;
        cmsPage.UpdatedAt = DateTimeOffset.Now;
    }
    public static CmsPageVM ConvertCmsToVM(CmsPage cms)
    {
        return new CmsPageVM()
        {
            CmsPageId = cms.CmsPageId,
            Description = cms.Description,
            Slug = cms.Slug,
            Title = cms.Title,
            Status = cms.Status,
            CreatedAt = cms.CreatedAt,
            UpdatedAt = cms.UpdatedAt
        };
    }
    public List<CmsPageVM> Search(string? query)
    {
        IEnumerable<CmsPage> cmsList = _unitOfWork.CmsPage.GetAll();

        return string.IsNullOrEmpty(query) ? cmsList.Select(cms => ConvertCmsToVM(cms)).ToList()
            : cmsList
                .Where(cms => cms.Title.ToLower().Contains(query.ToLower()))
                .Select(cms => ConvertCmsToVM(cms))
                .ToList();
    }
    public bool IsSlugUnique(string slug, long? id)
    {
        Expression<Func<CmsPage, bool>> filter;
        if (id != null)
            filter = cms => cms.Slug == slug && cms.CmsPageId != id;
        else
            filter = cms => cms.Slug == slug;
        return _unitOfWork.CmsPage.GetFirstOrDefault(filter) == null ? true : false;
    }
}