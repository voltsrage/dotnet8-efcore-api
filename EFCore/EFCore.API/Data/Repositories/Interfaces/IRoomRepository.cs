using EFCore.API.Entities;

namespace EFCore.API.Data.Repositories.Interfaces
{
    /// <summary>
    /// Interface for Room Repository
    /// </summary>
    public interface IRoomRepository
    {
        /// <summary>
        /// Get all rooms
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<Room>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Get room by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Room?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get room by room number and hotel id
        /// </summary>
        /// <param name="roomNumber"></param>
        /// <param name="hotelId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Room?> GetByRoomNumberAndHotelIdAsync(string roomNumber, int hotelId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get room by hotel id
        /// </summary>
        /// <param name="hotelId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<Room>> GetByHotelIdAsync(int hotelId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Create a new room
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        Task<Room> CreateAsync(Room room);

        /// <summary>
        /// Update a room
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(Room room);

        /// <summary>
        /// Update room availability    
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isAvailable"></param>
        /// <returns></returns>
        Task<bool> UpdateAvailabilityAsync(int id, bool isAvailable);

        /// <summary>
        /// Get all room types
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<RoomType>> GetAllRoomTypesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete a room
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);

    }
}
