using TicketBookingService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketBookingService.DBContext
{
    public class ApplicationBookDbcontext :DbContext 
    {
        public ApplicationBookDbcontext(DbContextOptions<ApplicationBookDbcontext> options) : base(options)
        {

        }

        public DbSet<Bookings> tblBookings { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

    }
}
