using System.ComponentModel.DataAnnotations;

namespace DroWashWebsite.Models
{
    public class QuoteRequest
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter your name.")]
        [StringLength(100)]
        [Display(Name = "Full name")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter an email address.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a phone number.")]
        [Phone(ErrorMessage = "Please enter a valid phone number.")]
        [Display(Name = "Phone")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter the property address.")]
        [StringLength(200)]
        [Display(Name = "Property address")]
        public string PropertyAddress { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select a property type.")]
        [Display(Name = "Property type")]
        public string PropertyType { get; set; } = string.Empty;

        [StringLength(1000)]
        [Display(Name = "Tell us about the job")]
        public string? Notes { get; set; }

        // Honeypot field: real users never fill this in because it's hidden with CSS.
        // If it arrives populated, the submission is almost certainly a bot.
        public string? Website { get; set; }

        public DateTime SubmittedAtUtc { get; set; } = DateTime.UtcNow;

        public bool IsHandled { get; set; } = false;
    }
}