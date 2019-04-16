using BookingAPI.Data;
using BookingAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingAPI.Providers
{
    public class UserProvider
    {
        private readonly BookingContext _context;

        public UserProvider(BookingContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<User>> GetAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            return users.AsQueryable();
        }

        public async Task<User> GetUser(int id)
        {
            return await _context.Users.FirstAsync(r => r.Id == id);
        }
    }
}
