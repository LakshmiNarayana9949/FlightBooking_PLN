using TicketBookingService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketBookingService.Services
{
    public interface ITicketBookingInterface
    {
        void BookNewTicket(Ticket bookings);

        IEnumerable<Ticket> GetAllTickets();

        void UpdateTicket(Ticket bookings);
    }
}
