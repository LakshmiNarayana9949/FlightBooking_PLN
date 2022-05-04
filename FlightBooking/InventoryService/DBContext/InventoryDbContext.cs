using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InventoryService.Models;

namespace InventoryService.DBContext
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
        {

        }
        public DbSet<Inventorys> tblInventories { get; set; }
        public DbSet<AirLine> tblAirLine { get; set; }
        public DbSet<Flight> tblFlight { get; set; }

        protected override void OnModelCreating(ModelBuilder model)
        {
            
        }
    }
}
