using FluentValidation.AspNetCore;
using TN.Prototype.CleanArchitecture.Api.Filters;
using TN.Prototype.CleanArchitecture.Api.Services;
using TN.Prototype.CleanArchitecture.Application;
using TN.Prototype.CleanArchitecture.Application.Common.Interfaces;
using TN.Prototype.CleanArchitecture.Infrastructure;
using TN.Prototype.CleanArchitecture.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Linq;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using TN.Prototype.CleanArchitecture.Application.Common.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using TN.Prototype.CleanArchitecture.Api.Hub;

namespace TN.Prototype.CleanArchitecture
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
            services.AddApplication();
            services.AddInfrastructure(Configuration);
            services.AddSingleton<ICurrentUserService, CurrentUserService>();
            services.AddHttpContextAccessor();
            services.AddHealthChecks().AddDbContextCheck<ApplicationDbContext>();
            services.AddControllers(options => options.Filters.Add<ApiExceptionFilterAttribute>())
                    .AddNewtonsoftJson(options =>
                        {
                            options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                            options.UseMemberCasing();
                        });
            services.AddFluentValidationClientsideAdapters();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Clean Architecture Prototype", Version = "v1" });
            });
            services.AddSingleton<ICurrentUserService, CurrentUserService>();
            services.AddSignalR();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
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
            else
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseCors("AllowAll");
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("../swagger/v1/swagger.json", "Clean Architecture Prototype"));
            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = async (context, report) =>
                {
                    context.Response.ContentType = "application/json";
                    var response = new HealthCheckResponse
                    {
                        Status = report.Status.ToString(),
                        HealthChecks = report.Entries.Select(e => new IndividualHealthCheckResponse
                        {
                            Component = e.Key,
                            Status = e.Value.Status.ToString(),
                            Description = e.Value.Description
                        }),
                        HealthCheckDuration = report.TotalDuration
                    };
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
                }
            });
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<BroadcastHub>("/broadcastHub");
            });
        }
    }
}
