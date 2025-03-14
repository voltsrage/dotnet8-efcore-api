using EFCore.API.Entities;
using EFCore.API.Entities.BaseModel;
using Microsoft.EntityFrameworkCore;

namespace EFCore.API.Data
{
    public class AccommodationDBContext : DbContext
    {
        public AccommodationDBContext(DbContextOptions<AccommodationDBContext> options):base(options)
        {            
        }

        public DbSet<EntityStatus> EntityStatuses { get; set; } = null!;
        public DbSet<Hotel> Hotels { get; set; } = null!;
        public DbSet<Room> Rooms { get; set; } = null!;
        public DbSet<RoomType> RoomTypes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var now = DateTime.UtcNow;

            // Seed EntityStatus data
            modelBuilder.Entity<EntityStatus>().HasData(
                new EntityStatus
                {
                    Id = 1,
                    Name = "Active",
                    Description = "Entity is active and available",
                    CreatedAt = now,                    
                },
                new EntityStatus
                {
                    Id = 2,
                    Name = "Inactive",
                    Description = "Entity is temporarily unavailable",
                    CreatedAt = now                    
                },
                new EntityStatus
                {
                    Id = 3,
                    Name = "Deleted for everyone",
                    Description = "Entity has been deleted for everyone",
                    CreatedAt = now                    
                },
                new EntityStatus
                {
                    Id = 4,
                    Name = "Pending",
                    Description = "Entity is awaiting activation/verification",
                    CreatedAt = now
                },
                new EntityStatus
                {
                    Id = 5,
                    Name = "Archived",
                    Description = "Entity has been archived",
                    CreatedAt = now
                },
                new EntityStatus
                {
                    Id = 6,
                    Name = "Suspended",
                    Description = "Entity has been suspended",
                    CreatedAt = now
                },
                new EntityStatus
                {
                    Id = 7,
                    Name = "Delete for me",
                    Description = "Entity has been delete for me",
                    CreatedAt = now
                }
            );

            modelBuilder.Entity<RoomType>().HasData(
                new RoomType
                {
                    Id = 1,
                    Name = "Standard",
                    Description = "Standard room with basic amenities",
                    EntityStatusId = 1,
                    CreatedAt = now                    
                },
                new RoomType
                {
                    Id = 2,
                    Name = "Deluxe",
                    Description = "Deluxe room with enhanced amenities and comfort",
                    EntityStatusId = 1,
                    CreatedAt = now
                },
                new RoomType
                {
                    Id = 3,
                    Name = "Suite",
                    Description = "Suite with separate living area and bedroom",
                    EntityStatusId = 1,
                    CreatedAt = now
                },
                new RoomType
                {
                    Id = 4,
                    Name = "Executive",
                    Description = "Executive room with business amenities and services",
                    EntityStatusId = 1,
                    CreatedAt = now
                },
                new RoomType
                {
                    Id = 5,
                    Name = "Penthouse",
                    Description = "Penthouse suite, typically on the top floor with premium amenities",
                    EntityStatusId = 1,
                    CreatedAt = now
                },
                new RoomType
                {
                    Id = 6,
                    Name = "Family",
                    Description = "Family room with additional space for families",
                    EntityStatusId = 1,
                    CreatedAt = now
                },
                new RoomType
                {
                    Id = 7,
                    Name = "Accessible",
                    Description = "Accessible room designed for guests with disabilities",
                    EntityStatusId = 1,
                    CreatedAt = now
                },
                new RoomType
                {
                    Id = 8,
                    Name = "Single",
                    Description = "Single room designed for one person",
                    EntityStatusId = 1,
                    CreatedAt = now
                },
                new RoomType
                {
                    Id = 9,
                    Name = "Double",
                    Description = "Double room with a queen or king-sized bed",
                    EntityStatusId = 1,
                    CreatedAt = now
                },
                new RoomType
                {
                    Id = 10,
                    Name = "Twin",
                    Description = "Twin room with two separate beds",
                    EntityStatusId = 1,
                    CreatedAt = now
                },
                new RoomType
                {
                    Id = 11,
                    Name = "Presidential",
                    Description = "Presidential suite, the most luxurious accommodation",
                    EntityStatusId = 1,
                    CreatedAt = now
                },
                new RoomType
                {
                    Id = 12,
                    Name = "Villa",
                    Description = "Villa or cottage separate from the main hotel building",
                    EntityStatusId = 1,
                    CreatedAt = now
                }
            );

