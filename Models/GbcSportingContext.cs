using Microsoft.EntityFrameworkCore;

namespace GBCSporting_X_TEAM.Models
{
    public class GbcSportingContext : DbContext
    {
        public GbcSportingContext(DbContextOptions<GbcSportingContext> options) : base(options)
        {

        }

        public DbSet<Incident> Incidents { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Technician> Technicians { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Registration> Registrations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Technician>().HasData(
                new Technician
                {
                    TechnicianId = 1,
                    Name = "George Akron",
                    Email = "g.akron@gbcsport.com",
                    Phone = "332-974-2398"
                },
                 new Technician
                 {
                     TechnicianId = 2,
                     Name = "Kevin Pickrell",
                     Email = "k.pickrell@gbcsport.com",
                     Phone = "510-367-5104"
                 },
                 new Technician
                 {
                     TechnicianId = 3,
                     Name = "Elizabeth Bishop",
                     Email = "e.bishop@gbcsport.com",
                     Phone = "718-340-3265"
                 },
                 new Technician
                 {
                     TechnicianId = 4,
                     Name = "Jeanne Stephens",
                     Email = "j.stephens@gbcsport.com",
                     Phone = "732-519-3152"
                 },
                 new Technician
                 {
                     TechnicianId = 5,
                     Name = "Michael Day",
                     Email = "d.Michael@gbcsport.com",
                     Phone = "518-200-4074"
                 }
               );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = 1,
                    Name = "Driving Range Scheduler",
                    Code = "vsg-001",
                    Price = 30.99,
                    ReleaseDate = DateTime.Parse("2009-02-04")
                },
                new Product
                {
                    ProductId = 2,
                    Name = "Swing Simulator 2077",
                    Code = "vsg-002",
                    Price = 79.99,
                    ReleaseDate = DateTime.Parse("2019-01-01")
                },
                new Product
                {
                    ProductId = 3,
                    Name = "Quick Serve Legacy POS",
                    Code = "fsp-001",
                    Price = 159.99,
                    ReleaseDate = DateTime.Parse("2005-05-20")
                },
                new Product
                {
                    ProductId = 4,
                    Name = "Quick Serve - SaaS POS",
                    Code = "fsp-002",
                    Price = 59.99,
                    ReleaseDate = DateTime.Parse("2018-07-13")
                },
                new Product
                {
                    ProductId = 5,
                    Name = "Falcon Rental Manager",
                    Code = "rsp-001",
                    Price = 109.99,
                    ReleaseDate = DateTime.Parse("2017-03-28")
                }

                );

            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    CustomerId = 1,
                    FirstName = "Eleanor",
                    LastName = "Fortin",
                    Address = "4129 Wines Lane",
                    City = "Houston",
                    State = "TX",
                    PostalCode = "77002",
                    Phone = "832-261-2126",
                    CountryId = 1,
                    Email = "e.fortin@hotmail.com"
                },
                new Customer
                {
                    CustomerId = 2,
                    FirstName = "Matilde",
                    LastName = "Ely",
                    Address = "193 Yorkie Lane",
                    City = "Savannah",
                    State = "GA",
                    PostalCode = "77002",
                    Phone = "912-898-0483",
                    CountryId = 1,
                    Email = "m.ely@gmail.com"

                },
                new Customer
                {
                    CustomerId = 3,
                    FirstName = "Gerardo",
                    LastName = "Parker",
                    Address = "1029 Quiet Valley Lane",
                    City = "Northridge",
                    State = "CA",
                    PostalCode = "91324",
                    Phone = "818-882-6074",
                    CountryId = 1,
                    Email = "g.parker@yahoo.com"
                },
                new Customer
                {
                    CustomerId = 4,
                    FirstName = "David",
                    LastName = "Harrington",
                    Address = "3312 Coventry Court",
                    City = "Woodville",
                    State = "LA",
                    PostalCode = "39669",
                    Phone = "225-874-8694",
                    CountryId = 1,
                    Email = "d.harrington@aol.com"
                },
                new Customer
                {
                    CustomerId = 5,
                    FirstName = "George",
                    LastName = "Collette",
                    Address = "1000 Church Street",
                    City = "Brooklyn",
                    State = "NY",
                    PostalCode = "11238",
                    Phone = "843-871-6280",
                    CountryId = 1,
                    Email = "g.collette@proton.com"
                }
        );

            modelBuilder.Entity<Incident>().HasData(
                new Incident
                {
                    IncidentId = 1,
                    CustomerId = 1,
                    ProductId = 2,
                    TechnicianId = 5,
                    Title = "Simulator can not find installed camera.",
                    Description = "The camera works with other software, only the simulator doesnt seem to recogize it.",
                    DateOpened = DateTime.Now
                },
                new Incident
                {
                    IncidentId = 2,
                    CustomerId = 2,
                    ProductId = 4,
                    TechnicianId = 2,
                    Title = "Software causes Point of Sale hardware to freeze",
                    Description = "Freeze occurs when a customer tries to check out with an empty cart.",
                    DateOpened = DateTime.Now
                },
                new Incident
                {
                    IncidentId = 3,
                    CustomerId = 2,
                    ProductId = 4,
                    TechnicianId = 3,
                    Title = "Software gui glitches.",
                    Description = "Software gui glitches for a moment after tickets are purchased.",
                    DateOpened = DateTime.Now
                },
                new Incident
                {
                    IncidentId = 4,
                    CustomerId = 4,
                    ProductId = 5,
                    TechnicianId = 1,
                    Title = "Software does not always send out rental return reminders",
                    Description = "Seems to only occur if the return date is a tuesday.",
                    DateOpened = DateTime.Now
                },
                new Incident
                {
                    IncidentId = 5,
                    CustomerId = 4,
                    ProductId = 5,
                    TechnicianId = 1,
                    Title = "Software sometimes does not add returned item to available item pool automatically",
                    Description = "If an item is scanned as returned some times it does appear in the pool and has to be re-added manually.",
                    DateOpened = DateTime.Now
                }
                );

            modelBuilder.Entity<Country>().HasData(
                    new Country { CountryId = 1, CountryName = "United States" },
                    new Country { CountryId = 2, CountryName = "Canada" },
                    new Country { CountryId = 4, CountryName = "European Union" },
                    new Country { CountryId = 5, CountryName = "United kingdom" }
                );

            // composite primary key for Registration
            modelBuilder.Entity<Registration>()
            .HasKey(ba => new { ba.CustomerId, ba.ProductId });

            // one-to-many relationship between Product and Registration
            modelBuilder.Entity<Registration>()
            .HasOne(r => r.Product)
            .WithMany(p => p.Registrations)
            .HasForeignKey(r => r.ProductId);

            // one-to-many relationship between Customer and Registration
            modelBuilder.Entity<Registration>()
            .HasOne(r => r.Customer)
            .WithMany(c => c.Registrations)
            .HasForeignKey(r => r.CustomerId);

            modelBuilder.Entity<Registration>().HasData(
                    new Registration { CustomerId = 1, ProductId = 1 },
                    new Registration { CustomerId = 1, ProductId = 2 },
                    new Registration { CustomerId = 1, ProductId = 3 },
                    new Registration { CustomerId = 2, ProductId = 1 },
                    new Registration { CustomerId = 3, ProductId = 3 },
                    new Registration { CustomerId = 4, ProductId = 5 },
                    new Registration { CustomerId = 4, ProductId = 4 }
                );
        }
    }
}
