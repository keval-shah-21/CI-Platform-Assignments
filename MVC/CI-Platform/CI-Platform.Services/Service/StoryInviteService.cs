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
    public class StoryInviteService: IStoryInviteService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StoryInviteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public static StoryInviteVM ConvertStoryInviteToVM(StoryInvite si) {
            return new StoryInviteVM()
            {
                FromUserId = si.FromUserId,
                StoryId = si.StoryId,
                StoryInviteId = si.StoryInviteId,
                ToUserId = si.ToUserId
            };
        } 
    }
}
