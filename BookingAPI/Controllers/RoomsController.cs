using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingAPI.Data;
using BookingAPI.Models;
using BookingAPI.Providers;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookingAPI.Controllers
{
    [Route("api/rooms")]
    [ApiController]
    public class RoomsController : Controller
    {
        private readonly RoomProvider _provider;

        public RoomsController(BookingContext context)
        {
            _provider = new RoomProvider(context);
        }

        [HttpGet]
        [SwaggerResponse(200, "Ok", typeof(Room[]))]
        public async Task<IActionResult> Get()
        {
            var rooms = await _provider.GetAllRoomsAsync();
            return Ok(rooms.ToArray());
        }
    }
}