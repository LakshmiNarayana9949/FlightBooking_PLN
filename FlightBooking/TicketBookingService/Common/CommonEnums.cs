using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketBookingService.Common
{
    public class CommonEnums
    {
        public enum BookingStatus { Booked = 1, Cancel = 0 }
        public enum SeatType { BusinessClass = 1, NonBusinessClass = 0}
    }
}
