using Microsoft.EntityFrameworkCore;
using Procrastinator.Context;
using Procrastinator.Models;
using Procrastinator.Services;
using Procrastinator.Utilities;

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
                //IsAssigned = false,
                //IsDone = false
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
            var questDto = new QuestCreateDTO
            {
                Title = "New Quest",
                Description = "Description",
                EstimatedTime = 10,
                PriorityId = Guid.NewGuid(),
                StatusId = Guid.NewGuid()
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
                EstimatedTime = 5,
                PriorityId = Guid.NewGuid(),
                StatusId = Guid.NewGuid()
            };
            await _context.Quests.AddAsync(quest);
            await _context.SaveChangesAsync();

            var updatedDto = new QuestUpdateDTO
            {
                Id = quest.Id,
                Title = "Updated Title",
                Description = "Updated Description",
                EstimatedTime = 15,
                PriorityId = quest.PriorityId,
                StatusId = quest.StatusId
            };

            var result = await _questService.UpdateQuestAsync(quest.Id, updatedDto, userId);

            Assert.NotNull(result);
            Assert.Equal("Updated Title", result.Title);
        }

        [Fact]
        public async Task UpdateQuestAsync_WithInvalidId_ReturnsNull()
        {
            var userId = Guid.NewGuid();
            var updatedDto = new QuestUpdateDTO
            {
                Id = Guid.NewGuid(),
                Title = "Updated Title",
                Description = "Updated Description",
                EstimatedTime = 15,
                PriorityId = Guid.NewGuid(),
                StatusId = Guid.NewGuid()
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
                //IsAssigned = false,
                //IsDone = false
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
            var quest1 = new Quest { Id = Guid.NewGuid(), UserId = userId, Title = "Q1", Description = "", StatusId = HardCode.STATUS_COMPLETED_ID };
            var quest2 = new Quest { Id = Guid.NewGuid(), UserId = userId, Title = "Q2", Description = "", StatusId = HardCode.STATUS_COMPLETED_ID };
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
            var quest1 = new Quest { Id = Guid.NewGuid(), UserId = userId, Title = "Pending", Description = "", StatusId = HardCode.STATUS_WAITING_ID };
            //var quest2 = new Quest { Id = Guid.NewGuid(), UserId = userId, Title = "In_Progress", Description = "", StatusId = HardCode.STATUS_IN_PROGRESS_ID };
            var quest3 = new Quest { Id = Guid.NewGuid(), UserId = userId, Title = "Completed", Description = "", StatusId = HardCode.STATUS_COMPLETED_ID };

            await _context.Quests.AddRangeAsync(quest1, quest3);
            await _context.SaveChangesAsync();

            var result = await _questService.GetAllPendingQuestsAsync(userId);

            Assert.Single(result);
            Assert.Equal("Pending", result[0].Title);
        }

        [Fact]
        public async Task GetAllCompletedQuestsAsync_ReturnsCompletedQuests()
        {
            var userId = Guid.NewGuid();
            var quest1 = new Quest { Id = Guid.NewGuid(), UserId = userId, Title = "Pending", Description = "", StatusId = HardCode.STATUS_WAITING_ID };
            var quest2 = new Quest { Id = Guid.NewGuid(), UserId = userId, Title = "Done", Description = "", StatusId = HardCode.STATUS_COMPLETED_ID  };
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
            var quest1 = new Quest { Id = Guid.NewGuid(), UserId = userId, Title = "UnassignedPending", Description = "", StatusId = HardCode.STATUS_WAITING_ID  };
            var quest2 = new Quest { Id = Guid.NewGuid(), UserId = userId, Title = "AssignedPending", Description = "", StatusId = HardCode.STATUS_WAITING_ID };
            var quest3 = new Quest { Id = Guid.NewGuid(), UserId = userId, Title = "UnassignedDone", Description = "", StatusId = HardCode.STATUS_COMPLETED_ID };
            await _context.Quests.AddRangeAsync(quest1, quest2, quest3);
            await _context.SaveChangesAsync();
            
            var hexAssignment = new HexAssignment
            {
                Id = Guid.NewGuid(),
                Q = 0,
                R = 0,
                S = 0,
                QuestId = quest2.Id,
            };
            await _context.HexAssignments.AddRangeAsync(hexAssignment);
            await _context.SaveChangesAsync();

            var result = await _questService.GetAllUnassignedPendingQuestsAsync(userId);

            Assert.Single(result);
            Assert.Equal("UnassignedPending", result[0].Title);
        }
    }
}
