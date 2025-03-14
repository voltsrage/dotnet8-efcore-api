namespace EFCore.API.Dtos.Hotels
{
    public class BatchHotelCreateDto
    {
        public List<HotelCreateDto> Hotels { get; set; } = new List<HotelCreateDto>();
    }
}