            // Seed Hotels
            modelBuilder.Entity<Hotel>().HasData(
                new Hotel { Id = 1, Name = "Grand Hyatt", Address = "123 Park Avenue", City = "New York", Country = "United States", PhoneNumber = "+1-212-555-1234", Email = "reservations@grandhyatt.com", EntityStatusId = 1, CreatedAt = now},
                new Hotel { Id = 2, Name = "Ritz-Carlton", Address = "50 Central Park South", City = "New York", Country = "United States", PhoneNumber = "+1-212-555-2345", Email = "info@ritzcarlton.com", EntityStatusId = 1, CreatedAt = now},
                new Hotel { Id = 3, Name = "Burj Al Arab", Address = "Jumeirah Beach Road", City = "Dubai", Country = "United Arab Emirates", PhoneNumber = "+971-4-555-6789", Email = "reservations@burjalarab.com", EntityStatusId = 1, CreatedAt = now},
                new Hotel { Id = 4, Name = "The Savoy", Address = "Strand", City = "London", Country = "United Kingdom", PhoneNumber = "+44-20-7555-1234", Email = "info@savoy.com", EntityStatusId = 1, CreatedAt = now},
                new Hotel { Id = 5, Name = "Plaza Athénée", Address = "25 Avenue Montaigne", City = "Paris", Country = "France", PhoneNumber = "+33-1-5555-6789", Email = "reservations@plaza-athenee.com", EntityStatusId = 1, CreatedAt = now},
                new Hotel { Id = 6, Name = "Mandarin Oriental", Address = "5 Connaught Road", City = "Hong Kong", Country = "China", PhoneNumber = "+852-2555-1234", Email = "mohkg@mandarinoriental.com", EntityStatusId = 1, CreatedAt = now},
                new Hotel { Id = 7, Name = "Raffles Hotel", Address = "1 Beach Road", City = "Singapore", Country = "Singapore", PhoneNumber = "+65-6555-1234", Email = "singapore@raffles.com", EntityStatusId = 1, CreatedAt = now},
                new Hotel { Id = 8, Name = "Waldorf Astoria", Address = "301 Park Avenue", City = "New York", Country = "United States", PhoneNumber = "+1-212-555-3456", Email = "reservations@waldorfastoria.com", EntityStatusId = 1, CreatedAt = now},
                new Hotel { Id = 9, Name = "Four Seasons", Address = "57 E 57th Street", City = "New York", Country = "United States", PhoneNumber = "+1-212-555-4567", Email = "reservations.nyc@fourseasons.com", EntityStatusId = 1, CreatedAt = now},
                new Hotel { Id = 10, Name = "Park Hyatt", Address = "2 Rue de la Paix", City = "Paris", Country = "France", PhoneNumber = "+33-1-5555-1234", Email = "paris.park@hyatt.com", EntityStatusId = 1, CreatedAt = now},
                new Hotel { Id = 11, Name = "Marina Bay Sands", Address = "10 Bayfront Avenue", City = "Singapore", Country = "Singapore", PhoneNumber = "+65-6555-2345", Email = "reservations@marinabaysands.com", EntityStatusId = 1, CreatedAt = now},
                new Hotel { Id = 12, Name = "The Peninsula", Address = "Salisbury Road", City = "Hong Kong", Country = "China", PhoneNumber = "+852-2555-2345", Email = "phk@peninsula.com", EntityStatusId = 1, CreatedAt = now},
                new Hotel { Id = 13, Name = "Claridges", Address = "Brook Street", City = "London", Country = "United Kingdom", PhoneNumber = "+44-20-7555-2345", Email = "reservations@claridges.com", EntityStatusId = 1, CreatedAt = now},
                new Hotel { Id = 14, Name = "Bellagio", Address = "3600 Las Vegas Blvd S", City = "Las Vegas", Country = "United States", PhoneNumber = "+1-702-555-1234", Email = "reservations@bellagio.com", EntityStatusId = 1, CreatedAt = now},
                new Hotel { Id = 15, Name = "Atlantis", Address = "Paradise Island", City = "Nassau", Country = "Bahamas", PhoneNumber = "+1-242-555-1234", Email = "reservations@atlantis.com", EntityStatusId = 1, CreatedAt = now},
                new Hotel { Id = 16, Name = "The Venetian", Address = "3355 Las Vegas Blvd S", City = "Las Vegas", Country = "United States", PhoneNumber = "+1-702-555-2345", Email = "reservations@venetian.com", EntityStatusId = 1, CreatedAt = now},
                new Hotel { Id = 17, Name = "InterContinental", Address = "1 Hamilton Place", City = "London", Country = "United Kingdom", PhoneNumber = "+44-20-7555-3456", Email = "london@intercontinental.com", EntityStatusId = 1, CreatedAt = now},
                new Hotel { Id = 18, Name = "Hilton Tokyo", Address = "6-6-2 Nishi-Shinjuku", City = "Tokyo", Country = "Japan", PhoneNumber = "+81-3-5555-1234", Email = "tokyo@hilton.com", EntityStatusId = 1, CreatedAt = now},
                new Hotel { Id = 19, Name = "Caesars Palace", Address = "3570 Las Vegas Blvd S", City = "Las Vegas", Country = "United States", PhoneNumber = "+1-702-555-3456", Email = "reservations@caesars.com", EntityStatusId = 1, CreatedAt = now},
                new Hotel { Id = 20, Name = "Hotel Arts", Address = "19-21 Marina", City = "Barcelona", Country = "Spain", PhoneNumber = "+34-93-555-1234", Email = "arts@ritzcarlton.com", EntityStatusId = 1, CreatedAt = now}
            );

