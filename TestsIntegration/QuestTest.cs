using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Procrastinator.Models;

namespace TestsIntegration
{
    public class QuestTest: IClassFixture<Factory>
    {
        private readonly HttpClient httpClient;
        private readonly JsonSerializerOptions jsonOptions;

        public QuestTest(Factory factory)
        {
            this.httpClient = factory.CreateClient();
            this.jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            };
        }

        [Fact]
        public async Task ReadQuest()
        {
            var response = await httpClient.GetAsync("/quest");
            var responseContent = await response.Content.ReadAsStringAsync();
            var responseArray = JsonSerializer.Deserialize<List<Quest>>(responseContent, jsonOptions);

            //Assert.NotNull(response);
            //Assert.NotEqual(0, responseArray?.Count);
        }
    }
}