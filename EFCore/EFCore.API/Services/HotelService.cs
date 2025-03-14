﻿using AutoMapper;
using EFCore.API.Data.Repositories.Interfaces;
using EFCore.API.Dtos.Hotels;
using EFCore.API.Entities;
using EFCore.API.Enums;
using EFCore.API.Helpers;
using EFCore.API.Models;
using EFCore.API.Models.Pagination;
using EFCore.API.Services.Interfaces;
using EFCore.API.Validators;

namespace EFCore.API.Services
{
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;
        private readonly IHelperFunctions _helperFunctions;

        public HotelService(
            IHotelRepository hotelRepository,
            IMapper mapper,
            IHelperFunctions helperFunctions)
        {
            _hotelRepository = hotelRepository ?? throw new ArgumentException(nameof(hotelRepository));
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
            _helperFunctions = helperFunctions ?? throw new ArgumentException(nameof(helperFunctions));
        }

        /// <inheritdoc />
        public async Task<Response<HotelResponseDto>> CreateAsync(HotelCreateDto hotel)
        {
            var result = new Response<HotelResponseDto>();

            HotelCreateValidator validator = new();

            result = await _helperFunctions.ProcessValidation<HotelCreateDto, HotelResponseDto>(validator, hotel, result);

            if (!result.IsSuccess)
            {
                result.IsSuccess = false;
                result.StatusCode = StatusCodeEnum.BadRequest.Value;
                return result;
            }

            var existingHotel = await _hotelRepository.GetHotelByNameAndAddress(hotel.Name, hotel.Address);

            if (existingHotel != null)
            {
                return Response<HotelResponseDto>.Failure(SystemCodeEnum.HotelAlreadyExists);
            }

            var hotelToCreate = _mapper.Map<Hotel>(hotel);

            var createdHotel = await _hotelRepository.CreateAsync(hotelToCreate);

            return Response<HotelResponseDto>.Created(_mapper.Map<HotelResponseDto>(createdHotel));
        }

        /// <inheritdoc />
        public async Task<Response<List<HotelResponseDto>>> CreateBatchHotels(BatchHotelCreateDto hotels, CancellationToken cancellationToken = default)
        {
            var result = new Response<List<HotelResponseDto>>();

            BatchHotelCreateValidator validator = new BatchHotelCreateValidator(new HotelCreateValidator());

            result = await  _helperFunctions.ProcessValidation<BatchHotelCreateDto, List<HotelResponseDto>>(validator, hotels, result);

            if (!result.IsSuccess)
            {
                result.IsSuccess = false;
                result.StatusCode = StatusCodeEnum.BadRequest.Value;
                return result;
            }

            foreach (var hotel in hotels.Hotels)
            {
                var existingHotel = await _hotelRepository.GetHotelByNameAndAddress(hotel.Name, hotel.Address);
                if (existingHotel != null)
                {
                    return Response<List<HotelResponseDto>>.Failure(SystemCodeEnum.HotelAlreadyExists);
                }
            }

            var hotelsToCreate = _mapper.Map<List<Hotel>>(hotels.Hotels);

            var createdHotels = await _hotelRepository.CreateBatchHotels(hotelsToCreate, cancellationToken);

            return Response<List<HotelResponseDto>>.Success(_mapper.Map<List<HotelResponseDto>>(createdHotels));
        }

        /// <inheritdoc />
        public async Task<Response<HotelResponseDto>> CreateHotelWithRooms(HotelWithRoomsCreateDto hotel, CancellationToken cancellationToken = default)
        {
            var result = new Response<HotelResponseDto>();

            HotelWithRoomsCreateValidator validator = new HotelWithRoomsCreateValidator(new RoomForHotelWithRoomCreateValidator());

            result = await _helperFunctions.ProcessValidation<HotelWithRoomsCreateDto, HotelResponseDto>(validator, hotel, result);

            if (!result.IsSuccess)
            {
                result.IsSuccess = false;
                result.StatusCode = StatusCodeEnum.BadRequest.Value;
                return result;
            }

            var existingHotel = await _hotelRepository.GetHotelByNameAndAddress(hotel.Name, hotel.Address);

            if (existingHotel != null)
            {
                return Response<HotelResponseDto>.Failure(SystemCodeEnum.HotelAlreadyExists);
            }

            var hotelToCreate = _mapper.Map<Hotel>(hotel);

            var createdHotel = await _hotelRepository.CreateHotelWithRooms(hotelToCreate, hotelToCreate.Rooms.ToList(), cancellationToken);

            return Response<HotelResponseDto>.Created(_mapper.Map<HotelResponseDto>(createdHotel));

        }

