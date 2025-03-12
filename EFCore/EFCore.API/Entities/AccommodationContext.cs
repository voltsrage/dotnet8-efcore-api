using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EFCore.API.Entities;

public partial class AccommodationContext : DbContext
{
    public AccommodationContext()
    {
    }

    public AccommodationContext(DbContextOptions<AccommodationContext> options)
        : base(options)
    {
    }

    public virtual DbSet<EntityStatus> EntityStatuses { get; set; }

    public virtual DbSet<Hotel> Hotels { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<RoomType> RoomTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-KPOICH0;Initial Catalog=Accommodation;Trusted_Connection=True;Connect Timeout=3000;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EntityStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EntitySt__3214EC076890AF3E");

            entity.ToTable("EntityStatus");

            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Hotel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Hotels__3214EC07820F56BD");

            entity.HasIndex(e => e.EntityStatusId, "IX_Hotels_EntityStatusId");

            entity.HasIndex(e => e.Name, "IX_Hotels_Name");

            entity.HasIndex(e => new { e.Name, e.Address }, "IX_Hotels_Name_Address")
                .IsUnique()
                .HasFilter("([EntityStatusId]=(1))");

            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.EntityStatusId).HasDefaultValue(1);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Rooms__3214EC07CEEB24E5");

            entity.HasIndex(e => e.EntityStatusId, "IX_Rooms_EntityStatusId");

            entity.HasIndex(e => e.HotelId, "IX_Rooms_HotelId");

            entity.HasIndex(e => new { e.HotelId, e.RoomNumber }, "IX_Rooms_HotelId_RoomNumber")
                .IsUnique()
                .HasFilter("([EntityStatusId]=(1))");

            entity.HasIndex(e => e.IsAvailable, "IX_Rooms_IsAvailable");

            entity.HasIndex(e => e.RoomTypeId, "IX_Rooms_RoomTypeId");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EntityStatusId).HasDefaultValue(1);
            entity.Property(e => e.IsAvailable).HasDefaultValue(true);
            entity.Property(e => e.PricePerNight).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.RoomNumber).HasMaxLength(20);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Hotel).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.HotelId)
                .HasConstraintName("FK_Rooms_Hotels");

            entity.HasOne(d => d.RoomType).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.RoomTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Rooms_RoomTypes");
        });

        modelBuilder.Entity<RoomType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RoomType__3214EC07EA30BE1D");

            entity.HasIndex(e => e.EntityStatusId, "IX_RoomType_EntityStatusId");

            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.EntityStatusId).HasDefaultValue(1);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
