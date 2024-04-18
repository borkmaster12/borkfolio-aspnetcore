using AutoMapper;
using Borkfolio.Application.Contracts.Infrastructure;
using Borkfolio.Application.Exceptions;
using Borkfolio.Application.Features.BoardGames.Queries.GetBoardGameDetails;
using Borkfolio.Application.Models.BoardGameGeek;
using Borkfolio.Application.Profiles;
using Moq;

namespace Borkfolio.Application.UnitTests.BoardGames.Queries
{
    public class GetBoardGameDetailsShould
    {
        private readonly IMapper _mapper;

        private Mock<IBoardGameGeekService> _mockBoardGameGeekService;

        public GetBoardGameDetailsShould()
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
            _mockBoardGameGeekService = new Mock<IBoardGameGeekService>();
        }

        [Test]
        public async Task GetBoardGameDetailsFromBoardGameGeekService()
        {
            int bggGameId = 1;
            string bggGameName = "Example Name";
            int bggGameYear = 2024;
            int bggGameMinAge = 12;

            var sut = SetupQueryHandler(bggGameId, bggGameName, bggGameYear, bggGameMinAge);

            var request = new GetBoardGameDetailsQuery { Id = bggGameId };

            var result = await sut.Handle(request, CancellationToken.None);

            _mockBoardGameGeekService.Verify(s => s.GetBoardGameDetails(bggGameId), Times.Once());
        }

        [Test]
        public async Task ReturnGameDetailsDataInResponse()
        {
            int bggGameId = 1;
            string bggGameName = "Example Name";
            int bggGameYear = 2024;
            int bggGameMinAge = 12;

            var sut = SetupQueryHandler(bggGameId, bggGameName, bggGameYear, bggGameMinAge);

            var request = new GetBoardGameDetailsQuery { Id = bggGameId };

            var result = await sut.Handle(request, CancellationToken.None);

            Assert.Multiple(() =>
            {
                Assert.That(result.BoardGameGeekId, Is.EqualTo(bggGameId));
                Assert.That(result.Name, Is.EqualTo(bggGameName));
                Assert.That(result.Year, Is.EqualTo(bggGameYear));
                Assert.That(result.MinimumAge, Is.EqualTo(bggGameMinAge));
            });
        }

        [Test]
        public void ThrowIfGameIdNotFound()
        {
            int bggGameId = 1;
            string bggGameName = "Example Name";
            int bggGameYear = 2024;
            int bggGameMinAge = 12;

            var sut = SetupQueryHandler(bggGameId, bggGameName, bggGameYear, bggGameMinAge);

            _mockBoardGameGeekService
                .Setup(s => s.GetBoardGameDetails(bggGameId))
                .Throws(new NotFoundException("Game id", bggGameId));

            var request = new GetBoardGameDetailsQuery { Id = bggGameId };

            Assert.ThrowsAsync<NotFoundException>(
                () => sut.Handle(request, CancellationToken.None)
            );
        }

        private GetBoardGameDetailsQueryHandler SetupQueryHandler(
            int bggGameId,
            string bggGameName,
            int bggGameYear,
            int bggGameMinAge
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

            return new GetBoardGameDetailsQueryHandler(_mockBoardGameGeekService.Object, _mapper);
        }
    }
}
