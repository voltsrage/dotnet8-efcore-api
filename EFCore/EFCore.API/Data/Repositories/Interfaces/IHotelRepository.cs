using EFCore.API.Dtos.Hotels;
using EFCore.API.Entities;
using EFCore.API.Models.Pagination;

namespace EFCore.API.Data.Repositories.Interfaces
{
    /// <summary>
    /// Interface for Hotel Repository
    /// </summary>
    public interface IHotelRepository
    {
        /// <summary>
        /// Get all hotels
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<PaginatedResult<Hotel>> GetAllAsync(PaginationRequest pagination, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get all hotels with their accompanying rooms
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<PaginatedResult<HotelWithRoomsDto>> GetHotelsWithRoomsAsync(PaginationRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get hotel by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Hotel?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get hotel by name
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Hotel?> GetByNameAsync(string name, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get hotel by address and name
        /// </summary>
        /// <param name="name"></param>
        /// <param name="address"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Hotel?> GetHotelByNameAndAddress(string name, string address, CancellationToken cancellationToken = default);

        /// <summary>
        /// Create a new hotel
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns></returns>
        Task<Hotel> CreateAsync(Hotel hotel);

        /// <summary>
        /// Create a new hotel with rooms
        /// </summary>
        /// <param name="hotel"></param>
        /// <param name="rooms"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Hotel> CreateHotelWithRooms(Hotel hotel, List<Room> rooms, CancellationToken cancellationToken = default);

        /// <summary>
        /// Create multiple hotels
        /// </summary>
        /// <param name="hotels"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<Hotel>> CreateBatchHotels(List<Hotel> hotels, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update a hotel
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(Hotel hotel);

        /// <summary>
        /// Delete a hotel
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
