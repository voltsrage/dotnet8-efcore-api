using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EFCore.API.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedValuesForHotelsAndRooms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RoomNumber",
                table: "Rooms",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.UpdateData(
                table: "EntityStatuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101));

            migrationBuilder.UpdateData(
                table: "EntityStatuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101));

            migrationBuilder.UpdateData(
                table: "EntityStatuses",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101));

            migrationBuilder.UpdateData(
                table: "EntityStatuses",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101));

            migrationBuilder.UpdateData(
                table: "EntityStatuses",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101));

            migrationBuilder.UpdateData(
                table: "EntityStatuses",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101));

            migrationBuilder.UpdateData(
                table: "EntityStatuses",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101));

            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "Id", "Address", "City", "Country", "CreatedAt", "CreatedBy", "Email", "EntityStatusId", "Name", "PhoneNumber", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, "123 Park Avenue", "New York", "United States", new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101), null, "reservations@grandhyatt.com", 1, "Grand Hyatt", "+1-212-555-1234", null, null },
                    { 2, "50 Central Park South", "New York", "United States", new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101), null, "info@ritzcarlton.com", 1, "Ritz-Carlton", "+1-212-555-2345", null, null },
                    { 3, "Jumeirah Beach Road", "Dubai", "United Arab Emirates", new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101), null, "reservations@burjalarab.com", 1, "Burj Al Arab", "+971-4-555-6789", null, null },
                    { 4, "Strand", "London", "United Kingdom", new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101), null, "info@savoy.com", 1, "The Savoy", "+44-20-7555-1234", null, null },
                    { 5, "25 Avenue Montaigne", "Paris", "France", new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101), null, "reservations@plaza-athenee.com", 1, "Plaza Athénée", "+33-1-5555-6789", null, null },
                    { 6, "5 Connaught Road", "Hong Kong", "China", new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101), null, "mohkg@mandarinoriental.com", 1, "Mandarin Oriental", "+852-2555-1234", null, null },
                    { 7, "1 Beach Road", "Singapore", "Singapore", new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101), null, "singapore@raffles.com", 1, "Raffles Hotel", "+65-6555-1234", null, null },
                    { 8, "301 Park Avenue", "New York", "United States", new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101), null, "reservations@waldorfastoria.com", 1, "Waldorf Astoria", "+1-212-555-3456", null, null },
                    { 9, "57 E 57th Street", "New York", "United States", new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101), null, "reservations.nyc@fourseasons.com", 1, "Four Seasons", "+1-212-555-4567", null, null },
                    { 10, "2 Rue de la Paix", "Paris", "France", new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101), null, "paris.park@hyatt.com", 1, "Park Hyatt", "+33-1-5555-1234", null, null },
                    { 11, "10 Bayfront Avenue", "Singapore", "Singapore", new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101), null, "reservations@marinabaysands.com", 1, "Marina Bay Sands", "+65-6555-2345", null, null },
                    { 12, "Salisbury Road", "Hong Kong", "China", new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101), null, "phk@peninsula.com", 1, "The Peninsula", "+852-2555-2345", null, null },
                    { 13, "Brook Street", "London", "United Kingdom", new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101), null, "reservations@claridges.com", 1, "Claridges", "+44-20-7555-2345", null, null },
                    { 14, "3600 Las Vegas Blvd S", "Las Vegas", "United States", new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101), null, "reservations@bellagio.com", 1, "Bellagio", "+1-702-555-1234", null, null },
                    { 15, "Paradise Island", "Nassau", "Bahamas", new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101), null, "reservations@atlantis.com", 1, "Atlantis", "+1-242-555-1234", null, null },
                    { 16, "3355 Las Vegas Blvd S", "Las Vegas", "United States", new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101), null, "reservations@venetian.com", 1, "The Venetian", "+1-702-555-2345", null, null },
                    { 17, "1 Hamilton Place", "London", "United Kingdom", new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101), null, "london@intercontinental.com", 1, "InterContinental", "+44-20-7555-3456", null, null },
                    { 18, "6-6-2 Nishi-Shinjuku", "Tokyo", "Japan", new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101), null, "tokyo@hilton.com", 1, "Hilton Tokyo", "+81-3-5555-1234", null, null },
                    { 19, "3570 Las Vegas Blvd S", "Las Vegas", "United States", new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101), null, "reservations@caesars.com", 1, "Caesars Palace", "+1-702-555-3456", null, null },
                    { 20, "19-21 Marina", "Barcelona", "Spain", new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101), null, "arts@ritzcarlton.com", 1, "Hotel Arts", "+34-93-555-1234", null, null }
                });

            migrationBuilder.UpdateData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101));

            migrationBuilder.UpdateData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101));

            migrationBuilder.UpdateData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101));

            migrationBuilder.UpdateData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101));

            migrationBuilder.UpdateData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101));

            migrationBuilder.UpdateData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101));

            migrationBuilder.UpdateData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101));

            migrationBuilder.UpdateData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101));

            migrationBuilder.UpdateData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101));

            migrationBuilder.UpdateData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101));

            migrationBuilder.UpdateData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101));

            migrationBuilder.UpdateData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101));

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "EntityStatusId", "HotelId", "IsAvailable", "MaxOccupancy", "PricePerNight", "RoomNumber", "RoomTypeId", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101), null, 1, 1, true, 2, 299.99m, "101", 1, null, null },
                    { 2, new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101), null, 1, 1, true, 2, 299.99m, "102", 1, null, null },
                    { 3, new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101), null, 1, 1, true, 3, 459.99m, "201", 2, null, null },
                    { 4, new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101), null, 1, 2, true, 2, 699.99m, "A101", 2, null, null },
                    { 5, new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101), null, 1, 2, false, 4, 1299.99m, "A201", 3, null, null },
                    { 6, new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101), null, 1, 3, true, 4, 2999.99m, "Suite 1", 5, null, null },
                    { 7, new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101), null, 1, 3, true, 6, 3499.99m, "Suite 2", 5, null, null },
                    { 8, new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101), null, 1, 4, true, 2, 549.99m, "301", 2, null, null },
                    { 9, new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101), null, 1, 4, false, 2, 549.99m, "302", 2, null, null },
                    { 10, new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101), null, 1, 5, true, 3, 899.99m, "501", 3, null, null },
                    { 11, new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101), null, 1, 6, true, 2, 259.99m, "701", 1, null, null },
                    { 12, new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101), null, 1, 6, true, 4, 799.99m, "801", 4, null, null },
                    { 13, new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101), null, 1, 9, true, 4, 699.99m, "1201", 3, null, null },
                    { 14, new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101), null, 1, 9, false, 4, 699.99m, "1202", 3, null, null },
                    { 15, new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101), null, 1, 11, true, 3, 999.99m, "2501", 4, null, null },
                    { 16, new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101), null, 1, 12, true, 2, 449.99m, "601", 2, null, null },
                    { 17, new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101), null, 1, 14, true, 4, 599.99m, "1801", 3, null, null },
                    { 18, new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101), null, 1, 15, true, 6, 2499.99m, "Royal Suite", 5, null, null },
                    { 19, new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101), null, 1, 18, true, 2, 239.99m, "901", 1, null, null },
                    { 20, new DateTime(2025, 3, 14, 10, 57, 39, 900, DateTimeKind.Utc).AddTicks(9101), null, 1, 20, true, 3, 429.99m, "1501", 2, null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.AlterColumn<string>(
                name: "RoomNumber",
                table: "Rooms",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25);

            migrationBuilder.UpdateData(
                table: "EntityStatuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 14, 9, 52, 37, 153, DateTimeKind.Utc).AddTicks(8689));

            migrationBuilder.UpdateData(
                table: "EntityStatuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 14, 9, 52, 37, 153, DateTimeKind.Utc).AddTicks(8689));

            migrationBuilder.UpdateData(
                table: "EntityStatuses",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 14, 9, 52, 37, 153, DateTimeKind.Utc).AddTicks(8689));

            migrationBuilder.UpdateData(
                table: "EntityStatuses",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 14, 9, 52, 37, 153, DateTimeKind.Utc).AddTicks(8689));

            migrationBuilder.UpdateData(
                table: "EntityStatuses",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 14, 9, 52, 37, 153, DateTimeKind.Utc).AddTicks(8689));

            migrationBuilder.UpdateData(
                table: "EntityStatuses",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 14, 9, 52, 37, 153, DateTimeKind.Utc).AddTicks(8689));

            migrationBuilder.UpdateData(
                table: "EntityStatuses",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 14, 9, 52, 37, 153, DateTimeKind.Utc).AddTicks(8689));

            migrationBuilder.UpdateData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 14, 9, 52, 37, 153, DateTimeKind.Utc).AddTicks(8689));

            migrationBuilder.UpdateData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 14, 9, 52, 37, 153, DateTimeKind.Utc).AddTicks(8689));

            migrationBuilder.UpdateData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 14, 9, 52, 37, 153, DateTimeKind.Utc).AddTicks(8689));

            migrationBuilder.UpdateData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 14, 9, 52, 37, 153, DateTimeKind.Utc).AddTicks(8689));

            migrationBuilder.UpdateData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 14, 9, 52, 37, 153, DateTimeKind.Utc).AddTicks(8689));

            migrationBuilder.UpdateData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 14, 9, 52, 37, 153, DateTimeKind.Utc).AddTicks(8689));

            migrationBuilder.UpdateData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 14, 9, 52, 37, 153, DateTimeKind.Utc).AddTicks(8689));

            migrationBuilder.UpdateData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 14, 9, 52, 37, 153, DateTimeKind.Utc).AddTicks(8689));

            migrationBuilder.UpdateData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 14, 9, 52, 37, 153, DateTimeKind.Utc).AddTicks(8689));

            migrationBuilder.UpdateData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 14, 9, 52, 37, 153, DateTimeKind.Utc).AddTicks(8689));

            migrationBuilder.UpdateData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 14, 9, 52, 37, 153, DateTimeKind.Utc).AddTicks(8689));

            migrationBuilder.UpdateData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 14, 9, 52, 37, 153, DateTimeKind.Utc).AddTicks(8689));
        }
    }
}
