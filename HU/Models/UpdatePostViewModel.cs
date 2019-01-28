using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HU.Models
{
    public class UpdatePostViewModel
    {
        public int PostId { get; set; }

        [Required(ErrorMessage = "[ required ]")]
        public string Title { get; set; }

        public string Content { get; set; }
        public List<SelectListItem> PeriodsList { get; set; }

        [Required(ErrorMessage = "[ required ]")]
        public int SelectedPeriodId { get; set; }

        public List<SelectListItem> EventTypes { get; set; }
        public List<int> SelectedEventTypes { get; set; }

        public List<SelectListItem> FigureTypes { get; set; }
        public List<int> SelectedFigureTypes { get; set; }

        [Required(ErrorMessage = "[ required ]")]
        public DateTime PostDate { get; set; }

        public IFormFile Image { get; set; }
    }
}
