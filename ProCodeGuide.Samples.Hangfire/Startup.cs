using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ProCodeGuide.Samples.Hangfire.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using ProCodeGuide.Samples.Hangfire.Settings;
using ProCodeGuide.Samples.Hangfire.Model.Context;

namespace ProCodeGuide.Samples.Hangfire
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
            services.AddHangfire(x => x.UseSqlServerStorage("Server=MIRANPC\\SQLEXPRESS;Database=ProCodeGuide.Samples.Hangfire;Trusted_Connection=True;MultipleActiveResultSets=true"));
            services.AddHangfireServer();

            services.AddControllers();
            //services.AddTransient<IHealthCheckService, DummyHealthCheckService>();
            services.AddTransient<IHealthCheckService, HealthCheckService>();
            services.AddTransient<IMailService, Services.MailService>();
            services.AddDbContext<ProCodeGuideSamplesHangfireContext>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProCodeGuide.Samples.Hangfire", Version = "v1" });
            });
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
            });
           
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.Configure<TaskSettings>(Configuration.GetSection("TaskSettings"));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProCodeGuide.Samples.Hangfire v1"));
            }
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.UseHangfireDashboard();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
