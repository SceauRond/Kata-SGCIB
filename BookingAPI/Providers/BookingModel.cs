using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingAPI.Providers
{
    public class BookingModel
    {
        public int RoomId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public int StartingHour { get; set; }
        public int EndingHour { get; set; }
    }
}
