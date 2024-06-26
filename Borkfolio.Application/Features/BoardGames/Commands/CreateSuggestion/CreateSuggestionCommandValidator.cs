﻿using Borkfolio.Application.Contracts.Infrastructure;
using FluentValidation;

namespace Borkfolio.Application.Features.BoardGames.Commands.CreateSuggestion
{
    public class CreateSuggestionCommandValidator : AbstractValidator<CreateSuggestionDto>
    {
        public CreateSuggestionCommandValidator(IProfanityCheckerService profanityCheckerService)
        {
            RuleFor(suggestion => suggestion.MinimumAge)
                .NotEmpty()
                .WithMessage("Unable to confirm the age rating on that game")
                .LessThan(17)
                .WithMessage("No games with an age rating of 17+");

            RuleFor(suggestion => suggestion.Name)
                .Must(name => !profanityCheckerService.ContainsProfanity(name))
                .WithMessage("Keep the suggestions clean please");
        }
    }
}
