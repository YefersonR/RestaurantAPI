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
        public DbSet<Ingrediente> Ingredientes{ get; set; }
        public DbSet<Plato> Platos { get; set; }
        public DbSet<Mesa> Mesas { get; set; }
        public DbSet<Orden> Ordenes { get; set; }

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
            modelBuilder.Entity<Plato>()
                .HasKey(plato => plato.Id);
            modelBuilder.Entity<Ingrediente>()
                .HasKey(ingrediente => ingrediente.Id);
            modelBuilder.Entity<Mesa>()
                .HasKey(mesa => mesa.Id);
            modelBuilder.Entity<Orden>()
                .HasKey(orden => orden.Id);
            #endregion
            #region Relationship
            modelBuilder.Entity<Mesa>()
                .HasMany<Orden>(orden=>orden.Ordenes)
                .WithOne(mesa=>mesa.Mesa)
                .HasForeignKey(orden=>orden.MesaId);
            #endregion
        }
    }
}
