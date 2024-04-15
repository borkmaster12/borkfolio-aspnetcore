using Borkfolio.Application.Contracts.Persistence;
using Borkfolio.Domain.Entities;
using Borkfolio.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Borkfolio.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BorkfolioDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("BorkfolioConnectionString")));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IMyCollectionRepository, MyCollectionRepository>();
            services.AddScoped<IBoardGameRepository, BoardGameRepository>();
            services.AddScoped<IAsyncRepository<Suggestion>, SuggestionsRepository>();
            services.AddScoped<ISuggestionsRepository, SuggestionsRepository>();

            services.AddMemoryCache();

            return services;
        }
    }
}
