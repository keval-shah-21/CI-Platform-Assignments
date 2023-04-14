using CI_Platform.Entities.DataModels;

namespace CI_Platform.DataAccess.Repository.Interface;

public interface IBannerRepository: IRepository<Banner>
{
    void RemoveById(long id);
}
