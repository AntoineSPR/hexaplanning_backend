using System.Runtime.CompilerServices;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Procrastinator.Context;
using Procrastinator.Models;

namespace Procrastinator.Services
{
    public class FixturesService
    {
        private readonly DataContext _context;
        public FixturesService(DataContext context)
        {
            _context = context;
        }

        public void CreateUsers(int number)
        {
            var faker = new Faker("fr");
            for (int i = 0; i < number; i++)
            {
                // To make sure each generated email/username is unique
                var email = faker.Internet.Email();
                var splitEmail = email.Split("@");
                var emailName = splitEmail[0] + Guid.NewGuid().ToString().Substring(8, 8);
                email = emailName + "@" + splitEmail[1];

                var user = new UserApp 
                {
                    UserName = email,
                    Email = email,
                    NormalizedEmail = email.ToUpper(),
                    NormalizedUserName = email.ToUpper(),
                    Name = faker.Name.FullName(),
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAIAAYagAAAAEH23EZOdWWjTCZaC+rs1rjQ5IRxzpjCZ9yXP/A4m1GsUDenq3XRkY90WRswaWW32wQ=="
                };
                _context.Users.Add(user);
                _context.SaveChanges();
            }
        }

        public async Task CreateQuests(int number)
        {
            var faker = new Faker("fr");

            List<UserApp> users = await _context.Users.Where((u) => u.EmailConfirmed == true).ToListAsync();
            List<Quest> quests = new List<Quest>();

            foreach (var user in users)
            {
                for (int i = 0; i < number; i++)
                {
                    var quest = new Quest
                    {
                        Title = faker.Lorem.Sentence(3, 5),
                        Description = faker.Lorem.Paragraph(),
                        //Priority = (QuestPriority)faker.Random.Int(0, 2),
                        UserId = user.Id
                    };
                    //_context.Quests.Add(quest);
                    quests.Add(quest);
                }
            }
            _context.AddRange(quests);
            await _context.SaveChangesAsync();
        }
    }
}
