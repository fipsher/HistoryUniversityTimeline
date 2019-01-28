using System;
using System.Collections.Generic;

namespace HUDLL.ViewModels
{
    public class PostViewModel
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public int TimePeriodId { get; set; }
        public string PeriodName { get; set; }
        public DateTime PostDate { get; set; }
        public List<int> EventTypes { get; set; }
        public List<int> FigureTypes { get; set; }
    }
}
