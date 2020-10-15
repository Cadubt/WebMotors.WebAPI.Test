using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using WebMotorsTest.Context;
using WebMotorsTest.Repositories;
using WebMotorsTest.Repositories.Interfaces;
using WebMotorsTest.Services;
using WebMotorsTest.Services.Interfaces;

namespace WebMotorsTest
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
            services.AddDbContext<WebMotorsDataContext>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("WebMotors"));
            });

            services.AddControllers();

            //Services
            services.AddScoped<IAdService, AdService>();
            //Repositories
            services.AddScoped<IAdRepository, AdRepository>();

            services.AddSwaggerGen(c =>
            {
                //Swagger Documentation properties
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "WebMotors API",
                    Version = "v1",
                    Description = "Web API | Teste Admissional (Anúncios Online) Cadu Torres",
                    TermsOfService = new Uri("https://www.linkedin.com/in/cadubt/"),
                    Contact = new OpenApiContact
                    {
                        Name = "Cadu Torres",
                        Email = "cadubt@gmail.com",
                        Url = new Uri("http://www.cadubt.com.br/home"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "LinkedIn Cadu",
                        Url = new Uri("https://www.linkedin.com/in/cadubt/"),
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "REST API Authentication V1");
            });
        }
    }
}
