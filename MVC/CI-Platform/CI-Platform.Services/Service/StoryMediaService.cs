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
    public class StoryMediaService: IStoryMediaService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StoryMediaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SaveStoryMedia()
        {

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
                Default = sm.Default
            };
        }
    }
}
