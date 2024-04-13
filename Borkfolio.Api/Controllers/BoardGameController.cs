using Borkfolio.Application.Features.BoardGames.Queries.GetBoardGameDetails;
using Borkfolio.Application.Features.BoardGames.Queries.GetMyCollection;
using Borkfolio.Application.Features.BoardGames.Queries.SearchBoardGames;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Borkfolio.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BoardGameController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BoardGameController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "GetMyCollection")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<CollectionItemDto>>> GetMyCollection()
        {
            List<CollectionItemDto>? result = await _mediator.Send(new GetMyCollectionQuery());

            return Ok(result);
        }

        [HttpGet("{name}", Name = "Search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<BoardGameSearchResultDto>>> Search(string name)
        {
            List<BoardGameSearchResultDto>? result = await _mediator.Send(
                new SearchBoardGamesQuery { Name = name }
            );

            return Ok(result);
        }

        [HttpGet("{id}", Name = "Details")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<BoardGameDetailsDto>> Details(int id)
        {
            BoardGameDetailsDto? result = await _mediator.Send(
                new GetBoardGameDetailsQuery { Id = id }
            );

            return Ok(result);
        }
    }
}
