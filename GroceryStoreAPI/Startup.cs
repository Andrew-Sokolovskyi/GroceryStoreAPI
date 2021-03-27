using GroceryStoreAPI.Data;
using GroceryStoreAPI.Data.Context;
using GroceryStoreAPI.Domain.Models;
using GroceryStoreAPI.Logging;
using GroceryStoreAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NLog;
using System;
using System.IO;

namespace GroceryStoreAPI
{
    public class Startup
    {
        const string ApiTitle = "Grocery Store API";
        const string ApiName = "groceryStore";

        private readonly IHostEnvironment _hostingEnvironment;

        public Startup(IConfiguration configuration, IHostEnvironment hostingEnvironment)
        {
            LogManager.LoadConfiguration(System.String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var dataBaseFilePath = _hostingEnvironment.ContentRootPath + @"\" + Configuration.GetConnectionString("Database");
            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);
            services.AddScoped<IRepository<Customer>, CustomerRepository>();
            services.AddScoped<ICustomersServices, CustomersServices>();
            services.AddSingleton<IDatabase>(new Database(dataBaseFilePath));
            services.AddSingleton<ILog, Nlog>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(ApiName, new OpenApiInfo { Title = ApiTitle, Version = "v1" });

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [Obsolete]
        public void Configure(IApplicationBuilder app, IHostEnvironment env, IDatabase database)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{ApiName}/swagger.json", ApiTitle);
            });

            app.UseHttpsRedirection();
            app.UseMvc();

            database.ReadData().Wait();
        }
    }
}
