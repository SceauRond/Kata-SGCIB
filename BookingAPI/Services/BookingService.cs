using BookingAPI.Data;
using BookingAPI.Models;
using BookingAPI.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingAPI.Services
{
    public class BookingService
    {
        private readonly BookingProvider _bookingProvider;

        public BookingService(BookingProvider bookingProvider)
        {
            _bookingProvider = bookingProvider;
        }

        public async Task<bool> IsBookingAvailable(Booking booking)
        {
            var bookings = await _bookingProvider.GetAllBookingAsync();
            var startingHour = booking.StartingHour;
            var endingHour = booking.EndingHour;
            var date = booking.Date;
            var roomId = booking.Room.Id;

            if (startingHour == endingHour)
                return false;

            if (bookings
                .Where(b => b.Date.Date == date.Date && b.Room.Id == roomId)
                .Any(b => (startingHour >= b.StartingHour && startingHour < b.EndingHour)
                 || endingHour > b.StartingHour && endingHour <= b.EndingHour))
            {
                return false;
            }
            return true;
        }
    }
}
