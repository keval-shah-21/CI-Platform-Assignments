using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;

namespace CI_Platform.Services.Service;

public class ResetPasswordService:IResetPasswordService
{
    private readonly IUnitOfWork _unitOfWork;

    public ResetPasswordService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void Add(ResetPasswordVM resetPasswordVM)
    {
        ResetPassword obj = new ResetPassword(){
            Email = resetPasswordVM.Email,
            Token = resetPasswordVM.Token,
            CreatedAt = resetPasswordVM.CreatedAt
        };
        _unitOfWork.ResetPassword.Add(obj);
    }

    
    public bool IsValidRequest(string email, string token){
        ResetPassword obj = _unitOfWork.ResetPassword.GetFirstOrDefault(data => data.Email == email && data.Token == token);
        return obj == null ? false : true;
    }

    public byte IsValidRecord(string email){
        ResetPassword obj = _unitOfWork.ResetPassword.GetFirstOrDefault(data => data.Email == email);
        if(obj != null){
            DateTimeOffset recordTime = obj.CreatedAt;
            DateTimeOffset current = DateTimeOffset.Now;
            if((current - recordTime).TotalMinutes < 60){
                return 0;
            }
            else
                return 1;
        }
        return 11;
    }

    public void Remove(ResetPasswordVM resetPasswordVM){
        ResetPassword obj = new ResetPassword(){
            Email = resetPasswordVM.Email,
            Token = resetPasswordVM.Token
        };
        _unitOfWork.ResetPassword.Remove(obj);
    }

    public void RemoveByEmail(string email){
        ResetPassword obj = _unitOfWork.ResetPassword.GetFirstOrDefault(data => data.Email == email);
        _unitOfWork.ResetPassword.Remove(obj);
    }

    public void Update(ResetPasswordVM resetPasswordVM){
        ResetPassword obj = _unitOfWork.ResetPassword.GetFirstOrDefault(rp => 
            rp.Email == resetPasswordVM.Email);
        obj.Token = resetPasswordVM.Token;
        _unitOfWork.ResetPassword.Update(obj);
    }

    public ResetPasswordVM GetFirstOrDefaultByEmail(string email){
        ResetPassword obj = _unitOfWork.ResetPassword.GetFirstOrDefault(user => user.Email == email);
        if(obj != null)
            return new ResetPasswordVM(){
                Email = obj.Email,
                Token = obj.Token
            };
        return null!;
    }
}
