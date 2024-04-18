using Borkfolio.Application.Contracts.Persistence;
using Borkfolio.Application.Features.BoardGames.Queries.GetMyCollection;
using Moq;

namespace Borkfolio.Application.UnitTests.BoardGames.Queries
{
    public class GetMyCollectionShould
    {
        private Mock<IMyCollectionRepository> _mockCollectionRepository;

        [SetUp]
        public void Setup()
        {
            var collection = new List<CollectionItemDto>();

            _mockCollectionRepository = new Mock<IMyCollectionRepository>();
            _mockCollectionRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(collection);
        }

        [Test]
        public async Task GetMyCollectionFromRepository()
        {
            var request = new GetMyCollectionQuery();

            var sut = new GetMyCollectionQueryHandler(_mockCollectionRepository.Object);

            var result = await sut.Handle(request, CancellationToken.None);

            _mockCollectionRepository.Verify(r => r.GetAllAsync(), Times.Once);
        }
    }
}
