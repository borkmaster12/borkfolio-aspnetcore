using Borkfolio.Application.Contracts.Infrastructure;
using Borkfolio.Infrastructure.BoardGameGeek;
using Borkfolio.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<BorkfolioDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BorkfolioConnectionString"));
});

builder.Services.AddScoped<IBoardGameGeekService, BoardGameGeekService>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient("BoardGameGeek", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://boardgamegeek.com/xmlapi2/");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
