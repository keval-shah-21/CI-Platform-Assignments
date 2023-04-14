using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CI_PlatformWeb.Areas.Admin.Controllers;

[Area("Admin")]
public class BannerController : Controller
{
    private readonly IUnitOfService _unitOfService;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public BannerController(IUnitOfService unitOfService, IWebHostEnvironment webHostEnvironment)
    {
        _unitOfService = unitOfService;
        _webHostEnvironment = webHostEnvironment;
    }
    public IActionResult LoadBannerPage()
    {
        try
        {
            return PartialView("_Banner", _unitOfService.Banner.GetAll().OrderBy(b => b.SortOrder).ToList());
        }
        catch (Exception e)
        {
            Console.WriteLine("Error loading banner: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    public IActionResult AddBanner()
    {
        try
        {
            return PartialView("_AddBanner");
        }
        catch (Exception e)
        {
            Console.WriteLine("Error loading add banner: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }
    [HttpPost]
    public IActionResult AddBanner(BannerVM banner, IFormFile bannerImage)
    {
        try
        {
            if (bannerImage != null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(wwwRootPath, @"images\banner");
                string extension = Path.GetExtension(bannerImage.FileName);
                using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    bannerImage.CopyTo(fileStreams);
                }
                banner.MediaPath = @"\images\banner\";
                banner.MediaName = fileName;
                banner.MediaType = extension;
            }
            _unitOfService.Banner.AddBanner(banner);
            _unitOfService.Save();
            return PartialView("_Banner", _unitOfService.Banner.GetAll().OrderBy(b => b.SortOrder).ToList());
        }
        catch (Exception e)
        {
            Console.WriteLine("Error saving banner: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    public IActionResult EditBanner(long id)
    {
        try
        {
            return PartialView("_EditBanner", _unitOfService.Banner.GetById(id));
        }
        catch (Exception e)
        {
            Console.WriteLine("Error loading edit banner: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }
    [HttpPut]
    public IActionResult EditBanner(BannerVM banner, IFormFile bannerImage)
    {
        try
        {
            if(bannerImage.FileName != banner.MediaName + banner.MediaType)
            {

                string wwwRootPath = _webHostEnvironment.WebRootPath;
                System.IO.File.Delete(Path.Combine(wwwRootPath, banner.MediaPath.TrimStart('\\'), banner.MediaName + banner.MediaType));

                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(wwwRootPath, @"images\banner");
                string extension = Path.GetExtension(bannerImage.FileName);
                using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    bannerImage.CopyTo(fileStreams);
                }
                banner.MediaPath = @"\images\banner\";
                banner.MediaName = fileName;
                banner.MediaType = extension;
            }
            _unitOfService.Banner.UpdateBanner(banner);
            _unitOfService.Save();
            return PartialView("_Banner", _unitOfService.Banner.GetAll().OrderBy(b => b.SortOrder).ToList());
        }
        catch (Exception e)
        {
            Console.WriteLine("Error updating banner: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpDelete]
    public IActionResult DeleteBanner(long id)
    {
        try
        {
            _unitOfService.Banner.RemoveById(id);
            return PartialView("_Banner", _unitOfService.Banner.GetAll().OrderBy(b => b.SortOrder).ToList());
        }
        catch (Exception e)
        {
            Console.WriteLine("Error loading add banner: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }
}
