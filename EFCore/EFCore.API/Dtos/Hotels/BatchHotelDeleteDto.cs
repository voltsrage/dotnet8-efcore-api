namespace EFCore.API.Dtos.Hotels
{
    public class BatchHotelDeleteDto
    {
        public List<int> HotelIds { get; set; } = new List<int>();
    }
}
