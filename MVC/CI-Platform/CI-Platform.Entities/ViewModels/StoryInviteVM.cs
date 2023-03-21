using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Entities.ViewModels
{
    public class StoryInviteVM
    {
        public long StoryInviteId { get; set; }

        public long StoryId { get; set; }

        public long FromUserId { get; set; }

        public long ToUserId { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
    }
}
