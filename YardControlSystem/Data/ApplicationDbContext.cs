using YardControlSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YardControlSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Warehouse> Warehouses { get; set; }

        public DbSet<Ramp> Ramps { get; set; }

        public DbSet<Operation> Operations { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
