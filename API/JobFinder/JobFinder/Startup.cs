using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using InitJobOffers.Services;
using Authorization.Services;
using AutoMapper;
using Database.Models.Authorization;
using Microsoft.AspNetCore.Identity;
using Authorization;
using Database.Models;
using JobSeeker.Services;

namespace JobFinder
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
            services.AddDbContext<DataContext>(x =>
                x.UseSqlServer("Server=.;Database=JobFinder;Trusted_Connection=True;MultipleActiveResultSets=true"));

            services.AddControllers();
            services.AddScoped<IInitJobs, InitJobs>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJWTService, JWTService>();
            services.AddScoped<IJobSeekerService, JobSeekerService>();
            services.AddTransient<IInitJobs, InitJobs>();

            services.AddAuth(Configuration["Tokens:Issuer"], Configuration["Tokens:Audience"], Configuration["Tokens:JWTSecret"], "Server=.;Database=JobFinder;Trusted_Connection=True;MultipleActiveResultSets=true");

            services.AddAutoMapper(typeof(Startup));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

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
