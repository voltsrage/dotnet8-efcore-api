using EFCore.API.Data.Repositories.Interfaces;
using EFCore.API.Entities;
using EFCore.API.Enums.StandardEnums;
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
        public async Task<(IEnumerable<Hotel> items, int totalCount)> GetAllAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            if(page < 1)
            {
                page = 1;
            }

            if (pageSize < 1)
            {
                pageSize = 10;
            }

            var collections =  _context.Hotels.Where(h => h.EntityStatusId == (int)EntityStatusEnum.Active) as IQueryable<Hotel>;

            var totalCount = await collections.CountAsync();

            var collectionToReturn =  await _context.Hotels
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (collectionToReturn,totalCount);
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
    }
}
