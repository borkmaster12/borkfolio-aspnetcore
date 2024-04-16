using Borkfolio.Application.Contracts.Persistence;
using Borkfolio.Domain.Entities;
using Moq;

namespace Borkfolio.Application.UnitTests.Mocks
{
    public class RepositoryMockFactory
    {
        public static Mock<ISuggestionsRepository> GetSuggestionsRepository()
        {
            var suggestions = new List<Suggestion>();
            var mockSuggestionsRepository = new Mock<ISuggestionsRepository>();

            mockSuggestionsRepository.Setup(r => r.ListAllAsync()).ReturnsAsync(suggestions);

            mockSuggestionsRepository.Setup(repo => repo.AddAsync(It.IsAny<Suggestion>())).ReturnsAsync(
                (Suggestion suggestion) =>
                {
                    suggestions.Add(suggestion);
                    return suggestion;
                });

            return mockSuggestionsRepository;
        }

        public static Mock<T1> GetMockRepository<T1, T2>()
            where T1 : class, IAsyncRepository<T2>
            where T2 : class
        {
            var collection = new List<T2>();
            var mockRepository = new Mock<T1>();

            mockRepository.Setup(r => r.ListAllAsync()).ReturnsAsync(collection);

            mockRepository.Setup(repo => repo.AddAsync(It.IsAny<T2>())).ReturnsAsync(
                (T2 newItem) =>
                {
                    collection.Add(newItem);
                    return newItem;
                });

            return mockRepository;
        }
    }
}
