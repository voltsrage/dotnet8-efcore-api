namespace EFCore.API.Dtos.Rooms
{
    public class RoomUpdateDto
    {
        public string RoomNumber { get; set; } = string.Empty;
        public int RoomTypeId { get; set; }
        public decimal PricePerNight { get; set; }
        public bool IsAvailable { get; set; }
        public int MaxOccupancy { get; set; }
    }
}
