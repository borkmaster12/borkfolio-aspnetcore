namespace Borkfolio.Application.Contracts.Infrastructure
{
    public interface IProfanityCheckerService
    {
        bool ContainsProfanity(string phrase);
    }
}