            // Seed Rooms
            modelBuilder.Entity<Room>().HasData(
                // Grand Hyatt (ID 1)
                new Room { Id = 1, HotelId = 1, RoomNumber = "101", RoomTypeId = 1, PricePerNight = 299.99m, IsAvailable = true, MaxOccupancy = 2, EntityStatusId = 1, CreatedAt = now},
                new Room { Id = 2, HotelId = 1, RoomNumber = "102", RoomTypeId = 1, PricePerNight = 299.99m, IsAvailable = true, MaxOccupancy = 2, EntityStatusId = 1, CreatedAt = now},
                new Room { Id = 3, HotelId = 1, RoomNumber = "201", RoomTypeId = 2, PricePerNight = 459.99m, IsAvailable = true, MaxOccupancy = 3, EntityStatusId = 1, CreatedAt = now},

                // Ritz-Carlton (ID 2)
                new Room { Id = 4, HotelId = 2, RoomNumber = "A101", RoomTypeId = 2, PricePerNight = 699.99m, IsAvailable = true, MaxOccupancy = 2, EntityStatusId = 1, CreatedAt = now},
                new Room { Id = 5, HotelId = 2, RoomNumber = "A201", RoomTypeId = 3, PricePerNight = 1299.99m, IsAvailable = false, MaxOccupancy = 4, EntityStatusId = 1, CreatedAt = now},

                // Burj Al Arab (ID 3)
                new Room { Id = 6, HotelId = 3, RoomNumber = "Suite 1", RoomTypeId = 5, PricePerNight = 2999.99m, IsAvailable = true, MaxOccupancy = 4, EntityStatusId = 1, CreatedAt = now},
                new Room { Id = 7, HotelId = 3, RoomNumber = "Suite 2", RoomTypeId = 5, PricePerNight = 3499.99m, IsAvailable = true, MaxOccupancy = 6, EntityStatusId = 1, CreatedAt = now},

                // The Savoy (ID 4)
                new Room { Id = 8, HotelId = 4, RoomNumber = "301", RoomTypeId = 2, PricePerNight = 549.99m, IsAvailable = true, MaxOccupancy = 2, EntityStatusId = 1, CreatedAt = now},
                new Room { Id = 9, HotelId = 4, RoomNumber = "302", RoomTypeId = 2, PricePerNight = 549.99m, IsAvailable = false, MaxOccupancy = 2, EntityStatusId = 1, CreatedAt = now},

                // Plaza Athénée (ID 5)
                new Room { Id = 10, HotelId = 5, RoomNumber = "501", RoomTypeId = 3, PricePerNight = 899.99m, IsAvailable = true, MaxOccupancy = 3, EntityStatusId = 1, CreatedAt = now},

                // Mandarin Oriental (ID 6)
                new Room { Id = 11, HotelId = 6, RoomNumber = "701", RoomTypeId = 1, PricePerNight = 259.99m, IsAvailable = true, MaxOccupancy = 2, EntityStatusId = 1, CreatedAt = now},
                new Room { Id = 12, HotelId = 6, RoomNumber = "801", RoomTypeId = 4, PricePerNight = 799.99m, IsAvailable = true, MaxOccupancy = 4, EntityStatusId = 1, CreatedAt = now},

                // Four Seasons (ID 9)
                new Room { Id = 13, HotelId = 9, RoomNumber = "1201", RoomTypeId = 3, PricePerNight = 699.99m, IsAvailable = true, MaxOccupancy = 4, EntityStatusId = 1, CreatedAt = now},
                new Room { Id = 14, HotelId = 9, RoomNumber = "1202", RoomTypeId = 3, PricePerNight = 699.99m, IsAvailable = false, MaxOccupancy = 4, EntityStatusId = 1, CreatedAt = now},

                // Marina Bay Sands (ID 11)
                new Room { Id = 15, HotelId = 11, RoomNumber = "2501", RoomTypeId = 4, PricePerNight = 999.99m, IsAvailable = true, MaxOccupancy = 3, EntityStatusId = 1, CreatedAt = now},

                // The Peninsula (ID 12)
                new Room { Id = 16, HotelId = 12, RoomNumber = "601", RoomTypeId = 2, PricePerNight = 449.99m, IsAvailable = true, MaxOccupancy = 2, EntityStatusId = 1, CreatedAt = now},

                // Bellagio (ID 14)
                new Room { Id = 17, HotelId = 14, RoomNumber = "1801", RoomTypeId = 3, PricePerNight = 599.99m, IsAvailable = true, MaxOccupancy = 4, EntityStatusId = 1, CreatedAt = now},

                // Atlantis (ID 15)
                new Room { Id = 18, HotelId = 15, RoomNumber = "Royal Suite", RoomTypeId = 5, PricePerNight = 2499.99m, IsAvailable = true, MaxOccupancy = 6, EntityStatusId = 1, CreatedAt = now},

                // Hilton Tokyo (ID 18)
                new Room { Id = 19, HotelId = 18, RoomNumber = "901", RoomTypeId = 1, PricePerNight = 239.99m, IsAvailable = true, MaxOccupancy = 2, EntityStatusId = 1, CreatedAt = now},

                // Hotel Arts (ID 20)
                new Room { Id = 20, HotelId = 20, RoomNumber = "1501", RoomTypeId = 2, PricePerNight = 429.99m, IsAvailable = true, MaxOccupancy = 3, EntityStatusId = 1, CreatedAt = now}
            );
        }


        public override int SaveChanges()
        {
            UpdateAuditFields();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditFields();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateAuditFields()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is IEntity && (
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified));

            var now = DateTime.UtcNow;

            foreach (var entityEntry in entries)
            {

                ((IEntity)entityEntry.Entity).UpdatedAt = now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((IEntity)entityEntry.Entity).CreatedAt = now;
                }

                // You might want to set CreatedBy and UpdatedBy here as well,
                // but you'll need to implement a way to get the current user's ID
            }
        }
    }
}
