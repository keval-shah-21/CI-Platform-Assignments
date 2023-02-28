namespace CI_Platform.DataAccess.Repository.Interface;

public interface IUnitOfWork{
    IUserRepository User{get;}
    void Save();
}