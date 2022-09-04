using Contactos.Business.Services;
using Contactos.Data;
using Contactos.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;

namespace Contactos.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("DevCorsPolicy", builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            services.AddControllers();

            //Inyección de dependencias
            string dbConnString = Configuration.GetConnectionString("ContactsContext");
            services.AddScoped(d => new DatabaseContext(dbConnString));
            services.AddScoped<ContactRepository>(r => new ContactRepository(r.GetRequiredService<DatabaseContext>()));
            services.AddScoped<ContactosService>(s => new ContactosService(s.GetRequiredService<ContactRepository>()));

            //Agregando Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Contactos API (Test para Freshbooks)",
                    Version = "v1",
                    Description = "Esta API provee las funcionalidades necesarias para operar un sistema de gestión de contactos como ejercicio para aplicar a la vacante de empleo en FreshBooks",
                    Contact = new OpenApiContact
                    {
                        Name = "Emmanuel Reyes",
                        Email = "ekingsb@hotmail.com",
                        Url = new Uri("https://www.google.com"),
                    },
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                
            }

            //Habilita swagger.
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "swagger";
                c.SwaggerEndpoint("v1/swagger.json", "Contactos API V1");

                //c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseCors("DevCorsPolicy");

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapSwagger();
            });
        }
    }
}
