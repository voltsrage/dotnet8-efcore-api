using Azure.Core;
using EFCore.API.Data.Repositories.Interfaces;
using EFCore.API.Dtos.Hotels;
using EFCore.API.Dtos.Rooms;
using EFCore.API.Entities;
using EFCore.API.Enums.StandardEnums;
using EFCore.API.Extensions;
using EFCore.API.Models.Pagination;
using Microsoft.EntityFrameworkCore;

namespace EFCore.API.Data.Repositories
{
    /// <summary>
    /// Hotel Repository
    /// </summary>
    public class HotelRepository : IHotelRepository
    {
        private readonly AccommodationDBContext _context;

        public HotelRepository(AccommodationDBContext context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }

        /// <inheritdoc />
        public async Task<Hotel> CreateAsync(Hotel hotel)
        {
            hotel.EntityStatusId = (int)EntityStatusEnum.Active;
            hotel.CreatedAt = DateTime.UtcNow;
            hotel.CreatedBy = 0;

            await _context.Hotels.AddAsync(hotel);

            await _context.SaveChangesAsync();

            return hotel;
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var hotel = await _context.Hotels
                .Where(h => h.Id == id && h.EntityStatusId == (int)EntityStatusEnum.Active)
                .FirstOrDefaultAsync(cancellationToken);

            if (hotel == null)
            {
                return false;
            }

            hotel.EntityStatusId = (int)EntityStatusEnum.DeletedForEveryone;
            hotel.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return true;

        }

        /// <inheritdoc />
        public async Task<PaginatedResult<Hotel>> GetAllAsync(PaginationRequest pagination, CancellationToken cancellationToken = default)
        {
            var query =  _context.Hotels
                .Where(h => h.EntityStatusId == (int)EntityStatusEnum.Active)
                .AsQueryable();

            if(!string.IsNullOrWhiteSpace(pagination.SearchTerm))
            {
                query = query.ApplySearch(pagination.SearchTerm, "Name","Country","City");
            }

            // Apply filters
            if (pagination.HasFilters)
            {
                query = query.ApplyFilters(pagination.Filters);
            }

            var paginatedHotels = await query.ToPaginatedResultAsync(pagination);

            return paginatedHotels;
        }

        /// <inheritdoc />
        public async Task<Hotel?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Hotels
                .Where(h => h.Id == id && h.EntityStatusId == (int)EntityStatusEnum.Active)
                .FirstOrDefaultAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<Hotel?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Hotels
                .Where(h => h.Name == name && h.EntityStatusId == (int)EntityStatusEnum.Active)
                .FirstOrDefaultAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<Hotel?> GetHotelByNameAndAddress(string name, string address, CancellationToken cancellationToken = default)
        {
            return await _context.Hotels
                .Where(h => h.Name == name && h.Address == address && h.EntityStatusId == (int)EntityStatusEnum.Active)
                .FirstOrDefaultAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<bool> UpdateAsync(Hotel hotel)
        {
            var existingHotel = await _context.Hotels
                .Where(h => h.Id == hotel.Id && h.EntityStatusId == (int)EntityStatusEnum.Active)
                .FirstOrDefaultAsync();

            if (existingHotel == null)
            {
                return false;
            }

            existingHotel.Name = hotel.Name;
            existingHotel.Address = hotel.Address;
            existingHotel.City = hotel.City;
            existingHotel.Country = hotel.Country;
            existingHotel.PhoneNumber = hotel.PhoneNumber;
            existingHotel.Email = hotel.Email; 
            existingHotel.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }       

        /// <inheritdoc />
        public async Task<PaginatedResult<HotelWithRoomsDto>> GetHotelsWithRoomsAsync(PaginationRequest request, CancellationToken cancellationToken = default)
        {
            var query = _context.Hotels
                .Where(h => h.EntityStatusId == (int)EntityStatusEnum.Active)
                .AsQueryable();

            // Apply search if provided
            if (request.HasSearch)
            {
                query = query.ApplySearch(request.SearchTerm, "Name", "City", "Country");
            }

            // Apply filters
            if (request.HasFilters)
            {
                query = query.ApplyFilters(request.Filters);
            }

            // Get paginated hotel ids
            var (paginatedQuery, totalCount) = await query.ApplyPaginationAsync(request);
            var hotelIds = await paginatedQuery.Select(h => h.Id).ToListAsync(cancellationToken);

            // Now get full hotel data with rooms

            var hotelsWithRooms = await _context.Hotels              
                .Where(h => hotelIds.Contains(h.Id))
                .Include(h => h.Rooms.Where(r => r.EntityStatusId == (int)EntityStatusEnum.Active))
                .ThenInclude(r => r.RoomType)
                .ToListAsync(cancellationToken);

            var hotelDtos = hotelsWithRooms.Select(MapToHotelWithRoomsDto).ToList();

            var result = new PaginatedResult<HotelWithRoomsDto>
            (   hotelDtos,
                totalCount,
                request.Page,
                request.PageSize
            );

            return result;
        }

        private static HotelWithRoomsDto MapToHotelWithRoomsDto(Hotel hotel)
        {
            return new HotelWithRoomsDto
            {
                Id = hotel.Id,
                Name = hotel.Name,
                Address = hotel.Address,
                City = hotel.City,
                Country = hotel.Country,
                PhoneNumber = hotel.PhoneNumber,
                Email = hotel.Email,
                CreatedAt = hotel.CreatedAt,
                Rooms = hotel.Rooms?.Select(r => new RoomResponseDto
                {
                    Id = r.Id,
                    HotelId = r.HotelId,
                    RoomNumber = r.RoomNumber,
                    RoomTypeId = r.RoomTypeId,
                    RoomTypeName = r.RoomType?.Name ?? string.Empty,
                    PricePerNight = r.PricePerNight,
                    IsAvailable = r.IsAvailable,
                    MaxOccupancy = r.MaxOccupancy,
                    CreatedAt = r.CreatedAt
                }).ToList() ?? new List<RoomResponseDto>()
            };
        }

        /// <inheritdoc />
        public async Task<Hotel> CreateHotelWithRooms(Hotel hotel, List<Room> rooms, CancellationToken cancellationToken = default)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                hotel.Rooms = new List<Room>();
                hotel.CreatedBy = 0;

                await _context.Hotels.AddAsync(hotel);
                await _context.SaveChangesAsync();

                // Now add the rooms with the correct HotelId

                if (rooms.Any())
                {
                    foreach (var room in rooms)
                    {
                        room.HotelId = hotel.Id;
                        room.CreatedBy = 0;
                    }

                    await _context.Rooms.AddRangeAsync(rooms);
                    await _context.SaveChangesAsync();
                }

                // Commit the transaction
                await transaction.CommitAsync(cancellationToken);

                return hotel;
            }
            catch (Exception)
            {
                // Rollback the transaction if any error occurs
                await transaction.RollbackAsync();
                throw; // Re-throw the exception to handle it at a higher level
            }
        }

        // <inheritdoc />
        public async Task<List<Hotel>> CreateBatchHotels(List<Hotel> hotels, CancellationToken cancellationToken = default)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                _context.Hotels.AddRange(hotels);

                await _context.SaveChangesAsync();

                // Commit the transaction
                await transaction.CommitAsync(cancellationToken);

                return hotels;
            }
            catch (Exception)
            {
                // Rollback the transaction if any error occurs
                await transaction.RollbackAsync();
                throw; // Re-throw the exception to handle it at a higher level
            }
        }
    }
}
