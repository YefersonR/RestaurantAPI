using Core.Domain.Commons;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Contexts
{
    public class RestaurantContext : DbContext
    {
        public DbSet<Mesa> Mesas { get; set; }
        public DbSet<Orden> Ordenes{ get; set; }
        public DbSet<Plato> Platos { get; set; }
        public DbSet<PlatoIngredientes> PlatoIngredientes { get; set; }
        public DbSet<Ingrediente> Ingredientes{ get; set; }
        public DbSet<OrdenesPlatos> OrdenesPlatos { get; set; }

        public RestaurantContext(DbContextOptions<RestaurantContext> options): base(options)
        {}
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBase>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.CreatedBy = "";
                        break;
                    case EntityState.Modified:
                        entry.Entity.Updated = DateTime.Now;
                        entry.Entity.UpdatedBy = "";
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Primary Key
            modelBuilder.Entity<Mesa>()
                .HasKey(mesa=>mesa.Id);
            modelBuilder.Entity<OrdenesPlatos>()
                .HasKey(mesaorden => mesaorden.Id);
            modelBuilder.Entity<Orden>()
                .HasKey(orden => orden.Id);
            modelBuilder.Entity<Plato>()
                .HasKey(plato => plato.Id);
            modelBuilder.Entity<PlatoIngredientes>()
                .HasKey(platoIngredientes => platoIngredientes.Id);
            modelBuilder.Entity<OrdenesPlatos>()
             .HasKey(mesaOrdenes => mesaOrdenes.Id);
            modelBuilder.Entity<Ingrediente>()
                .HasKey(ingrediente => ingrediente.Id);
            modelBuilder.Entity<PlatoIngredientes>()
                .HasKey(ingrediente => ingrediente.Id);
            modelBuilder.Entity<Ingrediente>()
                .HasKey(ingrediente => ingrediente.Id);

            #endregion

            #region Relationship


            modelBuilder.Entity<Orden>()
                .HasMany<OrdenesPlatos>(Pi => Pi.Platos)
                .WithOne(p => p.Orden)
                .HasForeignKey(p => p.OrdenId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Plato>()
                .HasMany<OrdenesPlatos>(p => p.Ordens)
                .WithOne(p => p.Plato)
                .HasForeignKey(p => p.PlatoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Ingrediente>()
                .HasMany<PlatoIngredientes>(Pi => Pi.Platos)
                .WithOne(p => p.Ingrediente)
                .HasForeignKey(p => p.IngredienteId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Plato>()
                .HasMany<PlatoIngredientes>(p => p.Ingredientes)
                .WithOne(p => p.Plato)
                .HasForeignKey(p => p.PlatoId)
                .OnDelete(DeleteBehavior.Cascade);


            #endregion

        }
    }
}
