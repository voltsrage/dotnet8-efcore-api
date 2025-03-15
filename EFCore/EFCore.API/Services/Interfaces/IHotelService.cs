using EFCore.API.Dtos.Hotels;
using EFCore.API.Entities;
using EFCore.API.Models;
using EFCore.API.Models.Pagination;

namespace EFCore.API.Services.Interfaces
{
    /// <summary>
    /// Interface for Hotel Service
    /// </summary>
    public interface IHotelService
    {
        /// <summary>
        /// Get all hotels
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Response<PaginatedResult<HotelResponseDto>>> GetAllAsync(PaginationRequest pagination, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get all hotels with their accompanying rooms
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Response<PaginatedResult<HotelWithRoomsDto>>> GetHotelsWithRoomsAsync(PaginationRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get hotel by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Response<HotelResponseDto?>> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get hotel by name
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Response<HotelResponseDto?>> GetByNameAsync(string name, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get hotel by address and name
        /// </summary>
        /// <param name="name"></param>
        /// <param name="address"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Response<HotelResponseDto?>> GetHotelByNameAndAddress(string name, string address, CancellationToken cancellationToken = default);

        /// <summary>
        /// Create a new hotel
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns></returns>
        Task<Response<HotelResponseDto>> CreateAsync(HotelCreateDto hotel);

        /// <summary>
        /// Create a new hotel with rooms
        /// </summary>
        /// <param name="hotel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Response<HotelResponseDto>> CreateHotelWithRooms(HotelWithRoomsCreateDto hotel, CancellationToken cancellationToken = default);

        /// <summary>
        /// Create multiple hotels
        /// </summary>
        /// <param name="hotels"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Response<List<HotelResponseDto>>> CreateBatchHotels(BatchHotelCreateDto hotels, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update a hotel
        /// </summary>
        /// <param name="hotelId"></param>
        /// <param name="hotel"></param>
        /// <returns></returns>
        Task<Response<bool>> UpdateAsync(int hotelId, HotelUpdateDto hotel);

        /// <summary>
        /// Delete a hotel
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Response<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete multiple hotels
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Response<BulkDeleteResult>> DeleteBatchAsync(BatchHotelDeleteDto ids, CancellationToken cancellationToken = default);
    }
}
