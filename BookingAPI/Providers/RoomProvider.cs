using BookingAPI.Data;
using BookingAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingAPI.Providers
{
    public class RoomProvider
    {
        private readonly BookingContext _context;

        public RoomProvider(BookingContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<Room>> GetAllRoomsAsync()
        {
            var rooms = await _context.Rooms.ToListAsync();
            return rooms.AsQueryable();
        }

        public async Task<Room> GetRoom(int id)
        {
            return await _context.Rooms.FirstAsync(r => r.Id == id);
        }
    }
}
