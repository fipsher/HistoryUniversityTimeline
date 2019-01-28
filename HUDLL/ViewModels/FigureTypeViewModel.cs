using System.ComponentModel.DataAnnotations;

namespace HU.Models
{
    public class FigureTypeViewModel
    {
        public int FigureTypeId { get; set; }
        [Required(ErrorMessage = "[ required ]")]
        public string FigureTypeName { get; set; }
    }
}
