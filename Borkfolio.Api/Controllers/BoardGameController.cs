using Borkfolio.Application.Contracts.Infrastructure;
using Borkfolio.Application.Models.BoardGameGeek;
using Microsoft.AspNetCore.Mvc;

namespace Borkfolio.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BoardGameController : ControllerBase
    {
        private readonly IBoardGameGeekService _boardGameGeekService;

        public BoardGameController(IBoardGameGeekService boardGameGeekService)
        {
            _boardGameGeekService = boardGameGeekService;
        }

        [HttpGet(Name = "GetMyCollection")]
        public async Task<List<BggCollectionItem>> GetMyCollection()
        {
            return await _boardGameGeekService.GetMyCollection();
        }

        [HttpGet("{name}", Name = "Search")]
        public async Task<List<BggSearchResultItem>> Search(string name)
        {
            return await _boardGameGeekService.SearchBoardGames(name);
        }
    }
}
