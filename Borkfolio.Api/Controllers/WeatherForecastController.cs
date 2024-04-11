using Borkfolio.Application.Contracts.Infrastructure;
using Borkfolio.Application.Models.BoardGameGeek;
using Microsoft.AspNetCore.Mvc;

namespace Borkfolio.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IBoardGameGeekService _boardGameGeekService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IBoardGameGeekService boardGameGeekService)
        {
            _logger = logger;
            _boardGameGeekService = boardGameGeekService;
        }

        //[HttpGet(Name = "GetWeatherForecast")]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        //        TemperatureC = Random.Shared.Next(-20, 55),
        //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}

        [HttpGet(Name = "GetMyCollection")]
        public async Task<BggCollection> GetMyCollection()
        {
            return await _boardGameGeekService.GetMyCollection();
        }
    }
}
