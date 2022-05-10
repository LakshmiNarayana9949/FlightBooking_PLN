using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketBookingService.Events
{
    public class TicketBookingEvent
    {
        public string FlightNumber { get; set; }
        public string FromPlace { get; set; }
        public string ToPlace { get; set; }
        public int NumberOfTickets { set; get; }
        public int SeatType { set; get; }
    }
}
