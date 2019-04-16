using BookingAPI.Data;
using BookingAPI.Models;
using BookingAPI.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingAPI.Controllers
{
    [Route("api/booking")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly BookingProvider _provider;

        public BookingController(BookingContext context)
        {
            _provider = new BookingProvider(context);
        }


        [HttpGet]
        [SwaggerResponse(200, "All bookings retrieved", typeof(Booking[]))]
        public async Task<IActionResult> GetAll()
        {
            var bookings = await _provider.GetAllBookingAsync();
            return Ok(bookings.ToArray());
        }

        [HttpGet("{id}")]
        [SwaggerResponse(200, "Booking retrieved", typeof(Booking))]
        [SwaggerResponse(404, "booking not found")]
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            var booking = await _provider.GetBooking(id);
            if (booking == null)
                return NotFound();
            return Ok(booking);
        }

        [HttpPost]
        [SwaggerResponse(200, "Booking created", typeof(Booking))]
        [SwaggerResponse(409, "Booking unavailable", typeof(IEnumerable<Tuple<int, int>>))]
        public async Task<IActionResult> Post([FromBody] BookingModel booking)
        {
            var isBookingSaved = await _provider.SaveBookingAsync(booking);
            if (isBookingSaved)
                return Ok(booking);
            else
                return Conflict(await _provider.GetAvailableBookingsAsync(booking.Date, booking.RoomId));
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(200, "Booking deleted")]
        [SwaggerResponse(404, "booking not found")]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _provider.DeleteBooking(id);

            if (isDeleted)
                return Ok();
            return NotFound();

        }
    }
}
