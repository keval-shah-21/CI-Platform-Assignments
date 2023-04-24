using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;
using Microsoft.AspNetCore.Http;

namespace CI_Platform.Services.Service;

public class MissionMediaService: IMissionMediaService
{
    private readonly IUnitOfWork _unitOfWork;

    public MissionMediaService(IUnitOfWork unitOfWork)
    {   
        _unitOfWork = unitOfWork;
    }
    public List<MissionMediaVM> GetAll()
    {
        IEnumerable<MissionMedium> obj = _unitOfWork.MissionMedia.GetAll();
        if(obj == null) return null!;
        return obj.Select(mm => ConvertMissionMediaToVM(mm)
        ).ToList();
    }
    public void AddMissionMedia(string wwwRootPath, List<IFormFile> images, long missionId)
    {
        var medias = images.Select(image =>
        {
            string fileName = Guid.NewGuid().ToString();
            var uploads = Path.Combine(wwwRootPath, @"images\mission");
            string extension = Path.GetExtension(image.FileName);
            using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
            {
                image.CopyTo(fileStreams);
            }
            return new MissionMedium()
            {
                MediaPath = @"\images\mission\",
                MediaName = fileName,
                MediaType = extension,
                MissionId = missionId,
            };
        });
        _unitOfWork.MissionMedia.AddRange(medias);
    }
    public void EditMissionMedia(string wwwRootPath, List<IFormFile> images, long missionId, List<string>? preLoaded)
    {
        preLoaded?.ForEach(pre =>
        {
            if (!images.Any(ss => pre == ss.FileName))
            {
                System.IO.File.Delete(Path.Combine(wwwRootPath, @"images\mission", pre));
                string name = pre.Split(".")[0];
                _unitOfWork.MissionMedia.Remove(_unitOfWork.MissionMedia.GetFirstOrDefault(mm => mm.MissionId == missionId && 
                mm.MediaName == name));
            }
        });
        List<IFormFile> newImages = new();
        images.ForEach(image =>
        {
            if (!preLoaded.Contains(image.FileName))
            {
                newImages.Add(image);
            }
        });
        if (newImages.Count > 0)
            AddMissionMedia(wwwRootPath, newImages, missionId);
    }
    public static MissionMediaVM ConvertMissionMediaToVM(MissionMedium mm)
    {
        return new MissionMediaVM()
        {
            Default = mm.Default,
            MediaName = mm.MediaName,
            MediaPath = mm.MediaPath,
            MediaType = mm.MediaType,
            MissionId = mm.MissionId,
            MissionMediaId = mm.MissionMediaId
        };
    }
}
