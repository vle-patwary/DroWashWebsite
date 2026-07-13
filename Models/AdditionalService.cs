using System.ComponentModel.DataAnnotations;

namespace DroWashWebsite.Models
{
    public class AdditionalService
    {
        public int Id { get; set; }

        [Required, StringLength(150)]
        public string Name { get; set; } = string.Empty;

        public int SortOrder { get; set; }
    }
}