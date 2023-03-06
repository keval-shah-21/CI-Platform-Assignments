using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;

namespace CI_Platform.DataAccess.Repository;

public class FavouriteMissionRepository : Repository<FavouriteMission>, IFavouriteMissionRepository
{
    public FavouriteMissionRepository(ApplicationDbContext context) : base(context)
    {
    }
}
