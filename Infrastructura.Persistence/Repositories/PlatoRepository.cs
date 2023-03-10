using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class PlatoRepository : GenericRepository<Plato>,IPlatoRepository
    {
        private readonly RestaurantContext _restaurantContext;
        public PlatoRepository(RestaurantContext restaurantContext) : base(restaurantContext)
        {
            _restaurantContext = restaurantContext;
        }
        public override Task<Plato> AddAsync(Plato entity)
        {
            return base.AddAsync(entity);
        }
        public override Task UpdateAsync(Plato entity, int ID)
        {
            entity.Id = ID;
            return base.UpdateAsync(entity, ID);
        }

    }
}
