﻿using EFCore.API.Dtos.Hotels;
using EFCore.API.Models;
using EFCore.API.Models.Pagination;
using EFCore.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EFCore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelService _hotelService;
        private readonly ILogger<HotelsController> _logger;

        public HotelsController(
            IHotelService hotelService,
            ILogger<HotelsController> logger)
        {
            _hotelService = hotelService ?? throw new ArgumentException(nameof(hotelService));
            _logger = logger ?? throw new ArgumentException(nameof(logger));
        }

        /// <summary>
        /// Get all hotels
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(Response<PaginatedResult<HotelResponseDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetHotels(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var result = new Response<PaginatedResult<HotelResponseDto>>();

            try
            {
                result = await _hotelService.GetAllAsync(page, pageSize, cancellationToken);

                if (result.StatusCode != null)
                {
                    return StatusCode(result.StatusCode.Value, result);
                }
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during hotels retrieval");
                result.ErrorMessage = ex.Message;
                throw;
            }
        }

        /// <summary>
        /// Get hotel by id
        /// </summary>
        /// <param name="hotelId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{hotelId}")]
        [ProducesResponseType(typeof(Response<HotelResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetHotelById(int hotelId, CancellationToken cancellationToken = default)
        {
            var result = new Response<HotelResponseDto>();
            try
            {
                result = await _hotelService.GetByIdAsync(hotelId, cancellationToken);
                if (result.StatusCode != null)
                {
                    return StatusCode(result.StatusCode.Value, result);
                }
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during hotel retrieval");
                result.ErrorMessage = ex.Message;
                throw;
            }
        }

        [HttpGet("name/{name}")]
        [ProducesResponseType(typeof(Response<HotelResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetHotelByName(string name, CancellationToken cancellationToken = default)
        {
            var result = new Response<HotelResponseDto>();
            try
            {
                result = await _hotelService.GetByNameAsync(name, cancellationToken);
                if (result.StatusCode != null)
                {
                    return StatusCode(result.StatusCode.Value, result);
                }
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during hotel retrieval");
                result.ErrorMessage = ex.Message;
                throw;
            }
        }

        /// <summary>
        /// Create a new hotel
        /// </summary>
        /// <param name="hotel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Response<HotelResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateHotel([FromBody] HotelCreateDto hotel, CancellationToken cancellationToken = default)
        {
            var result = new Response<HotelResponseDto>();
            try
            {
                result = await _hotelService.CreateAsync(hotel);
                if (result.StatusCode != null)
                {
                    return StatusCode(result.StatusCode.Value, result);
                }
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during hotel creation");
                result.ErrorMessage = ex.Message;
                throw;
            }
        }

        /// <summary>
        /// Update a hotel
        /// </summary>
        /// <param name="hotelId"></param>
        /// <param name="hotel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("{hotelId}")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateHotel(int hotelId, [FromBody] HotelUpdateDto hotel, CancellationToken cancellationToken = default)
        {
            var result = new Response<bool>();
            try
            {
                result = await _hotelService.UpdateAsync(hotelId, hotel);
                if (result.StatusCode != null)
                {
                    return StatusCode(result.StatusCode.Value, result);
                }
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during hotel update");
                result.ErrorMessage = ex.Message;
                throw;
            }
        }

        /// <summary>
        /// Delete a hotel
        /// </summary>
        /// <param name="hotelId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("{hotelId}")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteHotel(int hotelId, CancellationToken cancellationToken = default)
        {
            var result = new Response<bool>();
            try
            {
                result = await _hotelService.DeleteAsync(hotelId, cancellationToken);
                if (result.StatusCode != null)
                {
                    return StatusCode(result.StatusCode.Value, result);
                }
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during hotel deletion");
                result.ErrorMessage = ex.Message;
                throw;
            }
        }
    }
}
