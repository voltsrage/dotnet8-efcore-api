namespace EFCore.API.Dtos.Rooms
{
    public class RoomCreateDto
    {
        public int HotelId { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public int RoomTypeId { get; set; }
        public decimal PricePerNight { get; set; }
        public bool IsAvailable { get; set; } = true;
        public int MaxOccupancy { get; set; }
    }
}
