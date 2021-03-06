﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using CuttingEdge.Patterns.Abstractions;
using CuttingEdge.DemoWeb.Entity;
using CuttingEdge.Patterns.Repository;

namespace CuttingEdge.DemoWeb.Server
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            // Enable cross-origin services.
            services.AddCors();

            // Add options services.
            services.AddOptions();
            
            // Add Entity Framework services.
            services.AddEntityFramework();
            services.AddEntityFrameworkSqlServer();

            // Add application entity context.
            services.AddDbContext<EntityContext<Domain>>(options =>
                options.UseSqlServer(Configuration["DataSource:ConnectionString"])
                );

            // Add unit of work factory.
            services.AddScoped<IUnitOfWorkFactory<Domain>, UnitOfWorkFactory<Domain>>();

            // Add repository factory.
            services.AddScoped<IRepositoryFactory<Domain>, EntityRepositoryFactory<Domain>>();

            // Add unit of work service.
            services.AddScoped(sp => sp.GetService<IUnitOfWorkFactory<Domain>>().Create());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials());
            app.UseMvc();
        }
    }
}
