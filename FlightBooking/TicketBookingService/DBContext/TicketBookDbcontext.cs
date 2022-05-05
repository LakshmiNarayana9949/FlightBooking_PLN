using TicketBookingService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketBookingService.DBContext
{
    public class TicketBookingDbcontext :DbContext 
    {
        public TicketBookingDbcontext(DbContextOptions<TicketBookingDbcontext> options) : base(options)
        {

        }

        public DbSet<Ticket> Tickets { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

    }
}
