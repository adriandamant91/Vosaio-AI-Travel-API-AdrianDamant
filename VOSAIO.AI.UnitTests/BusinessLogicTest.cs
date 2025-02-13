using VOSAIO.AI.GETS.Infastructure.Services;
using VOSAIO.AI.GETS.Models;

namespace VOSAIO.AI.UnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CalculateAccomidationNightlyBudget()
        {
            var userInput = new UserInput()
            {
                Destination ="Texas", 
                TravelDates = new() { DateTime.Now,DateTime.UtcNow.AddDays(10) },
                Budget = 5000,
                Interests = new() { "Hiking","Swimming","Shooting","Reading"}
            };
            ChatGPTIntegrationService chatGPTIntegrationService = new ChatGPTIntegrationService();
            var nightlyBudget = chatGPTIntegrationService.CalculateAccomidationNightlyBudget(userInput);
            Assert.That(nightlyBudget, Is.EqualTo(200));
        }

        [Test]
        public void CalculateAccomidationNights()
        {
            var userInput = new UserInput()
            {
                Destination = "Texas",
                TravelDates = new() { DateTime.Now, DateTime.UtcNow.AddDays(10) },
                Budget = 5000,
                Interests = new() { "Hiking", "Swimming", "Shooting", "Reading" }
            };
            ChatGPTIntegrationService chatGPTIntegrationService = new ChatGPTIntegrationService();
            var accomidationNights = chatGPTIntegrationService.CalculateAccomidationNights(userInput);
            Assert.That(accomidationNights, Is.EqualTo(10));
        }


        [Test]
        public async Task TestHotelsAIReturn()
        {
            var userInput = new UserInput()
            {
                Destination = "Texas",
                TravelDates = new() { DateTime.Now, DateTime.UtcNow.AddDays(10) },
                Budget = 5000,
                Interests = new() { "Hiking", "Swimming", "Shooting", "Reading" }
            };
            ChatGPTIntegrationService chatGPTIntegrationService = new ChatGPTIntegrationService();
            var hotels = await chatGPTIntegrationService.GetGeneratedHotels(userInput);
            Assert.That((hotels ?? new()).Count, Is.EqualTo(3));
        }


        [Test]
        public async Task TestActivitiesAIReturn()
        {
            var userInput = new UserInput()
            {
                Destination = "Texas",
                TravelDates = new() { DateTime.Now, DateTime.UtcNow.AddDays(10) },
                Budget = 5000,
                Interests = new() { "Hiking", "Swimming", "Shooting", "Reading" }
            };
            ChatGPTIntegrationService chatGPTIntegrationService = new ChatGPTIntegrationService();
            var activities = await chatGPTIntegrationService.GetGeneratedActivities(userInput);
            Assert.That((activities ?? new()).Count, Is.EqualTo(5));
        }

        [Test]
        public async Task TestRestuarantsAIReturn()
        {
            var userInput = new UserInput()
            {
                Destination = "Texas",
                TravelDates = new() { DateTime.Now, DateTime.UtcNow.AddDays(10) },
                Budget = 5000,
                Interests = new() { "Hiking", "Swimming", "Shooting", "Reading" }
            };

            ChatGPTIntegrationService chatGPTIntegrationService = new ChatGPTIntegrationService();
            var accomidationNights = chatGPTIntegrationService.CalculateAccomidationNights(userInput);
            var restuarants = await chatGPTIntegrationService.GetGeneratedRestaurants(userInput);
            Assert.That((restuarants ?? new()).Count, Is.EqualTo(accomidationNights));
        }
    }
}