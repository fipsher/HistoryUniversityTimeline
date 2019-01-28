using System.ComponentModel.DataAnnotations;

namespace HU.Models
{
    public class EventTypeViewModel
    {
        public int EventTypeId { get; set; }

        [Required(ErrorMessage = "[required]")]
        public string EventTypeName { get; set; }
    }
}
