using CI_Platform.DataAccess.Repository;
using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;
using System.ComponentModel.DataAnnotations;

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
        return obj == null ? new() : obj.Select(cms => ConvertCmsToVM(cms)).ToList();
    }

    public void SaveCmsPage(CmsPageVM cms)
    {
        _unitOfWork.CmsPage.Add(new CmsPage()
        {
            CreatedAt = DateTimeOffset.Now,
            Title = cms.Title,
            Description = cms.Description,
            Slug = cms.Slug,
            Status = true
        });
    }

    public void UpdateCmsPage(CmsPageVM cms)
    {
        CmsPage cmsPage = _unitOfWork.CmsPage.GetFirstOrDefault(c => c.CmsPageId == cms.CmsPageId);

        cmsPage.Title = cms.Title;
        cmsPage.Description = cms.Description;
        cmsPage.Slug = cms.Slug;
        cmsPage.UpdatedAt = DateTimeOffset.Now;
    }
    public void DeleteCmsPage(long id)
    {
        CmsPage cmsPage = _unitOfWork.CmsPage.GetFirstOrDefault(c => c.CmsPageId == id);
        cmsPage.Status = false;
        cmsPage.DeletedAt = DateTimeOffset.Now;
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
        };
    }
}

//public class UniqueSlugAttribute : ValidationAttribute
//{
//    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
//    {
//        var slug = (string)value;
//        var pages = new UnitOfWork().CmsPage.GetFirstOrDefault(u => u.Email == email);

//        if (pages != null)
//        {
//            return new ValidationResult(ErrorMessage);
//        }

//        return ValidationResult.Success;
//    }
//}