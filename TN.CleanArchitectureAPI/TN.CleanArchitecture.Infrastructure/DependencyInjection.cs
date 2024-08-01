using TN.Prototype.CleanArchitecture.Application.Common.Interfaces;
using TN.Prototype.CleanArchitecture.Infrastructure.Persistence;
using TN.Prototype.CleanArchitecture.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentMigrator.Runner;
using TN.Prototype.CleanArchitecture.Infrastructure.Persistence.Constants;

namespace TN.Prototype.CleanArchitecture.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddFluentMigratorCore().ConfigureRunner(rb => rb
                                            .AddSqlServer()
                                            .WithGlobalConnectionString(configuration.GetConnectionString(ConnectionString.Default))
                                            .ScanIn(typeof(ApplicationDbContext).Assembly).For.Migrations())
                                            .BuildServiceProvider(false);

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString(ConnectionString.Default)));


            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
            services.AddScoped<IDomainEventService, DomainEventService>();
            services.AddTransient<IDateTimeService, DateTimeService>();
            services.AddTransient<IEmailService, EmailService>();
            return services;
        }
    }
}
