using Borkfolio.Application.Contracts.Infrastructure;

namespace Borkfolio.Infrastructure.Services.ProfanityChecker
{
    public class ProfanityCheckerService : IProfanityCheckerService
    {
        public bool ContainsProfanity(string phrase)
        {
            var profanityChecker = new ProfanityFilter.ProfanityFilter();

            return profanityChecker.IsProfanity(phrase);
        }
    }
}
