using Borkfolio.Api.Middleware;
using Borkfolio.Application;
using Borkfolio.Infrastructure;
using Borkfolio.Persistence;

namespace Borkfolio.Api
{
    public static class StartupExtensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();

            builder.Services.AddCors(
                options => options.AddPolicy(
                    "open",
                    policy => policy.WithOrigins([builder.Configuration["ApiUrl"] ?? "https://localhost:7146",
                        builder.Configuration["WebAppUrl"] ?? "https://localhost:7169"])
            .AllowAnyMethod()
            .SetIsOriginAllowed(pol => true)
            .AllowAnyHeader()
            .AllowCredentials()));

            builder.Services.AddInfrastructureServices();
            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddApplicationServices();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            return builder.Build();
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            app.UseCors("open");

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCustomExceptionHandler();

            app.MapControllers();

            return app;
        }
    }
}
