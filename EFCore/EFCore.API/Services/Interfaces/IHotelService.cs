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
        /// <returns></returns>
        Task<Response<PaginatedResult<HotelResponseDto>>> GetAllAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default);

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
    }
}
