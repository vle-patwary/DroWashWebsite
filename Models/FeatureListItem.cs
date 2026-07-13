using System.ComponentModel.DataAnnotations;

namespace DroWashWebsite.Models
{
    public enum FeatureListCategory
    {
        Deionized,
        StreakFree
    }

    public class FeatureListItem
    {
        public int Id { get; set; }

        [Required]
        public FeatureListCategory Category { get; set; }

        [Required, StringLength(10)]
        public string Number { get; set; } = string.Empty;

        [Required, StringLength(150)]
        public string Title { get; set; } = string.Empty;

        [Required, StringLength(500)]
        public string Description { get; set; } = string.Empty;

        public int SortOrder { get; set; }
    }
}