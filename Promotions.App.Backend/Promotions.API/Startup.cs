using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Promotions.Core.Interfaces;
using Promotions.Core.Services;
using Promotions.Infrastucture.Context;

using Promotions.Infrastucture.Interfaces;
using Promotions.Infrastucture.Repositories;

namespace Promotions.API
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
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddControllers().AddNewtonsoftJson(option =>
            {
                option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            services.AddSwaggerGen(c =>
            {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API de promociones Fravega", Version = "v1"});
            });

            //MONGODB
            //services.Configure<MongoDBSettings>(
            //    Configuration.GetSection(nameof(MongoDBSettings)));

            //services.AddSingleton<IMongoDBSettings>(sp => sp.GetRequiredService<IOptions<MongoDBSettings>>().Value);

            services.AddTransient<IMongoContext, MongoContext>();

            services.AddTransient<IPromotionService, PromotionService>();

            services.AddScoped<IPromotionRepository, PromotionRepository>();

         
            services.AddMvc().AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API de promociones Fravega");
                c.RoutePrefix = string.Empty;
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
