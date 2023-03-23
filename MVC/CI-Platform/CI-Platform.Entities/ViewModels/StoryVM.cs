using CI_Platform.Entities.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Entities.ViewModels
{
    public class StoryVM
    {
        public long StoryId { get; set; }

        public long MissionId { get; set; }
        public string? MissionTheme { get; set; }

        public long UserId { get; set; }
        public string Title { get; set; } = null!;

        public DateTimeOffset? PublishedAt { get; set; }

        public string? VideoUrl { get; set; }

        public string Description { get; set; } = null!;

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }

        public DateTimeOffset? DeletedAt { get; set; }

        public ApprovalStatus ApprovalStatus { get; set; }
    }
}
