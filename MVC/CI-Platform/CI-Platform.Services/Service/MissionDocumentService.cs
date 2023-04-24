using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;
using Microsoft.AspNetCore.Http;

namespace CI_Platform.Services.Service
{
    public class MissionDocumentService : IMissionDocumentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MissionDocumentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public List<MissionDocumentVM> GetAll()
        {
            IEnumerable<MissionDocument> obj = _unitOfWork.MissionDocument.GetAll();
            return obj.Select(md => ConvertMissionDocumentToVM(md)).ToList();
        }

        public void AddMissionDocuments(string wwwRootPath, List<IFormFile> docs, long missionId)
        {
            var medias = docs.Select(doc =>
            {
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(wwwRootPath, @"documents\mission");
                string extension = Path.GetExtension(doc.FileName);
                using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    doc.CopyTo(fileStreams);
                }
                return new MissionDocument()
                {
                    DocumentPath = @"\documents\mission\",
                    DocumentName = fileName,
                    DocumentType = extension,
                    MissionId = missionId,
                    Title = doc.FileName
                };
            });
            if (medias.Any())
                _unitOfWork.MissionDocument.AddRange(medias);
        }

        public void EditMissionDocuments(string wwwRootPath, List<IFormFile> images, long missionId, List<string>? preLoaded)
        {
            preLoaded?.ForEach(pre =>
            {
                if (!images.Any(ss => pre == ss.FileName))
                {
                    System.IO.File.Delete(Path.Combine(wwwRootPath, @"documents\mission", pre));
                    string name = pre.Split(".")[0];
                    _unitOfWork.MissionDocument.Remove(_unitOfWork.MissionDocument.GetFirstOrDefault(mm => mm.MissionId == missionId &&
                    mm.DocumentName == name));
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
                AddMissionDocuments(wwwRootPath, newImages, missionId);
        }

        public static MissionDocumentVM ConvertMissionDocumentToVM(MissionDocument md)
        {
            return new MissionDocumentVM()
            {
                MissionDocumentId = md.MissionDocumentId,
                DocumentName = md.DocumentName,
                DocumentPath = md.DocumentPath,
                DocumentType = md.DocumentType,
                MissionId = md.MissionId,
                Title = md.Title
            };
        }
    }
}
