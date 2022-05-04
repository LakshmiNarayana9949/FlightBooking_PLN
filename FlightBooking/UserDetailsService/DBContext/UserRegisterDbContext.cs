using UserDetailsService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace UserDetailsService.DBContext
{
    public class UserRegisterDbContext : DbContext
    {
        public UserRegisterDbContext(DbContextOptions<UserRegisterDbContext> options):base(options)
        {

        }

        public DbSet<UserModel> UserRegistor { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 

        }

    }
}
