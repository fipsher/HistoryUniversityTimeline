using HUDLL.ViewModels;
using System.Collections.Generic;

namespace HU.Models
{
    public class PostsByPeriodViewModel
    {
        public List<PostViewModel> PostsByPeriod { get; set; }
        public List<TimePeriodViewModel> Periods { get; set; }
        public List<FigureTypeViewModel> FigureTypes { get; set; }
        public List<EventTypeViewModel> EventTypes { get; set; }
    }
}
