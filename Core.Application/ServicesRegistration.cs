using Core.Application.Interfaces.Services;
using Core.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application
{
    public static class ServicesRegistration
    {
        public static void AddAplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(IGenericService<,,>),typeof(GenericService<,,>));
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IMesaService, MesaService>();
            services.AddTransient<IOrdenService, OrdenService>();
            services.AddTransient<IPlatoService, PlatoService>();
            services.AddTransient<IIngredienteService, IngredienteService>();


            services.AddAutoMapper(Assembly.GetExecutingAssembly());

        }
    }
}
