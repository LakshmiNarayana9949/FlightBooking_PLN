using UserDetailsService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace UserDetailsService.DBContext
{
    public class UserDetailsDbContext : DbContext
    {
        public UserDetailsDbContext(DbContextOptions<UserDetailsDbContext> options):base(options)
        {

        }

        public DbSet<UserModel> Users { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 

        }

    }
}
