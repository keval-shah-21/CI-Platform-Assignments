namespace CI_Platform.Services.Service.Interface;

public interface IUnitOfService
{
    IUserService User{get;}

    void Save();
}