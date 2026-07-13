using System.ComponentModel.DataAnnotations;

namespace DroWashWebsite.Models
{
    public class Benefit
    {
        public int Id { get; set; }

        [Required, StringLength(300)]
        public string Text { get; set; } = string.Empty;

        public int SortOrder { get; set; }
    }
}