using Borkfolio.Domain.Entities;
using Borkfolio.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Borkfolio.Persistence.IntegrationTests
{
    public class BaseRepositoryShould
    {
        private BorkfolioDbContext _context;

        [SetUp]
        public void Setup()
        {
            var dbContextOptions = new DbContextOptionsBuilder<BorkfolioDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new BorkfolioDbContext(dbContextOptions);
        }

        [TearDown]
        public async Task TearDown()
        {
            await _context.DisposeAsync();
        }

        [Test]
        public async Task List_ReturnAllRows()
        {
            var seedCount = await Utilities.SeedDatabase(_context);
            var sut = new BaseRepository<Suggestion>(_context);

            var result = await sut.ListAllAsync();

            Assert.That(result, Has.Count.EqualTo(seedCount));
        }

        [Test]
        public async Task Add_ReturnInputObject()
        {
            var sut = new BaseRepository<Suggestion>(_context);
            var boardGameId = Guid.NewGuid();
            var suggestion = new Suggestion
            {
                BoardGameId = boardGameId,
                BoardGame = new BoardGame
                {
                    BoardGameId = boardGameId,
                    BoardGameGeekId = 1,
                    Name = "Test",
                    Year = 2024
                },
                CreatedDate = DateTime.UtcNow,
                LastModifiedDate = DateTime.UtcNow,
            };

            var result = await sut.AddAsync(suggestion);

            Assert.Multiple(() =>
            {
                Assert.That(result.BoardGameId, Is.EqualTo(suggestion.BoardGameId));
                Assert.That(result.BoardGame, Is.EqualTo(suggestion.BoardGame));
                Assert.That(result.CreatedDate, Is.EqualTo(suggestion.CreatedDate));
                Assert.That(result.LastModifiedDate, Is.EqualTo(suggestion.LastModifiedDate));
                Assert.That(_context.Suggestions.Count, Is.EqualTo(1));
            });
        }

        [Test]
        public async Task Add_CreateSingleInstance()
        {
            var sut = new BaseRepository<Suggestion>(_context);
            var boardGameId = Guid.NewGuid();
            var suggestion = new Suggestion
            {
                BoardGameId = boardGameId,
                BoardGame = new BoardGame
                {
                    BoardGameId = boardGameId,
                    BoardGameGeekId = 1,
                    Name = "Test",
                    Year = 2024
                },
                CreatedDate = DateTime.UtcNow,
                LastModifiedDate = DateTime.UtcNow,
            };

            var result = await sut.AddAsync(suggestion);

            Assert.That(_context.Suggestions.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task GetById_ReturnCorrectRow()
        {
            var target = new Suggestion
            {
                SuggestionId = Guid.NewGuid(),
                BoardGameId = Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow,
                LastModifiedDate = DateTime.UtcNow,
            };
            _context.Suggestions.Add(target);
            await _context.SaveChangesAsync();

            var sut = new BaseRepository<Suggestion>(_context);

            var result = await sut.GetByIdAsync(target.SuggestionId);

            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.SuggestionId, Is.EqualTo(target.SuggestionId));
                Assert.That(result.BoardGameId, Is.EqualTo(target.BoardGameId));
                Assert.That(result.CreatedDate, Is.EqualTo(target.CreatedDate));
                Assert.That(result.LastModifiedDate, Is.EqualTo(target.LastModifiedDate));
            });
        }

        [Test]
        public async Task Update_PersistChanges()
        {
            var initialBoardGameId = Guid.NewGuid();
            var target = new Suggestion
            {
                SuggestionId = Guid.NewGuid(),
                BoardGameId = initialBoardGameId,
                CreatedDate = DateTime.UtcNow,
                LastModifiedDate = DateTime.UtcNow,
            };
            _context.Suggestions.Add(target);
            await _context.SaveChangesAsync();
            var sut = new BaseRepository<Suggestion>(_context);

            target.BoardGameId = Guid.NewGuid();
            await sut.UpdateAsync(target);
            var result = _context.Suggestions.Find(target.SuggestionId);

            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.SuggestionId, Is.EqualTo(target.SuggestionId));
                Assert.That(result.BoardGameId, Is.Not.EqualTo(initialBoardGameId));
                Assert.That(result.BoardGameId, Is.EqualTo(target.BoardGameId));
                Assert.That(result.CreatedDate, Is.EqualTo(target.CreatedDate));
                Assert.That(result.LastModifiedDate, Is.EqualTo(target.LastModifiedDate));
            });
        }

        [Test]
        public async Task Delete_RemoveSingleInstance()
        {
            var seedCount = await Utilities.SeedDatabase(_context);
            var target = new Suggestion
            {
                BoardGameId = Guid.NewGuid(),
                BoardGame = new BoardGame
                {
                    BoardGameId = Guid.NewGuid(),
                    BoardGameGeekId = 1,
                    Name = "Test",
                    Year = 2024
                },
                CreatedDate = DateTime.UtcNow,
                LastModifiedDate = DateTime.UtcNow,
            };
            _context.Add(target);
            await _context.SaveChangesAsync();

            var sut = new BaseRepository<Suggestion>(_context);

            await sut.DeleteAsync(target);

            Assert.That(_context.Suggestions.Count, Is.EqualTo(seedCount));
        }

        [Test]
        public async Task Delete_RemovesTargetInstance()
        {
            var target = new Suggestion
            {
                BoardGameId = Guid.NewGuid(),
                BoardGame = new BoardGame
                {
                    BoardGameId = Guid.NewGuid(),
                    BoardGameGeekId = 1,
                    Name = "Test",
                    Year = 2024
                },
                CreatedDate = DateTime.UtcNow,
                LastModifiedDate = DateTime.UtcNow,
            };
            _context.Add(target);
            await _context.SaveChangesAsync();

            var sut = new BaseRepository<Suggestion>(_context);

            await sut.DeleteAsync(target);
            var result = await sut.GetByIdAsync(target.SuggestionId);

            Assert.That(result, Is.Null);
        }
    }
}
