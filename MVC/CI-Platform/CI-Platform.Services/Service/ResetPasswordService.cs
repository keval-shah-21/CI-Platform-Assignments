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
            Token = resetPasswordVM.Token
        };
        _unitOfWork.ResetPassword.Add(obj);
    }

    
    public bool IsValidRecord(string email){
        ResetPassword obj = _unitOfWork.ResetPassword.GetFirstOrDefault(data => data.Email == email);
        return obj == null ? false : true;
    }

    public void Remove(ResetPasswordDataVM resetPasswordDataVM){
        ResetPassword obj = new ResetPassword(){
            Email = resetPasswordDataVM.Email,
            Token = resetPasswordDataVM.Token
        };
        _unitOfWork.ResetPassword.Remove(obj);
    }

    public void Update(ResetPasswordVM resetPasswordVM){
        ResetPassword obj = new ResetPassword(){
            Email = resetPasswordVM.Email,
            Token = resetPasswordVM.Token
        };
        _unitOfWork.ResetPassword.Update(obj);
    }

    public ResetPasswordVM GetFirstOrDefaultByEmail(string email){
        ResetPassword obj = _unitOfWork.ResetPassword.GetFirstOrDefault(user => user.Email == email);
        return new ResetPasswordVM(){
            Email = obj.Email,
            Token = obj.Token
        };
    }
}
