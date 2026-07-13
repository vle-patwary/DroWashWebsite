using System.ComponentModel.DataAnnotations;

namespace DroWashWebsite.Models
{
    public class WhyCard
    {
        public int Id { get; set; }

        [Required, StringLength(150)]
        public string Title { get; set; } = string.Empty;

        [Required, StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string IconSvgPath { get; set; } = string.Empty;

        public int SortOrder { get; set; }
    }
}