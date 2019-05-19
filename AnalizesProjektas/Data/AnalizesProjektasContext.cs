using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AnalizesProjektas.Models;

namespace AnalizesProjektas.Models
{
    public class AnalizesProjektasContext : DbContext
    {
        public AnalizesProjektasContext (DbContextOptions<AnalizesProjektasContext> options)
            : base(options)
        {
        }

        public DbSet<AnalizesProjektas.Models.GateTransportType> GateTransportTypes { get; set; }
        public DbSet<AnalizesProjektas.Models.WareHouse> WareHouse { get; set; }
        public DbSet<AnalizesProjektas.Models.Driver> Driver { get; set; }
        public DbSet<AnalizesProjektas.Models.Supplier> Supplier { get; set; }
        public DbSet<AnalizesProjektas.Models.Shipment> Shipments { get; set; }
        public DbSet<AnalizesProjektas.Models.SendingProduct> SendingProducts { get; set; }
        public DbSet<AnalizesProjektas.Models.Gate> Gate { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WareHouse>()
                .HasMany(c => c.Gates)
                .WithOne(e => e.WareHouse);
        }
    }
}
