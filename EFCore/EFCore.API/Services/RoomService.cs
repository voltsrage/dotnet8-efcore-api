using AutoMapper;
using EFCore.API.Data.Repositories;
using EFCore.API.Data.Repositories.Interfaces;
using EFCore.API.Dtos.Hotels;
using EFCore.API.Dtos.Rooms;
using EFCore.API.Entities;
using EFCore.API.Enums;
using EFCore.API.Helpers;
using EFCore.API.Models;
using EFCore.API.Models.Pagination;
using EFCore.API.Services.Interfaces;
using EFCore.API.Validators;

namespace EFCore.API.Services
{
    /// <summary>
    /// Service for Room
    /// </summary>
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;
        private readonly IHelperFunctions _helperFunctions;

        public RoomService(
            IRoomRepository roomRepository,
            IMapper mapper,
            IHelperFunctions helperFunctions)
        {
            _roomRepository = roomRepository ?? throw new ArgumentException(nameof(roomRepository));
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
            _helperFunctions = helperFunctions ?? throw new ArgumentException(nameof(helperFunctions));
        }

        ///<inheritdoc/>
        public async Task<Response<RoomResponseDto>> CreateAsync(RoomCreateDto room)
        {
            var result = new Response<RoomResponseDto>();

            RoomCreateValidator validator = new();

            result = await _helperFunctions.ProcessValidation<RoomCreateDto,RoomResponseDto>(validator, room, result);

            if (!result.IsSuccess)
            {
                result.IsSuccess = false;
                result.StatusCode = StatusCodeEnum.BadRequest.Value;
                return result;
            }

            var existingRoom = await _roomRepository.GetByRoomNumberAndHotelIdAsync(room.RoomNumber, room.HotelId);

            if (existingRoom != null)
            {
                return Response<RoomResponseDto>.Failure(SystemCodeEnum.RoomAlreadyExists);
            }

            var newRoom = _mapper.Map<Room>(room);

            var createdRoom = await _roomRepository.CreateAsync(newRoom);

            return Response<RoomResponseDto>.Success(
                _mapper.Map<RoomResponseDto>(createdRoom));
        }

        ///<inheritdoc/>
        public async Task<Response<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var room = await _roomRepository.GetByIdAsync(id, cancellationToken);

            if (room == null)
            {
                return Response<bool>.Failure(SystemCodeEnum.RoomNotFound);
            }

            var deleted = await _roomRepository.DeleteAsync(id,cancellationToken);

            if (!deleted)
            {
                return Response<bool>.Failure(SystemCodeEnum.RoomDeleteFailed);
            }

            return Response<bool>.Success(true);
        }

        ///<inheritdoc/>
        public async Task<Response<PaginatedResult<RoomResponseDto>>> GetAllAsync(PaginationRequest pagination, CancellationToken cancellationToken = default)
        {
            var paginatedRooms = await _roomRepository.GetAllAsync(pagination, cancellationToken);

            var hotelDtos = paginatedRooms.Items.Select(hotel => _mapper.Map<RoomResponseDto>(hotel)).ToList();

            var result = new PaginatedResult<RoomResponseDto>(
                   hotelDtos,
                   paginatedRooms.TotalCount,
                   paginatedRooms.Page,
                   paginatedRooms.PageSize
               );

            return Response<PaginatedResult<RoomResponseDto>>.Success(result);
        }

        ///<inheritdoc/>
        public async Task<Response<IEnumerable<RoomTypeResponseDto>>> GetAllRoomTypesAsync(CancellationToken cancellationToken = default)
        {
            var roomTypes = await _roomRepository.GetAllRoomTypesAsync(cancellationToken);

            return Response<IEnumerable<RoomTypeResponseDto>>.Success(
                _mapper.Map<IEnumerable<RoomTypeResponseDto>>(roomTypes));
        }

        ///<inheritdoc/>
        public async Task<Response<IEnumerable<RoomResponseDto>>> GetByHotelIdAsync(int hotelId, CancellationToken cancellationToken = default)
        {
            var rooms = await _roomRepository.GetByHotelIdAsync(hotelId, cancellationToken);

            return Response<IEnumerable<RoomResponseDto>>.Success(
                _mapper.Map<IEnumerable<RoomResponseDto>>(rooms));
        }

        ///<inheritdoc/>
        public async Task<Response<RoomResponseDto?>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var room = await _roomRepository.GetByIdAsync(id, cancellationToken);

            if (room == null)
            {
                return Response<RoomResponseDto?>.Failure(SystemCodeEnum.RoomNotFound);
            }

            return Response<RoomResponseDto?>.Success(
                _mapper.Map<RoomResponseDto>(room));
        }

        ///<inheritdoc/>
        public async Task<Response<RoomResponseDto?>> GetByRoomNumberAndHotelIdAsync(string roomNumber, int hotelId, CancellationToken cancellationToken = default)
        {
            var room = await _roomRepository.GetByRoomNumberAndHotelIdAsync(roomNumber, hotelId, cancellationToken);

            if (room == null)
            {
                return Response<RoomResponseDto?>.Failure(SystemCodeEnum.RoomNotFound);
            }

            return Response<RoomResponseDto?>.Success(
                _mapper.Map<RoomResponseDto>(room));
        }

        ///<inheritdoc/>
        public async Task<Response<bool>> UpdateAsync(int roomId, RoomUpdateDto room)
        {
            var result = new Response<bool>();

            var existingRoom = await _roomRepository.GetByIdAsync(roomId);

            if (existingRoom == null)
            {
                return Response<bool>.Failure(SystemCodeEnum.RoomNotFound);
            }

            RoomUpdateValidator validator = new();

            result = await _helperFunctions.ProcessValidation<RoomUpdateDto, bool>(validator, room, result);

            if (!result.IsSuccess)
            {
                result.IsSuccess = false;
                result.StatusCode = StatusCodeEnum.BadRequest.Value;
                return result;
            }

            var updatedRoom = _mapper.Map<Room>(room);

            updatedRoom.Id = roomId;

            var updated = await _roomRepository.UpdateAsync(updatedRoom);

            if (!updated)
            {
                return Response<bool>.Failure(SystemCodeEnum.RoomUpdateFailed);
            }

            return Response<bool>.Success(true);

        }

        ///<inheritdoc/>
        public async Task<Response<bool>> UpdateAvailabilityAsync(int id, bool isAvailable)
        {
            var room = await _roomRepository.GetByIdAsync(id);

            if (room == null)
            {
                return Response<bool>.Failure(SystemCodeEnum.RoomNotFound);
            }

            var updated = await _roomRepository.UpdateAvailabilityAsync(id, isAvailable);

            if (!updated)
            {
                return Response<bool>.Failure(SystemCodeEnum.RoomUpdateFailed);
            }

            return Response<bool>.Success(true);
        }
    }
}
