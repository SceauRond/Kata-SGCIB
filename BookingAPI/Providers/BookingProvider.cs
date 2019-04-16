using BookingAPI.Data;
using BookingAPI.Models;
using BookingAPI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingAPI.Providers
{
    public class BookingProvider
    {
        private readonly BookingContext _context;
        private readonly BookingService _service;

        public BookingProvider(BookingContext context)
        {
            _context = context;
            _service = new BookingService(this);
        }

        public async Task<IQueryable<Booking>> GetAllBookingAsync()
        {
            var bookings = await _context.Bookings.Include(o => o.Room).Include(o => o.User).ToListAsync();
            return bookings.AsQueryable();
        }

        public async Task<Booking> GetBooking(int id)
        {
            return await _context.Bookings
                .Include(b => b.Room)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<Tuple<int, int>>> GetAvailableBookingsAsync(DateTime date, int roomId)
        {
            var bookings = await _context.Bookings.Where(b => b.Date.Date == date.Date && b.Room.Id == roomId).ToListAsync();
            var availableBookings = new List<Tuple<int, int>>();

            for (int i = 0; i < 24; i++)
            {
                if (!bookings.Any(b => (i >= b.StartingHour && i < b.EndingHour)
                 || i + 1 > b.StartingHour && i + 1 <= b.EndingHour))
                    availableBookings.Add(Tuple.Create(i, i + 1));
            }
            return availableBookings;
        }

        public async Task<bool> SaveBookingAsync(BookingModel bookingModel)
        {
            var booking = new Booking()
            {
                Date = bookingModel.Date,
                EndingHour = bookingModel.EndingHour,
                StartingHour = bookingModel.StartingHour,
            };
            booking.Room = await _context.Rooms.FirstAsync(r => r.Id == bookingModel.RoomId);
            booking.User = await _context.Users.FirstAsync(u => u.Id == bookingModel.UserId);

            var isBookingValid = await _service.IsBookingAvailable(booking);
            if (isBookingValid)
            {
                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync();
            }
            return isBookingValid;
        }

        public async Task<bool> DeleteBooking(int id)
        {
            var bookings = _context.Bookings;
            var booking = await bookings.FirstOrDefaultAsync(b => b.Id == id);

            if (booking == null)
                return false;

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
