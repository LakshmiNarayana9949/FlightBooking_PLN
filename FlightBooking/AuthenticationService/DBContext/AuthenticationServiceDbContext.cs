﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AuthenticationService.Models;

namespace AuthenticationService.DBContext
{
    public class AuthenticationServiceDbContext :DbContext
    {
        public AuthenticationServiceDbContext(DbContextOptions<AuthenticationServiceDbContext> options) : base(options)
        {

        }
        public DbSet<AuthenticationUser> AuthenticationUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
