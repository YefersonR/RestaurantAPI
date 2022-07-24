using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Contexts
{
    public class RestaurantContext : DbContext
    {
        public DbSet<Mesa> Mesas { get; set; }
        public DbSet<Orden> Ordenes{ get; set; }
        public DbSet<Plato> Platos { get; set; }
        public DbSet<Ingrediente> Ingredientes{ get; set; }
        public RestaurantContext(DbContextOptions<RestaurantContext> options): base(options)
        {}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Primary Key
            modelBuilder.Entity<Mesa>()
                .HasKey(mesa=>mesa.Id);
            modelBuilder.Entity<Orden>()
                .HasKey(orden => orden.Id);
            modelBuilder.Entity<Plato>()
                .HasKey(plato => plato.Id);
            modelBuilder.Entity<Ingrediente>()
                .HasKey(ingrediente => ingrediente.Id);
            #endregion

            #region Relationship

            modelBuilder.Entity<Orden>()
            .HasOne<Mesa>(orden => orden.Mesa);  

            modelBuilder.Entity<Orden>()
                .HasMany<Plato>(platos => platos.Platos);

            modelBuilder.Entity<Plato>()
                .HasMany<Ingrediente>(ingrediente=> ingrediente.Ingrediente);


            #endregion

        }
    }
}
