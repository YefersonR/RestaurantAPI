using Core.Application.Interfaces.Repositories;
using Infrastructura.Persistence.Repositories;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public static class RepositoriesRegistration
    {
        public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("DatabaseInMemory"))
            {
                services.AddDbContext<RestaurantContext>(option => option.UseInMemoryDatabase("InMemoryDB"));

            }
            else
            {
                services.AddDbContext<RestaurantContext>(option =>
                    option.UseSqlServer(configuration.GetConnectionString("RestaurantString"),
                    m => m.MigrationsAssembly(typeof(RestaurantContext).Assembly.FullName)));
            }

            services.AddTransient(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            services.AddTransient<IIngredienteRepository,IngredienteRepository>();
            services.AddTransient<IMesaRepository, MesaRepository>();
            services.AddTransient<IMesaOrdenesRepository, MesaOrdenesRepository>();
            services.AddTransient<IOrdenRepository, OrdenRepository>();
            services.AddTransient<IPlatoRepository, PlatoRepository>();
            services.AddTransient<IPlatoIngredienteRepository, PlatoIngredienteRepository>();



        }
    }
}
