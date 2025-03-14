using EFCore.API.Dtos.Rooms;
using EFCore.API.Entities;
using EFCore.API.Models;

namespace EFCore.API.Services.Interfaces
{
    /// <summary>
    /// Interface for Room Service
    /// </summary>
    public interface IRoomService
    {
        /// <summary>
        /// Get all rooms
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Response<IEnumerable<RoomResponseDto>>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Get room by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Response<RoomResponseDto?>> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get room by room number and hotel id
        /// </summary>
        /// <param name="roomNumber"></param>
        /// <param name="hotelId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Response<RoomResponseDto?>> GetByRoomNumberAndHotelIdAsync(string roomNumber, int hotelId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get room by hotel id
        /// </summary>
        /// <param name="hotelId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Response<IEnumerable<RoomResponseDto>>> GetByHotelIdAsync(int hotelId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Create a new room
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        Task<Response<RoomResponseDto>> CreateAsync(RoomCreateDto room);

        /// <summary>
        /// Update a room
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="room"></param>
        /// <returns></returns>
        Task<Response<bool>> UpdateAsync(int roomId, RoomUpdateDto room);

        /// <summary>
        /// Update room availability    
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isAvailable"></param>
        /// <returns></returns>
        Task<Response<bool>> UpdateAvailabilityAsync(int id, bool isAvailable);

        /// <summary>
        /// Get all room types
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Response<IEnumerable<RoomTypeResponseDto>>> GetAllRoomTypesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete a room
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Response<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
