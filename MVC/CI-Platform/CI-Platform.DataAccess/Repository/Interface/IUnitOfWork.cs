namespace CI_Platform.DataAccess.Repository.Interface;

public interface IUnitOfWork{
    IUserRepository User{get;}

    IResetPasswordRepository ResetPassword {get;}
    void Save();
}