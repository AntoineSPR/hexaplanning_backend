using Microsoft.EntityFrameworkCore;
using Procrastinator.Context;
using Procrastinator.Services;

namespace TestsUnitaires
{
    public class QuestServiceTests
    {
        private readonly DataContext _context;
        private readonly QuestService _questService;

        public QuestServiceTests(
            )
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new DataContext(options);

            _questService = new QuestService(_context);
        }

        public async Task GetQuestByIdAsync_WithValidId_ReturnsQuest()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var quest = new Procrastinator.Models.Quest
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Title = "Test Quest",
                Description = "This is a test quest",
                IsAssigned = false,
                IsDone = false
            };
            await _context.Quests.AddAsync(quest);
            await _context.SaveChangesAsync();
            // Act
            var result = await _questService.GetQuestByIdAsync(quest.Id, userId);

            // Assert
            Assert.NotNull(result);
            Assert.Single(_context.Quests);
            Assert.Equal(quest.Id, result.Id);
            Assert.Equal(userId, result.UserId);
        }
    }
}
