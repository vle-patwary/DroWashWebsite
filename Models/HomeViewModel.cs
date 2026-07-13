namespace DroWashWebsite.Models
{
    public class ServiceItem
    {
        public string Index { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string IconSvgPath { get; set; } = string.Empty;
    }

    public class HomeViewModel
    {
        public List<ServiceItem> Services { get; set; } = new();
        public List<string> AdditionalServices { get; set; } = new();
        public List<string> Benefits { get; set; } = new();
        public List<string> HeroSlides { get; set; } = new();
        public List<ProcessStep> ProcessSteps { get; set; } = new();
        public List<WhyCard> WhyCards { get; set; } = new();
        public List<FeatureListItem> DeionizedFeatures { get; set; } = new();
        public List<FeatureListItem> StreakFreeFeatures { get; set; } = new();
        public string? BeforeImageUrl { get; set; }
        public string? AfterImageUrl { get; set; }
    }
}