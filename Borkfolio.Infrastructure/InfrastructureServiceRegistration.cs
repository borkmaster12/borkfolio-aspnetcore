using Borkfolio.Application.Contracts.Infrastructure;
using Borkfolio.Infrastructure.Services.BoardGameGeek;
using Borkfolio.Infrastructure.Services.ProfanityChecker;
using Microsoft.Extensions.DependencyInjection;

namespace Borkfolio.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IBoardGameGeekService, BoardGameGeekService>();
            services.AddScoped<IProfanityCheckerService, ProfanityCheckerService>();

            services.AddHttpClient(
                "BoardGameGeek",
                httpClient =>
                {
                    httpClient.BaseAddress = new Uri("https://boardgamegeek.com/xmlapi2/");
                }
            );

            return services;
        }
    }
}
