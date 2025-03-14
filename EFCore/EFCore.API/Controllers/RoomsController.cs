using EFCore.API.Dtos.Rooms;
using EFCore.API.Models;
using EFCore.API.Models.Pagination;
using EFCore.API.Services;
using EFCore.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EFCore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _roomService;
        private readonly ILogger<RoomsController> _logger;

        public RoomsController(
            IRoomService roomService, 
            ILogger<RoomsController> logger)
        {
            _roomService = roomService ?? throw new ArgumentException(nameof(roomService));
            _logger = logger ?? throw new ArgumentException(nameof(logger));
        }

        /// <summary>
        /// Get all rooms
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchTerm"></param>
        /// <param name="sortColumn"></param>
        /// <param name="sortDirection"></param>
        /// <param name="hotelId">Optional hotel ID filter</param>
        /// <param name="roomTypeId">Optional room type ID filter</param> 
        /// <param name="minPrice">Minimum price per night</param>
        /// <param name="maxPrice">Maximum price per night</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(Response<PaginatedResult<RoomResponseDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRooms(
            int page = 1,
            int pageSize = 10,
            string? searchTerm = null,
            string? sortColumn = "Id",
            string? sortDirection = "asc",
            int? hotelId = null,
            int? roomTypeId = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            CancellationToken cancellationToken = default)
        {
            var result = new Response<PaginatedResult<RoomResponseDto>>();
            try
            {
                var pagination = new PaginationRequest
                {
                    Page = page,
                    PageSize = pageSize,
                    SearchTerm = searchTerm,
                    SortColumn = sortColumn,
                    SortDirection = sortDirection,
                    Filters = new Dictionary<string, string>()
                };

                if (hotelId.HasValue)
                    pagination.Filters.Add("hotelId", hotelId.Value.ToString());

                if (roomTypeId.HasValue)
                    pagination.Filters.Add("roomTypeId", roomTypeId.Value.ToString());

                if (minPrice.HasValue)
                    pagination.Filters.Add("pricePerNight__gte", minPrice.Value.ToString());

                if (maxPrice.HasValue)
                    pagination.Filters.Add("pricePerNight__lte", maxPrice.Value.ToString());

                result = await _roomService.GetAllAsync(pagination,cancellationToken);
                if (result.StatusCode != null)
                {
                    return StatusCode(result.StatusCode.Value, result);
                }
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during rooms retrieval");
                result.ErrorMessage = ex.Message;
                throw;
            }
        }

        /// <summary>
        /// Get room by id
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{roomId}")]
        [ProducesResponseType(typeof(Response<RoomResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRoomById(int roomId, CancellationToken cancellationToken)
        {
            var result = new Response<RoomResponseDto>();
            try
            {
                result = await _roomService.GetByIdAsync(roomId, cancellationToken);
                if (result.StatusCode != null)
                {
                    return StatusCode(result.StatusCode.Value, result);
                }
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during room retrieval");
                result.ErrorMessage = ex.Message;
                throw;
            }
        }

        /// <summary>
        /// Get rooms by hotel id
        /// </summary>
        /// <param name="hotelId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("hotel/{hotelId}")]
        [ProducesResponseType(typeof(Response<IEnumerable<RoomResponseDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRoomsByHotelId(int hotelId, CancellationToken cancellationToken)
        {
            var result = new Response<IEnumerable<RoomResponseDto>>();
            try
            {
                result = await _roomService.GetByHotelIdAsync(hotelId, cancellationToken);
                if (result.StatusCode != null)
                {
                    return StatusCode(result.StatusCode.Value, result);
                }
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during rooms retrieval");
                result.ErrorMessage = ex.Message;
                throw;
            }
        }

        /// <summary>
        /// Create a new room
        /// </summary>
        /// <param name="room"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Response<RoomResponseDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateRoom([FromBody] RoomCreateDto room, CancellationToken cancellationToken)
        {
            var result = new Response<RoomResponseDto>();
            try
            {
                result = await _roomService.CreateAsync(room);
                if (result.StatusCode != null)
                {
                    return StatusCode(result.StatusCode.Value, result);
                }
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during room creation");
                result.ErrorMessage = ex.Message;
                throw;
            }
        }

        /// <summary>
        /// Update a room
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="room"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("{roomId}")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateRoom(int roomId, [FromBody] RoomUpdateDto room, CancellationToken cancellationToken)
        {
            var result = new Response<bool>();
            try
            {
                result = await _roomService.UpdateAsync(roomId, room);
                if (result.StatusCode != null)
                {
                    return StatusCode(result.StatusCode.Value, result);
                }
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during room update");
                result.ErrorMessage = ex.Message;
                throw;
            }
        }

        /// <summary>
        /// Update room availability
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="isAvailable"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("{roomId}/availability")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateRoomAvailability(int roomId, [FromBody] bool isAvailable, CancellationToken cancellationToken)
        {
            var result = new Response<bool>();
            try
            {
                result = await _roomService.UpdateAvailabilityAsync(roomId, isAvailable);
                if (result.StatusCode != null)
                {
                    return StatusCode(result.StatusCode.Value, result);
                }
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during room availability update");
                result.ErrorMessage = ex.Message;
                throw;
            }
        }

        /// <summary>
        /// Get all room types
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("types")]
        [ProducesResponseType(typeof(Response<IEnumerable<RoomTypeResponseDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRoomTypes(CancellationToken cancellationToken)
        {
            var result = new Response<IEnumerable<RoomTypeResponseDto>>();
            try
            {
                result = await _roomService.GetAllRoomTypesAsync(cancellationToken);
                if (result.StatusCode != null)
                {
                    return StatusCode(result.StatusCode.Value, result);
                }
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during room types retrieval");
                result.ErrorMessage = ex.Message;
                throw;
            }
        }

        /// <summary>
        /// Delete a room
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("{roomId}")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteRoom(int roomId, CancellationToken cancellationToken)
        {
            var result = new Response<bool>();
            try
            {
                result = await _roomService.DeleteAsync(roomId);
                if (result.StatusCode != null)
                {
                    return StatusCode(result.StatusCode.Value, result);
                }
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during room deletion");
                result.ErrorMessage = ex.Message;
                throw;
            }
        }
    }
}
