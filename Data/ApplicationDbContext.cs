using Microsoft.EntityFrameworkCore;
using DroWashWebsite.Models;

namespace DroWashWebsite.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Service> Services { get; set; } = null!;
        public DbSet<AdditionalService> AdditionalServices { get; set; } = null!;
        public DbSet<Benefit> Benefits { get; set; } = null!;
        public DbSet<ProcessStep> ProcessSteps { get; set; } = null!;
        public DbSet<WhyCard> WhyCards { get; set; } = null!;
        public DbSet<FeatureListItem> FeatureListItems { get; set; } = null!;
        public DbSet<HeroSlide> HeroSlides { get; set; } = null!;
        public DbSet<BeforeAfterImage> BeforeAfterImages { get; set; } = null!;
        public DbSet<QuoteRequest> QuoteRequests { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Store enums as readable strings in the DB instead of numbers,
            // so the data is human-readable if you ever query it directly.
            modelBuilder.Entity<FeatureListItem>()
                .Property(f => f.Category)
                .HasConversion<string>();

            modelBuilder.Entity<BeforeAfterImage>()
                .Property(b => b.Type)
                .HasConversion<string>();
        }
    }
}