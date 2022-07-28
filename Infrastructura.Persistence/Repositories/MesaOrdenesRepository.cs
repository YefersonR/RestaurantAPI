using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructura.Persistence.Repositories
{
    public class MesaOrdenesRepository : GenericRepository<MesaOrdenes>, IMesaOrdenesRepository
    {
        private readonly RestaurantContext _restaurantContext;
        public MesaOrdenesRepository(RestaurantContext restaurantContext) : base(restaurantContext)
        {
            _restaurantContext = restaurantContext;
        }
    }
}
