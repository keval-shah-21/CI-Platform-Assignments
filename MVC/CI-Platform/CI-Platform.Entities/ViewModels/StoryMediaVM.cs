using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Entities.ViewModels
{
    public  class StoryMediaVM
    {
        public long StoryMediaId { get; set; }

        public long StoryId { get; set; }

        public string? MediaName { get; set; }

        public string? MediaType { get; set; }

        public string? MediaPath { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
    }
}
