using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Restaurante.WebApi.Extensions
{
    public static class ServiceExtentions
    {
        public static void AddSwaggerExtentions(this IServiceCollection services)
        {
            services.AddSwaggerGen(options=>
            {
                List<string> XMLFiles = Directory.GetFiles(AppContext.BaseDirectory,"*.xml",searchOption:SearchOption.TopDirectoryOnly).ToList();
                XMLFiles.ForEach(xmlfile => options.IncludeXmlComments(xmlfile));

                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Restaurant",
                    Description = "Api Restaurante",
                    Contact = new OpenApiContact
                    {
                        Name = "Yeferson Rubio",
                        Email = "yefersonrubio27@gmail.com" ,
                        Url = new Uri("https://www.Google.com")
                    }
                });
                options.DescribeAllParametersInCamelCase();
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "Input  your Bearer Token in this format - Bearer {your token here}"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference =new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id="Bearer"
                            },
                            Scheme="Bearer",
                            Name="Bearer",
                            In=ParameterLocation.Header,
                        }, new List<string>()
                    },
                });

            });
        }
        public static void AddApiVersioningExtensions(this IServiceCollection services)
        {
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1,0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;

            });
        }
    }
}
