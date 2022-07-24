using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurante.WebApi.Extensions
{
    public static class AppExtensions
    {
        public static void UseSwaggerExtentions(this IApplicationBuilder application)
        {
            application.UseSwagger();
            application.UseSwaggerUI(options=>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Restaurant Api");
            });
        }
    }
}
