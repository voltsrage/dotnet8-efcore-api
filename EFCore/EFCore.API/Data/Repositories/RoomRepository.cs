using EFCore.API.Data.Repositories.Interfaces;
using EFCore.API.Entities;
using EFCore.API.Enums.StandardEnums;
using Microsoft.EntityFrameworkCore;

namespace EFCore.API.Data.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly AccommodationDBContext _context;

        public RoomRepository(AccommodationDBContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task<Room> CreateAsync(Room room)
        {
            room.EntityStatusId = (int)EntityStatusEnum.Active;
            room.CreatedAt = DateTime.UtcNow;
            room.CreatedBy = 0;

            await _context.Rooms.AddAsync(room);

            await _context.SaveChangesAsync();

            return room;
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var room = await _context.Rooms
                .FirstOrDefaultAsync(r => r.Id == id && r.EntityStatusId == (int)EntityStatusEnum.Active, cancellationToken);

            if (room == null)
            {
                return false;
            }

            room.EntityStatusId = (int)EntityStatusEnum.DeletedForEveryone;
            room.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Room>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Rooms
                .Include(h => h.RoomType)
                .Where(r => r.EntityStatusId == (int)EntityStatusEnum.Active)
                .ToListAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<RoomType>> GetAllRoomTypesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.RoomTypes
                .Where(rt => rt.EntityStatusId == (int)EntityStatusEnum.Active)
                .ToListAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Room>> GetByHotelIdAsync(int hotelId, CancellationToken cancellationToken = default)
        {
            return await _context.Rooms
                .Include(h => h.RoomType)
                .Where(r => r.HotelId == hotelId && r.EntityStatusId == (int)EntityStatusEnum.Active)
                .ToListAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<Room?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Rooms
                .Include(h => h.RoomType)
                .Where(r => r.Id == id && r.EntityStatusId == (int)EntityStatusEnum.Active)
                .FirstOrDefaultAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<Room?> GetByRoomNumberAndHotelIdAsync(string roomNumber, int hotelId, CancellationToken cancellationToken = default)
        {
            return await _context.Rooms
                .Include(h => h.RoomType)
                .Where(r => r.RoomNumber == roomNumber && r.HotelId == hotelId && r.EntityStatusId == (int)EntityStatusEnum.Active)
                .FirstOrDefaultAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<bool> UpdateAsync(Room room)
        {
            var existingRoom = await _context.Rooms
                .FirstOrDefaultAsync(r => r.Id == room.Id && r.EntityStatusId == (int)EntityStatusEnum.Active);

            if (existingRoom == null)
            {
                return false;
            }

            existingRoom.RoomTypeId = room.RoomTypeId;
            existingRoom.RoomNumber = room.RoomNumber;
            existingRoom.PricePerNight = room.PricePerNight;
            existingRoom.IsAvailable = room.IsAvailable;
            existingRoom.MaxOccupancy = room.MaxOccupancy;
            existingRoom.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }

        /// <inheritdoc />
        public async Task<bool> UpdateAvailabilityAsync(int id, bool isAvailable)
        {
            var room = await _context.Rooms
                .FirstOrDefaultAsync(r => r.Id == id && r.EntityStatusId == (int)EntityStatusEnum.Active);

            if (room == null)
            {
                return false;
            }

            room.IsAvailable = isAvailable;
            room.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
