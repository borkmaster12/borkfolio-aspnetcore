using AutoMapper;
using Borkfolio.Application.Contracts.Persistence;
using Borkfolio.Application.Features.BoardGames.Queries.GetSuggestionsSummary;
using Borkfolio.Application.Profiles;
using Borkfolio.Application.UnitTests.Mocks;
using Borkfolio.Domain.Entities;
using Moq;

namespace Borkfolio.Application.UnitTests.BoardGames.Queries
{
    public class GetSuggestionsSummaryShould
    {
        private readonly IMapper _mapper;

        private Mock<ISuggestionsRepository> _mockSuggestionsRepository;

        public GetSuggestionsSummaryShould()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [SetUp]
        public void Setup()
        {
            _mockSuggestionsRepository = MockRepositoryFactory.GetMockRepository<
                ISuggestionsRepository,
                Suggestion
            >();
        }

        [Test]
        public async Task GetSuggestionsSummaryFromRepository()
        {
            var request = new GetSuggestionsSummaryQuery();

            var sut = new GetSuggestionsSummaryQueryHandler(
                _mapper,
                _mockSuggestionsRepository.Object
            );

            var result = await sut.Handle(request, CancellationToken.None);

            _mockSuggestionsRepository.Verify(r => r.GetSuggestionsSummary(), Times.Once);
        }
    }
}
