using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EFCore.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EntityStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    EntityStatusId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EntityStatusId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoomTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    EntityStatusId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    RoomNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    RoomTypeId = table.Column<int>(type: "int", nullable: false),
                    PricePerNight = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    MaxOccupancy = table.Column<int>(type: "int", nullable: false),
                    EntityStatusId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rooms_RoomTypes_RoomTypeId",
                        column: x => x.RoomTypeId,
                        principalTable: "RoomTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EntityStatuses",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "EntityStatusId", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 3, 14, 9, 52, 37, 153, DateTimeKind.Utc).AddTicks(8689), null, "Entity is active and available", 1, "Active", null, null },
                    { 2, new DateTime(2025, 3, 14, 9, 52, 37, 153, DateTimeKind.Utc).AddTicks(8689), null, "Entity is temporarily unavailable", 1, "Inactive", null, null },
                    { 3, new DateTime(2025, 3, 14, 9, 52, 37, 153, DateTimeKind.Utc).AddTicks(8689), null, "Entity has been deleted for everyone", 1, "Deleted for everyone", null, null },
                    { 4, new DateTime(2025, 3, 14, 9, 52, 37, 153, DateTimeKind.Utc).AddTicks(8689), null, "Entity is awaiting activation/verification", 1, "Pending", null, null },
                    { 5, new DateTime(2025, 3, 14, 9, 52, 37, 153, DateTimeKind.Utc).AddTicks(8689), null, "Entity has been archived", 1, "Archived", null, null },
                    { 6, new DateTime(2025, 3, 14, 9, 52, 37, 153, DateTimeKind.Utc).AddTicks(8689), null, "Entity has been suspended", 1, "Suspended", null, null },
                    { 7, new DateTime(2025, 3, 14, 9, 52, 37, 153, DateTimeKind.Utc).AddTicks(8689), null, "Entity has been delete for me", 1, "Delete for me", null, null }
                });

            migrationBuilder.InsertData(
                table: "RoomTypes",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "EntityStatusId", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 3, 14, 9, 52, 37, 153, DateTimeKind.Utc).AddTicks(8689), null, "Standard room with basic amenities", 1, "Standard", null, null },
                    { 2, new DateTime(2025, 3, 14, 9, 52, 37, 153, DateTimeKind.Utc).AddTicks(8689), null, "Deluxe room with enhanced amenities and comfort", 1, "Deluxe", null, null },
                    { 3, new DateTime(2025, 3, 14, 9, 52, 37, 153, DateTimeKind.Utc).AddTicks(8689), null, "Suite with separate living area and bedroom", 1, "Suite", null, null },
                    { 4, new DateTime(2025, 3, 14, 9, 52, 37, 153, DateTimeKind.Utc).AddTicks(8689), null, "Executive room with business amenities and services", 1, "Executive", null, null },
                    { 5, new DateTime(2025, 3, 14, 9, 52, 37, 153, DateTimeKind.Utc).AddTicks(8689), null, "Penthouse suite, typically on the top floor with premium amenities", 1, "Penthouse", null, null },
                    { 6, new DateTime(2025, 3, 14, 9, 52, 37, 153, DateTimeKind.Utc).AddTicks(8689), null, "Family room with additional space for families", 1, "Family", null, null },
                    { 7, new DateTime(2025, 3, 14, 9, 52, 37, 153, DateTimeKind.Utc).AddTicks(8689), null, "Accessible room designed for guests with disabilities", 1, "Accessible", null, null },
                    { 8, new DateTime(2025, 3, 14, 9, 52, 37, 153, DateTimeKind.Utc).AddTicks(8689), null, "Single room designed for one person", 1, "Single", null, null },
                    { 9, new DateTime(2025, 3, 14, 9, 52, 37, 153, DateTimeKind.Utc).AddTicks(8689), null, "Double room with a queen or king-sized bed", 1, "Double", null, null },
                    { 10, new DateTime(2025, 3, 14, 9, 52, 37, 153, DateTimeKind.Utc).AddTicks(8689), null, "Twin room with two separate beds", 1, "Twin", null, null },
                    { 11, new DateTime(2025, 3, 14, 9, 52, 37, 153, DateTimeKind.Utc).AddTicks(8689), null, "Presidential suite, the most luxurious accommodation", 1, "Presidential", null, null },
                    { 12, new DateTime(2025, 3, 14, 9, 52, 37, 153, DateTimeKind.Utc).AddTicks(8689), null, "Villa or cottage separate from the main hotel building", 1, "Villa", null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_HotelId",
                table: "Rooms",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_RoomTypeId",
                table: "Rooms",
                column: "RoomTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntityStatuses");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Hotels");

            migrationBuilder.DropTable(
                name: "RoomTypes");
        }
    }
}
