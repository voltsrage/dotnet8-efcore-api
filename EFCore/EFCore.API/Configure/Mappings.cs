using AutoMapper;
using EFCore.API.Dtos.Hotels;
using EFCore.API.Dtos.Rooms;
using EFCore.API.Entities;

namespace EFCore.API.Configure
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<Hotel, HotelCreateDto>()
                .ReverseMap();

            CreateMap<Hotel, HotelUpdateDto>()
                .ReverseMap();

            CreateMap<Hotel, HotelResponseDto>()
                .ReverseMap();

            CreateMap<Room, RoomCreateDto>()
               .ReverseMap();

            CreateMap<Room, RoomUpdateDto>()
                .ReverseMap();
                
            CreateMap<Room, RoomResponseDto>()
                .ForMember(dest => dest.RoomTypeName, opt => opt.MapFrom(src => src.RoomType.Name))
                .ReverseMap();

            CreateMap<RoomType, RoomTypeResponseDto>()
                .ReverseMap();
        }
    }
}
