using CI_Platform.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.DataAccess.Repository.Interface
{
    public interface IStoryRepository: IRepository<Story>
    {
        IEnumerable<Story> GetAllWithInclude();
        Story GetFirstOrDefaultWithInclude(Expression<Func<Story, bool>> filter);
        void UpdateTotalViews(long storyId, long totalViews);
    }
}
