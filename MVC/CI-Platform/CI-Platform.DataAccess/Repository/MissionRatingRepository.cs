using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;

namespace CI_Platform.DataAccess.Repository;

public class MissionRatingRepository : Repository<MissionRating>, IMissionRatingRepository
{
    public MissionRatingRepository(ApplicationDbContext context) : base(context)
    {
    }
}
