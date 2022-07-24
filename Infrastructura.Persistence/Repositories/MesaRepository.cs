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
    public class MesaRepository : GenericRepository<Mesa>, IMesaRepository
    {
        private readonly RestaurantContext _restaurantContext;
        public MesaRepository(RestaurantContext restaurantContext):base(restaurantContext)
        {
            _restaurantContext = restaurantContext;
        }
    }
}
