using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class OrdenRepository : GenericRepository<Orden>,IOrdenRepository
    {
        private readonly RestaurantContext _restaurantContext;
        public OrdenRepository(RestaurantContext restaurantContext) : base(restaurantContext)
        {
            _restaurantContext = restaurantContext;
        }
        public async Task UpdateAsync(Orden entity)
        {
            _restaurantContext.Entry(entity).State = EntityState.Modified;
            await _restaurantContext.SaveChangesAsync();
        }
        public async Task<List<Orden>> GetAllWhitIncludes()
        {
            var query = _restaurantContext.Set<Orden>().Include(orden=>orden.Mesa)
                                                        .Include(x=>x.Platos)
                                                        .ThenInclude(plato=>plato.Ingredientes);
            return await query.ToListAsync();
        }

    }
}
