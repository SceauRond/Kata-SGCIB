using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingAPI.Models
{
    public class Booking : Model
    {
        [Required]
        public Room Room { get; set; }
        public User User { get; set; }
        public DateTime Date { get; set; }
        public int StartingHour { get; set; }
        public int EndingHour { get; set; }
    }
}
