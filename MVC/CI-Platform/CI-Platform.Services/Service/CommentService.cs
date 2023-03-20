﻿using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.Constants;
using CI_Platform.Entities.DataModels;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;

namespace CI_Platform.Services.Service
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public List<CommentVM> GetAll()
        {
            IEnumerable<Comment> obj = _unitOfWork.Comment.GetAll();
            if (obj == null) return null!;
            return obj.Select(c => ConvertCommentToVM(c)).ToList();
        }

        public static CommentVM ConvertCommentToVM(Comment c)
        {
            return new CommentVM()
            {
                CommentId = c.CommentId,
                ApprovalStatus = c.ApprovalStatus == 0 ? ApprovalStatus.PENDING : 
                                c.ApprovalStatus == 1 ? ApprovalStatus.APPROVED :
                                ApprovalStatus.DECLINED,
                MissionId = c.MissionId,
                UserId = c.UserId,
                UserName = c.User.FirstName + " " + c.User.LastName,
                Avatar = c.User.Avatar,
                Text = c.Text,
                CreatedAt = c.CreatedAt
            };
        }

        public void PostComment(long missionId, long userId, string comment){
            _unitOfWork.Comment.Add(new Comment(){
                MissionId = missionId,
                UserId = userId,
                Text = comment,
                CreatedAt = DateTimeOffset.Now
            });
        }
    }
}
