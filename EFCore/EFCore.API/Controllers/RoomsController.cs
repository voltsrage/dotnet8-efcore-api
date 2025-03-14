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

        [HttpGet]
        [ProducesResponseType(typeof(Response<PaginatedResult<RoomResponseDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRooms(
            int page = 1,
            int pageSize = 10,
            string? searchTerm = null,
            CancellationToken cancellationToken = default)
        {
            var result = new Response<PaginatedResult<RoomResponseDto>>();
            try
            {
                var pagination = new PaginationRequest
                {
                    Page = page,
                    PageSize = pageSize,
                    SearchTerm = searchTerm
                };

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
