using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TN.CleanArchitectureBlazorApp.Application.Framework;
using TN.CleanArchitectureBlazorApp.Infrastructure.Framework;

namespace TN.CleanArchitectureBlazorApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<CustomHttpClient>(client =>
            {
                var baseAddress = configuration.GetValue<string>("BaseApiUrl");
                client.BaseAddress = new Uri(baseAddress);
            });
            services.AddScoped<ICustomHttpClient>(provider => provider.GetService<CustomHttpClient>());
            return services;
        }
    }
}
