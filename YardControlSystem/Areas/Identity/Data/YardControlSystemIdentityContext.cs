using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YardControlSystem.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using YardControlSystem.Models;

namespace YardControlSystem.Data
{
    public class YardControlSystemIdentityContext : IdentityDbContext<User>
    {
        public YardControlSystemIdentityContext(DbContextOptions<YardControlSystemIdentityContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
        public DbSet<Warehouse> Warehouses { get; set; }

        public DbSet<Ramp> Ramps { get; set; }

        public DbSet<Operation> Operations { get; set; }

        public DbSet<Order> Orders { get; set; }
    }
}
