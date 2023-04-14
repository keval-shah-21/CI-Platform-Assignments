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

        [Required(ErrorMessage = "The Mission field is required.")]
        public string? MissionTitle { get; set; }
        public long MissionId { get; set; }
        public string? MissionTheme { get; set; }

        public long UserId { get; set; }

        [Required]
        [StringLength(80, MinimumLength = (20), ErrorMessage = "Minimum 20 characters required.")]
        public string Title { get; set; } = null!;

        public DateTimeOffset? PublishedAt { get; set; }

        [Url]
        public string? VideoUrl { get; set; }

        [Required]
        [Display(Name ="Short Description")]
        [StringLength(150, MinimumLength = (30), ErrorMessage = "Minimum 30 characters required.")]
        public string? ShortDescription { get; set; }

        [Required]
        public string Description { get; set; } = null!;

        public string StoryThumbnail { get; set; } = null!;
        public long? TotalViews { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }

        public DateTimeOffset? DeletedAt { get; set; }

        public ApprovalStatus ApprovalStatus { get; set; }

        public List<MissionVM>? MissionVMs { get; set; }

        public List<StoryMediaVM> StoryMediaVM { get; set; } = new();

        public UserVM UserVM { get; set; } = new();

    }
}
