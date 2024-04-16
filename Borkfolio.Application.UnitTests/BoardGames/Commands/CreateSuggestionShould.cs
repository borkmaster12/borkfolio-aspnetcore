using AutoMapper;
using Borkfolio.Application.Contracts.Infrastructure;
using Borkfolio.Application.Contracts.Persistence;
using Borkfolio.Application.Features.BoardGames.Commands.CreateSuggestion;
using Borkfolio.Application.Models.BoardGameGeek;
using Borkfolio.Application.Profiles;
using Borkfolio.Application.UnitTests.Mocks;
using Borkfolio.Domain.Entities;
using Moq;

namespace Borkfolio.Application.UnitTests.BoardGames.Commands
{
    public class CreateSuggestionShould
    {
        private readonly IMapper _mapper;

        private Mock<ISuggestionsRepository> _mockSuggestionsRepository;
        private Mock<IBoardGameRepository> _mockBoardGameRepository;
        private Mock<IBoardGameGeekService> _mockBoardGameGeekService;
        private Mock<IProfanityCheckerService> _mockProfanityCheckerService;

        public CreateSuggestionShould()
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
            _mockSuggestionsRepository = RepositoryMockFactory.GetMockRepository<
                ISuggestionsRepository,
                Suggestion
            >();
            _mockBoardGameRepository = RepositoryMockFactory.GetMockRepository<
                IBoardGameRepository,
                BoardGame
            >();
            _mockBoardGameGeekService = new Mock<IBoardGameGeekService>();
            _mockProfanityCheckerService = new Mock<IProfanityCheckerService>();
        }

        [Test]
        [Sequential]
        public async Task ReturnSuggestionInResponse(
            [Values(230802, 225694, 233848)] int bggGameId,
            [Values("Azul", "Decrypto", "Hako Onna")] string bggGameName,
            [Values(2017, 2018, 2016)] int bggGameYear,
            [Values(8, 12, 10)] int bggGameMinAge
        )
        {
            var sut = SetupCommandHandler(
                bggGameId,
                bggGameName,
                bggGameYear,
                bggGameMinAge,
                containsProfanity: false
            );

            var request = new CreateSuggestionCommand { BoardGameGeekId = bggGameId };

            var result = await sut.Handle(request, CancellationToken.None);

            Assert.That(result.Suggestion.BoardGameGeekId, Is.EqualTo(bggGameId));
            Assert.That(result.Suggestion.Name, Is.EqualTo(bggGameName));
            Assert.That(result.Suggestion.Year, Is.EqualTo(bggGameYear));
            Assert.That(result.Suggestion.MinimumAge, Is.EqualTo(bggGameMinAge));
        }

        [Test]
        public async Task GetBoardGameDetailsFromBoardGameGeekService()
        {
            int bggGameId = 1;
            string bggGameName = "Example Name";
            int bggGameYear = 2024;
            int bggGameMinAge = 12;

            var sut = SetupCommandHandler(
                bggGameId,
                bggGameName,
                bggGameYear,
                bggGameMinAge,
                containsProfanity: false
            );

            var request = new CreateSuggestionCommand { BoardGameGeekId = bggGameId };

            var result = await sut.Handle(request, CancellationToken.None);

            _mockBoardGameGeekService.Verify(s => s.GetBoardGameDetails(bggGameId), Times.Once());
        }

        [Test]
        public async Task CheckBoardGameNameForProfanity()
        {
            int bggGameId = 1;
            string bggGameName = "Example Name";
            int bggGameYear = 2024;
            int bggGameMinAge = 12;

            var sut = SetupCommandHandler(
                bggGameId,
                bggGameName,
                bggGameYear,
                bggGameMinAge,
                containsProfanity: false
            );

            var request = new CreateSuggestionCommand { BoardGameGeekId = bggGameId };

            var result = await sut.Handle(request, CancellationToken.None);

            _mockProfanityCheckerService.Verify(
                s => s.ContainsProfanity(bggGameName),
                Times.Once()
            );
        }

        [Test]
        [Sequential]
        public async Task PersistSuggestedBoardGame(
            [Values(230802, 225694, 233848)] int bggGameId,
            [Values("Azul", "Decrypto", "Hako Onna")] string bggGameName,
            [Values(2017, 2018, 2016)] int bggGameYear,
            [Values(8, 12, 10)] int bggGameMinAge
        )
        {
            var sut = SetupCommandHandler(
                bggGameId,
                bggGameName,
                bggGameYear,
                bggGameMinAge,
                containsProfanity: false
            );

            var request = new CreateSuggestionCommand { BoardGameGeekId = bggGameId };

            var result = await sut.Handle(request, CancellationToken.None);

            _mockSuggestionsRepository.Verify(
                r =>
                    r.AddAsync(
                        It.Is<Suggestion>(s =>
                            s.BoardGame.BoardGameGeekId == bggGameId
                            && s.BoardGame.Name.Equals(bggGameName)
                            && s.BoardGame.Year == bggGameYear
                        )
                    ),
                Times.Once
            );
        }

        [Test]
        public async Task FailWhenNameContainsProfanity()
        {
            int bggGameId = 1;
            string bggGameName = "Example Name";
            int bggGameYear = 2024;
            int bggGameMinAge = 12;

            var sut = SetupCommandHandler(
                bggGameId,
                bggGameName,
                bggGameYear,
                bggGameMinAge,
                containsProfanity: true
            );

            var request = new CreateSuggestionCommand { BoardGameGeekId = bggGameId };

            var result = await sut.Handle(request, CancellationToken.None);

            Assert.Multiple(() =>
            {
                Assert.That(result.Success, Is.False);
                Assert.That(result.ValidationErrors, Is.Not.Empty);
            });
        }

        [Test]
        public async Task FailWhenMinimumAgeTooHigh()
        {
            int bggGameId = 1;
            string bggGameName = "Example Name";
            int bggGameYear = 2024;
            int bggGameMinAge = 17;

            var sut = SetupCommandHandler(
                bggGameId,
                bggGameName,
                bggGameYear,
                bggGameMinAge,
                containsProfanity: false
            );

            var request = new CreateSuggestionCommand { BoardGameGeekId = bggGameId };

            var result = await sut.Handle(request, CancellationToken.None);

            Assert.Multiple(() =>
            {
                Assert.That(result.Success, Is.False);
                Assert.That(result.ValidationErrors, Is.Not.Empty);
            });
        }

        private CreateSuggestionCommandHandler SetupCommandHandler(
            int bggGameId,
            string bggGameName,
            int bggGameYear,
            int bggGameMinAge,
            bool containsProfanity
        )
        {
            _mockBoardGameGeekService
                .Setup(s => s.GetBoardGameDetails(bggGameId))
                .ReturnsAsync(
                    new BggGameDetailsItem
                    {
                        Id = bggGameId,
                        _name = new BggGameDetailsItem.NameElement { Value = bggGameName },
                        _year = new BggGameDetailsItem.YearElement
                        {
                            Value = Convert.ToString(bggGameYear)
                        },
                        _minimumAge = new BggGameDetailsItem.MinimumAgeElement
                        {
                            Value = Convert.ToString(bggGameMinAge)
                        }
                    }
                );

            _mockProfanityCheckerService
                .Setup(pc => pc.ContainsProfanity(bggGameName))
                .Returns(containsProfanity);

            return new CreateSuggestionCommandHandler(
                _mockSuggestionsRepository.Object,
                _mockBoardGameGeekService.Object,
                _mapper,
                _mockBoardGameRepository.Object,
                _mockProfanityCheckerService.Object
            );
        }
    }
}
