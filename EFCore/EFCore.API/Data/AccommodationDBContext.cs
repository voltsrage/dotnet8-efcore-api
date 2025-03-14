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
