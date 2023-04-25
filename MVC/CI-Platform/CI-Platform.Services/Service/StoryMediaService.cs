using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;
using Microsoft.AspNetCore.Http;

namespace CI_Platform.Services.Service
{
    public class StoryMediaService : IStoryMediaService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StoryMediaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SaveAllStoryMedia(List<StoryMediaVM> storyMediaVMs)
        {
            List<StoryMedium> SMVMs = storyMediaVMs.Select(sm =>
                new StoryMedium()
                {
                    StoryId = sm.StoryId,
                    MediaName = sm.MediaName,
                    MediaPath = sm.MediaPath,
                    MediaType = sm.MediaType,
                    CreatedAt = DateTimeOffset.Now,
                }
            ).ToList();
            _unitOfWork.StoryMedia.AddRange(SMVMs);
        }
        public void RemoveStoryMedia(long storyId, string mediaName)
        {
            StoryMedium sm = _unitOfWork.StoryMedia.GetFirstOrDefault(s => s.StoryId == storyId && s.MediaName == mediaName);
            _unitOfWork.StoryMedia.Remove(sm);
        }
        public void RemoveAllStoryMediaByStoryId(long storyId)
        {
            _unitOfWork.StoryMedia.RemoveRange(_unitOfWork.StoryMedia.GetAll().Where(sm => sm.StoryId == storyId));
        }
        public void RemoveMediaFromFolder(long storyId, string wwwRootPath)
        {
            IEnumerable<StoryMedium> sm = _unitOfWork.StoryMedia.GetAll().Where(sm => sm.StoryId == storyId);
            foreach (var s in sm)
            {
                string path = Path.Combine(wwwRootPath, s.MediaPath.TrimStart('\\') + s.MediaName + s.MediaType);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
        }
        public void AddAllStoryMedia(string wwwRootPath, List<IFormFile> images, long storyId)
        {
            var medias = images.Select(image =>
            {
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(wwwRootPath, @"images\story");
                string extension = Path.GetExtension(image.FileName);
                using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    image.CopyTo(fileStreams);
                }
                return new StoryMedium()
                {
                    MediaPath = @"\images\story\",
                    MediaName = fileName,
                    MediaType = extension,
                    StoryId = storyId,
                };
            });
            _unitOfWork.StoryMedia.AddRange(medias);
        }
        public void EditAllStoryMedia(string wwwRootPath, List<IFormFile> images, long storyId, List<string>? preLoaded)
        {
            preLoaded?.ForEach(pre =>
            {
                if (!images.Any(ss => pre == ss.FileName))
                {
                    System.IO.File.Delete(Path.Combine(wwwRootPath, @"images\story", pre));
                    string name = pre.Split(".")[0];
                    _unitOfWork.StoryMedia.Remove(_unitOfWork.StoryMedia.GetFirstOrDefault(mm => mm.StoryId == storyId &&
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
                AddAllStoryMedia(wwwRootPath, newImages, storyId);
        }
        public static StoryMediaVM ConvertStoryMediaToVM(StoryMedium sm)
        {
            return new StoryMediaVM()
            {
                CreatedAt = sm.CreatedAt,
                MediaName = sm.MediaName,
                MediaPath = sm.MediaPath,
                MediaType = sm.MediaType,
                StoryId = sm.StoryId,
            };
        }
    }
}
