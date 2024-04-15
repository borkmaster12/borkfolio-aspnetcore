using Borkfolio.Application.Features.BoardGames.Commands.CreateSuggestion;
using Borkfolio.Application.Features.BoardGames.Queries.GetBoardGameDetails;
using Borkfolio.Application.Features.BoardGames.Queries.GetMyCollection;
using Borkfolio.Application.Features.BoardGames.Queries.GetSuggestions;
using Borkfolio.Application.Features.BoardGames.Queries.GetSuggestionsSummary;
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

        [HttpPost(Name = "AddSuggestion")]
        public async Task<ActionResult<CreateSuggestionCommandResponse>> AddSuggestion(
            [FromBody] CreateSuggestionCommand createSuggestionCommand
        )
        {
            var response = await _mediator.Send(createSuggestionCommand);

            return Ok(response);
        }

        [HttpGet(Name = "GetSuggestions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<GetSuggestionsDto>>> GetSuggestions()
        {
            List<GetSuggestionsDto>? result = await _mediator.Send(new GetSuggestionsQuery());

            return Ok(result);
        }

        [HttpGet(Name = "GetSuggestionsSummary")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<GetSuggestionsSummaryDto>>> GetSuggestionsSummary()
        {
            List<GetSuggestionsSummaryDto>? result = await _mediator.Send(new GetSuggestionsSummaryQuery());

            return Ok(result);
        }
    }
}
