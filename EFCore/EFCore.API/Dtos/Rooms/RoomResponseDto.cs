namespace EFCore.API.Dtos.Rooms
{
    public class RoomResponseDto
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public int RoomTypeId { get; set; }
        public string RoomTypeName { get; set; } = string.Empty;
        public decimal PricePerNight { get; set; }
        public bool IsAvailable { get; set; }
        public int MaxOccupancy { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
