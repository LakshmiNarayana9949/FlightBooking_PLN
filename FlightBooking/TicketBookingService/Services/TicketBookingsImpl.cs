using TicketBookingService.DBContext;
using TicketBookingService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketBookingService.Services
{
    public class TicketBookingsImpl : ITicketBookingInterface
    {
        public TicketBookingDbcontext _iTicketBookingDbContext;
        public TicketBookingsImpl(TicketBookingDbcontext bookingDbContext)
        {
            _iTicketBookingDbContext = bookingDbContext;
        }

        public IEnumerable<Ticket> GetAllTickets()
        {
            return _iTicketBookingDbContext.Tickets.ToList();
        }

        public void BookNewTicket(Ticket ticket)
        {
            _iTicketBookingDbContext.Tickets.Add(ticket);
            save();
        }

        public void UpdateTicket(Ticket ticket)
        {
            _iTicketBookingDbContext.Entry(ticket).State = EntityState.Modified;
            save();
        }

        public void save()
        {
            _iTicketBookingDbContext.SaveChanges();
        }
    }
}
