using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingAPI.Data;
using BookingAPI.Models;
using BookingAPI.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookingAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserProvider _provider;

        public UsersController(BookingContext context)
        {
            _provider = new UserProvider(context);
        }

        [HttpGet]
        [SwaggerResponse(200, "Ok", typeof(User[]))]
        public async Task<IActionResult> Get()
        {
            var users = await _provider.GetAllUsersAsync();
            return Ok(users.ToArray());
        }
    }
}