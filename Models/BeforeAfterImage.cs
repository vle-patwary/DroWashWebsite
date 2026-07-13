using System.ComponentModel.DataAnnotations;

namespace DroWashWebsite.Models
{
    public enum BeforeAfterType
    {
        Before,
        After
    }

    public class BeforeAfterImage
    {
        public int Id { get; set; }

        [Required]
        public BeforeAfterType Type { get; set; }

        [Required, StringLength(300)]
        public string ImageUrl { get; set; } = string.Empty;
    }
}