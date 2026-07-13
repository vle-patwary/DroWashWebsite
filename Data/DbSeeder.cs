using DroWashWebsite.Models;

namespace DroWashWebsite.Data
{
    public static class DbSeeder
    {
        public static void Seed(ApplicationDbContext db)
        {
            if (!db.Services.Any())
            {
                db.Services.AddRange(
                    new Service { Index = "01", Title = "High-Rise Window Cleaning", Description = "Professional high-rise window cleaning using advanced drone technology, eliminating the need for scaffolding, rope access or swing stages.", IconSvgPath = "<rect x=\"6\" y=\"2\" width=\"12\" height=\"20\" rx=\"1\"/><path d=\"M9 6h2M13 6h2M9 10h2M13 10h2M9 14h2M13 14h2M9 18h2M13 18h2\"/>", SortOrder = 1 },
                    new Service { Index = "02", Title = "Mid-Rise Window Cleaning", Description = "Exterior window cleaning for apartments, condos, offices, schools and hospitals that minimizes disruption while improving safety.", IconSvgPath = "<rect x=\"4\" y=\"7\" width=\"16\" height=\"15\" rx=\"1\"/><path d=\"M8 11h2M14 11h2M8 15h2M14 15h2M8 19h2M14 19h2\"/>", SortOrder = 2 },
                    new Service { Index = "03", Title = "Facade Cleaning", Description = "Restore your building's appearance across glass, aluminum panels, brick, stone, concrete, EIFS and metal cladding.", IconSvgPath = "<rect x=\"3\" y=\"3\" width=\"18\" height=\"18\" rx=\"1.5\"/><path d=\"M3 9h18M3 15h18M9 3v18M15 3v18\"/>", SortOrder = 3 },
                    new Service { Index = "04", Title = "Exterior Pressure Cleaning", Description = "Drone-assisted pressure washing for hard-to-reach walls, roofs, parking garages, warehouses, stadiums and bridges.", IconSvgPath = "<path d=\"M12 2v6M8 5l4 3 4-3\"/><path d=\"M5 22c0-5 3-8 7-8s7 3 7 8\"/>", SortOrder = 4 },
                    new Service { Index = "05", Title = "Solar Panel Cleaning", Description = "Safely remove dust, pollen, bird droppings and debris from commercial, industrial and utility-scale solar farms.", IconSvgPath = "<rect x=\"3\" y=\"6\" width=\"18\" height=\"12\" rx=\"1.5\"/><path d=\"M7 6V4h10v2\"/>", SortOrder = 5 },
                    new Service { Index = "06", Title = "Wind Turbine Cleaning", Description = "Drone-based cleaning for towers, blades, nacelles and inspection support to reduce downtime and maintenance costs.", IconSvgPath = "<circle cx=\"12\" cy=\"6\" r=\"1.4\"/><path d=\"M12 6L12 22M12 6L6 10M12 6L18 10\"/>", SortOrder = 6 },
                    new Service { Index = "07", Title = "Industrial Plant Cleaning", Description = "Manufacturing facilities, power plants, refineries, warehouses, steel plants and mining operations where access is difficult.", IconSvgPath = "<path d=\"M3 21V10l5 3v-3l5 3v-3l6 4v7z\"/><path d=\"M3 21h18\"/>", SortOrder = 7 },
                    new Service { Index = "08", Title = "Oil & Gas Facility Cleaning", Description = "Safe exterior cleaning for refineries, gas processing facilities, LNG terminals, offshore platforms and storage tanks.", IconSvgPath = "<path d=\"M12 22s7-4.5 7-11a7 7 0 10-14 0c0 6.5 7 11 7 11z\"/><circle cx=\"12\" cy=\"11\" r=\"2.4\"/>", SortOrder = 8 },
                    new Service { Index = "09", Title = "Hydro Infrastructure Cleaning", Description = "Hydroelectric stations, substations, transmission towers, dams and cooling towers, reducing worker exposure to hazards.", IconSvgPath = "<path d=\"M3 10h18M3 10v8h18v-8M7 10V6M12 10V4M17 10V6\"/>", SortOrder = 9 },
                    new Service { Index = "10", Title = "Infrastructure Cleaning", Description = "Bridges, stadiums, airports, ports, water towers, communication towers, monuments and government buildings.", IconSvgPath = "<path d=\"M2 16c3-4 6-4 10-4s7 0 10 4\"/><path d=\"M6 16v4M12 16v4M18 16v4\"/>", SortOrder = 10 },
                    new Service { Index = "11", Title = "Water Tower Cleaning", Description = "Professional exterior cleaning for municipal and industrial water towers, reducing downtime while maintaining safety.", IconSvgPath = "<path d=\"M7 8h10l-2 4v8H9v-8z\"/><path d=\"M9 8V5a3 3 0 016 0v3\"/>", SortOrder = 11 },
                    new Service { Index = "12", Title = "Cruise Ship & Marine Cleaning", Description = "Exterior cleaning for cruise ships, ferries, cargo vessels, yachts, docks and port infrastructure with minimal disruption.", IconSvgPath = "<path d=\"M4 15h16l-2 5H6z\"/><path d=\"M6 15V6h8l3 4\"/><path d=\"M9 6V3h3v3\"/>", SortOrder = 12 },
                    new Service { Index = "13", Title = "Statue & Monument Cleaning", Description = "Gentle drone-assisted cleaning that preserves monuments, historical landmarks, sculptures and public artwork.", IconSvgPath = "<path d=\"M9 21h6M8 21V9h8v12M6 9h12l-1-3H7z\"/>", SortOrder = 13 },
                    new Service { Index = "14", Title = "Roof Cleaning", Description = "Removes moss, algae, mold and debris from commercial and industrial roofs without workers walking on the surface.", IconSvgPath = "<path d=\"M3 12l9-8 9 8\"/><path d=\"M6 10v10h12V10\"/>", SortOrder = 14 },
                    new Service { Index = "15", Title = "Warehouse & Distribution Centre Cleaning", Description = "Exterior cleaning for warehouses, logistics centers and fulfillment centers to protect long-term asset value.", IconSvgPath = "<path d=\"M3 21V11l9-6 9 6v10\"/><path d=\"M9 21v-6h6v6\"/>", SortOrder = 15 }
                );
            }

            if (!db.AdditionalServices.Any())
            {
                var names = new[]
                {
                    "Building Soft Washing", "Parking Garage Cleaning", "Stadium Cleaning", "Airport Terminal Exterior Cleaning",
                    "Bridge Cleaning", "Communication Tower Cleaning", "Cell Tower Cleaning", "Cooling Tower Cleaning",
                    "Water Reservoir Cleaning", "Grain Silo Cleaning", "Cement Plant Cleaning", "Mining Equipment Cleaning",
                    "Port & Terminal Cleaning", "Railway Infrastructure Cleaning", "Billboard Cleaning", "Warehouse Roof Cleaning",
                    "Hotel Exterior Cleaning", "Shopping Mall Exterior Cleaning", "Hospital Exterior Cleaning",
                    "University & School Building Cleaning", "Government Building Cleaning", "Church & Heritage Building Cleaning"
                };
                var order = 1;
                foreach (var name in names)
                {
                    db.AdditionalServices.Add(new AdditionalService { Name = name, SortOrder = order++ });
                }
            }

            if (!db.Benefits.Any())
            {
                var benefits = new[]
                {
                    "Deionized (DI) purified water for a spot-free, streak-free finish",
                    "No soaps or harsh chemicals",
                    "Eco-friendly cleaning solution",
                    "Safe alternative to rope access and swing stages",
                    "Reduced liability and insurance risks",
                    "Faster project completion",
                    "Minimal disruption to occupants",
                    "Crystal-clear glass with no mineral deposits",
                    "Suitable for buildings up to 50+ stories",
                    "Ideal for scheduled maintenance programs"
                };
                var order = 1;
                foreach (var text in benefits)
                {
                    db.Benefits.Add(new Benefit { Text = text, SortOrder = order++ });
                }
            }

            if (!db.ProcessSteps.Any())
            {
                db.ProcessSteps.AddRange(
                    new ProcessStep { Number = "01", Title = "Request A Quote", Description = "Tell us the building type, height and problem areas to start the process.", SortOrder = 1 },
                    new ProcessStep { Number = "02", Title = "Discovery Call", Description = "We walk through access, scheduling and any site restrictions together.", SortOrder = 2 },
                    new ProcessStep { Number = "03", Title = "Cleaning Quote", Description = "A fixed quote is issued and a cleaning date is locked in.", SortOrder = 3 },
                    new ProcessStep { Number = "04", Title = "Site Inspection", Description = "A short pre-flight check confirms flight paths and surface conditions.", SortOrder = 4 },
                    new ProcessStep { Number = "05", Title = "Cleaning Complete", Description = "Final walkthrough, photo report and feedback form delivered same day.", SortOrder = 5 }
                );
            }

            if (!db.WhyCards.Any())
            {
                db.WhyCards.AddRange(
                    new WhyCard { Title = "Efficient and fast", Description = "Our drones cover large surfaces quickly, cleaning entire building exteriors in a fraction of traditional time.", IconSvgPath = "<path d=\"M13 2L4 14h7l-1 8 9-12h-7l1-8z\"/>", SortOrder = 1 },
                    new WhyCard { Title = "Cost-effective", Description = "By using drones instead of scaffolding or lift crews, we reduce labor and equipment costs significantly.", IconSvgPath = "<circle cx=\"12\" cy=\"12\" r=\"9\"/><path d=\"M12 7v5l3 3\"/>", SortOrder = 2 },
                    new WhyCard { Title = "Safer for everyone", Description = "No rope access or scaffolding means no crews working at height and no risk to pedestrians below.", IconSvgPath = "<path d=\"M12 3l7 4v5c0 5-3.4 8.4-7 9-3.6-.6-7-4-7-9V7l7-4z\"/>", SortOrder = 3 },
                    new WhyCard { Title = "No disruption", Description = "Cleaning runs from the air with no scaffolding footprint, so tenants and foot traffic carry on as normal.", IconSvgPath = "<rect x=\"3\" y=\"4\" width=\"18\" height=\"16\" rx=\"2\"/><path d=\"M3 9h18\"/>", SortOrder = 4 },
                    new WhyCard { Title = "Precise and controlled", Description = "GPS-guided flight paths and metered spray keep coverage consistent across every pass.", IconSvgPath = "<path d=\"M5 12a7 7 0 1114 0 7 7 0 01-14 0z\"/><path d=\"M12 8v4l3 2\"/>", SortOrder = 5 },
                    new WhyCard { Title = "Low environmental impact", Description = "Deionized water and metered cutting cuts chemical runoff versus manual pressure-washing.", IconSvgPath = "<path d=\"M12 22s7-4.5 7-11a7 7 0 10-14 0c0 6.5 7 11 7 11z\"/><circle cx=\"12\" cy=\"11\" r=\"2.4\"/>", SortOrder = 6 }
                );
            }

            if (!db.FeatureListItems.Any())
            {
                db.FeatureListItems.AddRange(
                    new FeatureListItem { Category = FeatureListCategory.Deionized, Number = "1", Title = "Spotless results", Description = "Deionized water dries clear on glass and metal, with no mineral residue.", SortOrder = 1 },
                    new FeatureListItem { Category = FeatureListCategory.Deionized, Number = "2", Title = "Stronger cleaning power", Description = "Purity lets cleaning agents work through grime more thoroughly.", SortOrder = 2 },
                    new FeatureListItem { Category = FeatureListCategory.Deionized, Number = "3", Title = "Eco-friendly by design", Description = "Less chemical use means a smaller footprint on every job.", SortOrder = 3 },
                    new FeatureListItem { Category = FeatureListCategory.Deionized, Number = "4", Title = "Versatile across surfaces", Description = "Safe for glass, stone, metal, membrane roofing and solar glass alike.", SortOrder = 4 },

                    new FeatureListItem { Category = FeatureListCategory.StreakFree, Number = "1", Title = "Improved aesthetics", Description = "Streak-free glass and facades sharpen a building's curb appeal instantly.", SortOrder = 1 },
                    new FeatureListItem { Category = FeatureListCategory.StreakFree, Number = "2", Title = "Reduced maintenance", Description = "Preventing mineral buildup means fewer repeat cleans and less surface wear.", SortOrder = 2 },
                    new FeatureListItem { Category = FeatureListCategory.StreakFree, Number = "3", Title = "Higher property value", Description = "A well-kept exterior signals upkeep to tenants and buyers alike.", SortOrder = 3 }
                );
            }

            db.SaveChanges();
        }
    }
}