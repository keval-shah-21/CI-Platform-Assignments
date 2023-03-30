using CI_Platform.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Services.Service.Interface
{
    public interface IUserSkillService
    {
        List<UserSkillVM> GetAll();
        List<UserSkillVM> GetUserSkillsByUserId(long userId);

    }
}