        /// <inheritdoc />
        public async Task<Response<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var hotel = await _hotelRepository.GetByIdAsync(id, cancellationToken);

            if (hotel == null)
            {
                return Response<bool>.Failure(SystemCodeEnum.HotelNotFound);
            }

            var isDeleted = await _hotelRepository.DeleteAsync(id,cancellationToken);

            if (!isDeleted)
            {
                return Response<bool>.Failure(SystemCodeEnum.HotelDeleteFailed);
            }

            return Response<bool>.Success(true);
        }

        /// <summary>
        /// Delete multiple hotels
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Response<BulkDeleteResult>> DeleteBatchAsync(BatchHotelDeleteDto ids, CancellationToken cancellationToken = default)
        {
            if(ids is null || !ids.HotelIds.Any())
            {
                return Response<BulkDeleteResult>.Failure(SystemCodeEnum.HotelNotFound);
            }

            var result = await _hotelRepository.DeleteBatchAsync(ids.HotelIds, cancellationToken);

            return Response<BulkDeleteResult>.Success(result);
         
        }

        /// <inheritdoc />
        public async Task<Response<PaginatedResult<HotelResponseDto>>> GetAllAsync(PaginationRequest pagination, CancellationToken cancellationToken = default)
        {
            var paginatedHotels = await _hotelRepository.GetAllAsync(pagination, cancellationToken);

            var hotelDtos = paginatedHotels.Items.Select(hotel => _mapper.Map<HotelResponseDto>(hotel)).ToList();

            var result = new PaginatedResult<HotelResponseDto>(
                   hotelDtos,
                   paginatedHotels.TotalCount,
                   paginatedHotels.Page,
                   paginatedHotels.PageSize
               );

            return Response<PaginatedResult<HotelResponseDto>>.Success(result);
        }

        /// <inheritdoc />
        public async Task<Response<HotelResponseDto?>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var hotel = await _hotelRepository.GetByIdAsync(id, cancellationToken);

            if (hotel == null)
            {
                return Response<HotelResponseDto?>.Failure(SystemCodeEnum.HotelNotFound);
            }

            return Response<HotelResponseDto?>.Success(_mapper.Map<HotelResponseDto>(hotel));
        }

        /// <inheritdoc />
        public async Task<Response<HotelResponseDto?>> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            var hotel = await _hotelRepository.GetByNameAsync(name, cancellationToken);

            if (hotel == null)
            {
                return Response<HotelResponseDto?>.Failure(SystemCodeEnum.HotelNotFound);
            }

            return Response<HotelResponseDto?>.Success(_mapper.Map<HotelResponseDto>(hotel));
        }

        /// <inheritdoc />
        public async Task<Response<HotelResponseDto?>> GetHotelByNameAndAddress(string name, string address, CancellationToken cancellationToken = default)
        {
            var hotel = await _hotelRepository.GetHotelByNameAndAddress(name, address, cancellationToken);

            if (hotel == null)
            {
                return Response<HotelResponseDto?>.Failure(SystemCodeEnum.HotelNotFound);
            }

            return Response<HotelResponseDto?>.Success(_mapper.Map<HotelResponseDto>(hotel));
        }

        /// <inheritdoc />
        public async Task<Response<PaginatedResult<HotelWithRoomsDto>>> GetHotelsWithRoomsAsync(PaginationRequest request, CancellationToken cancellationToken = default)
        {
            var paginatedHotels = await _hotelRepository.GetHotelsWithRoomsAsync(request, cancellationToken);

            return Response<PaginatedResult<HotelWithRoomsDto>>.Success(paginatedHotels);
        }

        /// <inheritdoc />
        public async Task<Response<bool>> UpdateAsync(int hotelId, HotelUpdateDto hotel)
        {
            var result = new Response<bool>();

            var existingHotel = await _hotelRepository.GetByIdAsync(hotelId);

            if (existingHotel == null)
            {
                return Response<bool>.Failure(SystemCodeEnum.HotelNotFound);
            }

            HotelUpdateValidator validator = new();

            result = await _helperFunctions.ProcessValidation<HotelUpdateDto,bool>(validator, hotel,result);

            if (!result.IsSuccess)
            {
                result.IsSuccess = false;
                result.StatusCode = StatusCodeEnum.BadRequest.Value;
                return result;
            }

            var hotelToUpdate = _mapper.Map<Hotel>(hotel);
            hotelToUpdate.Id = hotelId;

            var isUpdated = await _hotelRepository.UpdateAsync(hotelToUpdate);

            if (!isUpdated)
            {
                return Response<bool>.Failure(SystemCodeEnum.HotelUpdateFailed);
            }

            return Response<bool>.Success(true);
        }
    }
}
