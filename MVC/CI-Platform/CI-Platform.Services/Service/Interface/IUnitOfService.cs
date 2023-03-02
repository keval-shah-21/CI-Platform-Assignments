namespace CI_Platform.Services.Service.Interface;

public interface IUnitOfService
{
    IUserService User{get;}

    IResetPasswordService ResetPassword{get;}

    void Save();
}