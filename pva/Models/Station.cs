using System.ComponentModel.DataAnnotations;

namespace pva.Models
{
    public class Station
    {
        [Key]
        public int StationId { get; set; }

        [Required]
        [Display(Name = "Station Name")]
        public string Name { get; set; }

        [Required]
        [Range(1, 100)]
        [Display(Name = "Flood Level")]
        public int FloodLevel { get; set; }

        [Required]
        [Range(1, 100)]
        [Display(Name = "Drought Level")]
        public int DroughtLevel { get; set; }

        [Required]
        [Range(1, 100)]
        [Display(Name = "Timeout (Minutes)")]
        public int TimeOutinMinutes { get; set; }
    }
}
