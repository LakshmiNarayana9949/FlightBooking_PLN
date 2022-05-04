using TicketBookingService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketBookingService.Services
{
    public interface IBookingInterface
    {
        void Insert(Bookings bookings);

        IEnumerable<Bookings> GetBookings();

        void UpdateBooking(Bookings bookings);
    }
}
