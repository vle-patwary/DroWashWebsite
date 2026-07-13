using System.ComponentModel.DataAnnotations;

namespace DroWashWebsite.Models
{
    public class HeroSlide
    {
        public int Id { get; set; }

        [Required, StringLength(300)]
        public string ImageUrl { get; set; } = string.Empty;

        public int SortOrder { get; set; }
    }
}