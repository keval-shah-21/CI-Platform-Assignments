using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (obj == null) return null!;
            return obj.Select(md => ConvertMissionDocumentToVM(md)).ToList();
        }
        public static MissionDocumentVM ConvertMissionDocumentToVM(MissionDocument md)
        {
            return new MissionDocumentVM()
            {
                MissionDocumentId = md.MissionDocumentId,
                DocumentName = md.DocumentName,
                DocumentPath = md.DocumentPath,
                DocumentType = md.DocumentType,
                MissionId = md.MissionId
            };
        }
    }
}
