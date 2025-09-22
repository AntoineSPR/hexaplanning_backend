using Microsoft.EntityFrameworkCore;
using Procrastinator.Context;
using Procrastinator.Models;
using Procrastinator.Services;

namespace TestsUnitaires
{
    public class QuestServiceTests
    {
        private readonly DataContext _context;
        private readonly QuestService _questService;

        public QuestServiceTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new DataContext(options);
            _questService = new QuestService(_context);
        }

        [Fact]
        public async Task GetQuestByIdAsync_WithValidId_ReturnsQuest()
        {
            var userId = Guid.NewGuid();
            var quest = new Quest
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

            var result = await _questService.GetQuestByIdAsync(quest.Id, userId);

            Assert.NotNull(result);
            Assert.Single(_context.Quests);
            Assert.Equal(quest.Id, result.Id);
            Assert.Equal(userId, result.UserId);
        }

        [Fact]
        public async Task GetQuestByIdAsync_WithInvalidId_ReturnsNull()
        {
            var userId = Guid.NewGuid();
            var result = await _questService.GetQuestByIdAsync(Guid.NewGuid(), userId);
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateQuestAsync_CreatesQuest()
        {
            var userId = Guid.NewGuid();
            var questDto = new QuestDTO
            {
                Title = "New Quest",
                Description = "Description",
                UserId = userId,
                IsAssigned = false,
                IsDone = false,
                Priority = QuestPriority.PRIMARY,
            };

            var result = await _questService.CreateQuestAsync(questDto, userId);

            Assert.NotNull(result);
            Assert.Equal("New Quest", result.Title);
            Assert.Equal(userId, result.UserId);
            Assert.Single(_context.Quests);
        }

        [Fact]
        public async Task UpdateQuestAsync_UpdatesQuest()
        {
            var userId = Guid.NewGuid();
            var quest = new Quest
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Title = "Old Title",
                Description = "Old Description",
                IsAssigned = false,
                IsDone = false
            };
            await _context.Quests.AddAsync(quest);
            await _context.SaveChangesAsync();

            var updatedDto = new QuestDTO
            {
                Id = quest.Id,
                Title = "Updated Title",
                Description = "Updated Description",
                UserId = userId,
                IsAssigned = true,
                IsDone = true,
                Priority = QuestPriority.PRIMARY,
            };

            var result = await _questService.UpdateQuestAsync(quest.Id, updatedDto, userId);

            Assert.NotNull(result);
            Assert.Equal("Updated Title", result.Title);
            Assert.True(result.IsAssigned);
            Assert.True(result.IsDone);
        }

        [Fact]
        public async Task UpdateQuestAsync_WithInvalidId_ReturnsNull()
        {
            var userId = Guid.NewGuid();
            var updatedDto = new QuestDTO
            {
                Id = Guid.NewGuid(),
                Title = "Updated Title",
                Description = "Updated Description",
                UserId = userId,
                IsAssigned = true,
                IsDone = true,
                Priority = QuestPriority.PRIMARY,
            };

            var result = await _questService.UpdateQuestAsync(updatedDto.Id.Value, updatedDto, userId);
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteQuestAsync_DeletesQuest()
        {
            var userId = Guid.NewGuid();
            var quest = new Quest
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Title = "Quest to delete",
                Description = "Description",
                IsAssigned = false,
                IsDone = false
            };
            await _context.Quests.AddAsync(quest);
            await _context.SaveChangesAsync();

            var result = await _questService.DeleteQuestAsync(quest.Id, userId);

            Assert.True(result);
            Assert.Empty(_context.Quests);
        }

        [Fact]
        public async Task DeleteQuestAsync_WithInvalidId_ReturnsFalse()
        {
            var userId = Guid.NewGuid();
            var result = await _questService.DeleteQuestAsync(Guid.NewGuid(), userId);
            Assert.False(result);
        }

        [Fact]
        public async Task GetAllQuestsAsync_ReturnsAllUserQuests()
        {
            var userId = Guid.NewGuid();
            var quest1 = new Quest { Id = Guid.NewGuid(), UserId = userId, Title = "Q1", Description = "", IsAssigned = false, IsDone = false };
            var quest2 = new Quest { Id = Guid.NewGuid(), UserId = userId, Title = "Q2", Description = "", IsAssigned = true, IsDone = true };
            await _context.Quests.AddRangeAsync(quest1, quest2);
            await _context.SaveChangesAsync();

            var result = await _questService.GetAllQuestsAsync(userId);

            Assert.Equal(2, result.Count);
            Assert.Contains(result, q => q.Title == "Q1");
            Assert.Contains(result, q => q.Title == "Q2");
        }

        [Fact]
        public async Task GetAllPendingQuestsAsync_ReturnsPendingQuests()
        {
            var userId = Guid.NewGuid();
            var quest1 = new Quest { Id = Guid.NewGuid(), UserId = userId, Title = "Pending", Description = "", IsAssigned = false, IsDone = false };
            var quest2 = new Quest { Id = Guid.NewGuid(), UserId = userId, Title = "Done", Description = "", IsAssigned = false, IsDone = true };
            await _context.Quests.AddRangeAsync(quest1, quest2);
            await _context.SaveChangesAsync();

            var result = await _questService.GetAllPendingQuestsAsync(userId);

            Assert.Single(result);
            Assert.Equal("Pending", result[0].Title);
        }

        [Fact]
        public async Task GetAllCompletedQuestsAsync_ReturnsCompletedQuests()
        {
            var userId = Guid.NewGuid();
            var quest1 = new Quest { Id = Guid.NewGuid(), UserId = userId, Title = "Pending", Description = "", IsAssigned = false, IsDone = false };
            var quest2 = new Quest { Id = Guid.NewGuid(), UserId = userId, Title = "Done", Description = "", IsAssigned = false, IsDone = true };
            await _context.Quests.AddRangeAsync(quest1, quest2);
            await _context.SaveChangesAsync();

            var result = await _questService.GetAllCompletedQuestsAsync(userId);

            Assert.Single(result);
            Assert.Equal("Done", result[0].Title);
        }

        [Fact]
        public async Task GetAllUnassignedPendingQuestsAsync_ReturnsUnassignedPendingQuests()
        {
            var userId = Guid.NewGuid();
            var quest1 = new Quest { Id = Guid.NewGuid(), UserId = userId, Title = "UnassignedPending", Description = "", IsAssigned = false, IsDone = false };
            var quest2 = new Quest { Id = Guid.NewGuid(), UserId = userId, Title = "AssignedPending", Description = "", IsAssigned = true, IsDone = false };
            var quest3 = new Quest { Id = Guid.NewGuid(), UserId = userId, Title = "UnassignedDone", Description = "", IsAssigned = false, IsDone = true };
            await _context.Quests.AddRangeAsync(quest1, quest2, quest3);
            await _context.SaveChangesAsync();

            var result = await _questService.GetAllUnassignedPendingQuestsAsync(userId);

            Assert.Single(result);
            Assert.Equal("UnassignedPending", result[0].Title);
        }
    }
}
