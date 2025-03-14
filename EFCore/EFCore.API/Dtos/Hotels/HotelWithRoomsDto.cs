using EFCore.API.Dtos.Rooms;

namespace EFCore.API.Dtos.Hotels
{
    public class HotelWithRoomsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public DateTime CreateAt { get; set; }
        public List<RoomResponseDto> Rooms { get; set; } = new List<RoomResponseDto>();
    }
}
