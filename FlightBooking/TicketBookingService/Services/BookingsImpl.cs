using TicketBookingService.DBContext;
using TicketBookingService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketBookingService.Services
{
    public class BookingsImpl : IBookingInterface
    {
        public ApplicationBookDbcontext _bookingDbContext;
        public BookingsImpl(ApplicationBookDbcontext bookingDbContext)
        {
            _bookingDbContext = bookingDbContext;
        }

        public IEnumerable<Bookings> GetBookings()
        {
            return _bookingDbContext.tblBookings.ToList();
        }

        public void Insert(Bookings bookings)
        {
            _bookingDbContext.tblBookings.Add(bookings);
            _bookingDbContext.SaveChanges();
        }

        public void UpdateBooking(Bookings bookings)
        {
            _bookingDbContext.Entry(bookings).State = EntityState.Modified;
            _bookingDbContext.SaveChanges();
        }
    }
}
