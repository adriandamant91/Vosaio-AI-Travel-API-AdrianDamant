using VOSAIO.AI.GETS.Models;

namespace VOSAIO.AI.GETS.Infastructure.Services
{
    public interface IChatGPTIntegrationService
    {
        Task<List<Itinerary>> GetGeneratedItinerary(UserInput userInput);
    }
}
