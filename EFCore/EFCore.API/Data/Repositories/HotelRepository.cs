﻿using EFCore.API.Data.Repositories.Interfaces;
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
    }
}
