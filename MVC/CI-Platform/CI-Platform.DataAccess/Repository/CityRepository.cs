using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;

namespace CI_Platform.DataAccess.Repository;

public class CityRepository : Repository<City>, ICityRepository
{
    public CityRepository(ApplicationDbContext context) : base(context)
    {
    }
}
