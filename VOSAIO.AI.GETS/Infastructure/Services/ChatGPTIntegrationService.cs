using OpenAI;
using OpenAI.API.Completions;
using OpenAI.API;
using VOSAIO.AI.GETS.Data;
using VOSAIO.AI.GETS.Models;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using System.Collections.Generic;
using OpenAI.API.Models;


namespace VOSAIO.AI.GETS.Infastructure.Services
{


    public class ChatGPTIntegrationService: IChatGPTIntegrationService
    {
        public async Task<string> GetAIGeneratedRawData(DataRequestType dataRequestType, UserInput userInput)
        {
            APIAuthentication aPIAuthentication = new APIAuthentication(Constants.OpenAiApiKey);
            OpenAIAPI openAiApi = new OpenAIAPI(aPIAuthentication);

            try
            {
                var dataStructure = GetJsonFormat(dataRequestType);
                string prompt = $"{Constants.DataRequestPrompts[dataRequestType]} Return the data structured in JSON format ONLY as follows: {GetJsonFormat(dataRequestType)}. You must only return JSON. Keep these interests in mind when making your recommendations: {String.Join(",", userInput.Interests??new())}. ONLY Serialisable JSON RETURNED.";

                prompt = AddCityBudgetToAIPrompt(prompt, userInput);
                //add City name and Budget to prompt//

                var completionRequest = new CompletionRequest
                {
                    Prompt = prompt,
                    Model = Constants.AIModel,
                    MaxTokens = Constants.MaxTokenValue,
                   
                };
                var completionResult = await openAiApi.Completions.CreateCompletionAsync(completionRequest);
                var generatedText = completionResult.Completions[0].Text;

                return generatedText;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string AddCityBudgetToAIPrompt(string prompt, UserInput userInput) 
            => prompt.Replace("CITY", userInput.Destination)
            .Replace("COSTPERNIGHT", CalculateAccomidationNightlyBudget(userInput).ToString())
            .Replace("TRIPDAYS", CalculateAccomidationNights(userInput).ToString());

        public double CalculateAccomidationNightlyBudget(UserInput userInput)
        {
            double result = 0;
            if (userInput.TravelDates is null) return result;

            try
            {
                var tripDays = CalculateAccomidationNights(userInput);

                //40% of budget goes to hotel//
                result = (userInput.Budget * 0.4) / (tripDays == 0 ? 1 : tripDays);
            }
            catch { }
            return result;
        }

        public int CalculateAccomidationNights(UserInput userInput)
        {
            int result = 0;
            if (userInput.TravelDates is null) return result;

            try
            {
                return (userInput.TravelDates[1] - userInput.TravelDates[0]).Days;
            }
            catch { }
            return result;
        }

        public string GetJsonFormat(DataRequestType dataRequestType)
        {
            switch (dataRequestType)
            {
                case (DataRequestType.Activities): 
                    return JsonSerializer.Serialize(new List<Activity> { new() { Name="name"} }, _serializerOptions);
                case (DataRequestType.Restuarants): 
                    return JsonSerializer.Serialize(new List<Restaurant> { new() { Name = "name" } }, _serializerOptions);
                case (DataRequestType.Hotels): 
                    return JsonSerializer.Serialize(new List<Hotel>{ new() { Name="name"} }, _serializerOptions);
            }
            return "raw text";
        }

        public async Task<List<Itinerary>> GetGeneratedItinerary(UserInput userInput)
        {
            var itineraries = new List<Itinerary>();
            try
            {
                var suggestedHotels = await GetGeneratedHotels(userInput);
                if (suggestedHotels is null) return itineraries;

                var suggestedRestuarants = await GetGeneratedRestaurants(userInput);
                var suggestedActivities = await GetGeneratedActivities(userInput);

                if(suggestedRestuarants is not null && userInput.TravelDates is not null)
                {
                    var startDate = userInput.TravelDates[0];
                    foreach(var restuarant in suggestedRestuarants)
                    {
                        restuarant.ScheduledDate = startDate;
                        startDate = startDate.AddDays(1);
                    }
                }

                int itineraryID = 1;
                foreach (var hotel in suggestedHotels)
                {
                    itineraries.Add(new()
                    {
                        ItineraryID = itineraryID,
                        Hotel = hotel,
                        StartDate = userInput.TravelDates is null ? DateTime.UtcNow : userInput.TravelDates[0],
                        EndDate = userInput.TravelDates is null ? DateTime.UtcNow : userInput.TravelDates[1],
                        TotalPrice = userInput.Budget,
                        Activities = suggestedActivities,
                        Restaurants = suggestedRestuarants
                    });
                    itineraryID++;
                }
            }
            catch(Exception ex){ var message = ex.ToString(); }
            return itineraries;
        }

        public async Task<List<Hotel>?> GetGeneratedHotels(UserInput userInput)
        {
            try
            {
                //Had cases when AI model randomly returned bad data, attempting 3 times in case when bad data is returned to reduce this error margin
                List<Hotel>? suggestedHotels = null;
                int attempts = 0;
                while (suggestedHotels is null && attempts < 3)
                {
                    attempts++;
                    try
                    {
                        var hotelRawData = await GetAIGeneratedRawData(DataRequestType.Hotels, userInput);
                        suggestedHotels = JsonSerializer.Deserialize<List<Hotel>>(hotelRawData, _serializerOptions);
                    }
                    catch { }
                }
                return suggestedHotels;
            }
            catch{ return null; }
        }

        public async Task<List<Restaurant>?> GetGeneratedRestaurants(UserInput userInput)
        {
            try
            {
                //Had cases when AI model randomly returned bad data, attempting 3 times in case when bad data is returned to reduce this error margin
                List<Restaurant>? suggestedRestuarants = null;
                int attempts = 0;
                while (suggestedRestuarants is null && attempts < 3)
                {
                    attempts++;
                    try
                    {
                        var restuarantsRaw = await GetAIGeneratedRawData(DataRequestType.Restuarants, userInput);
                        suggestedRestuarants = JsonSerializer.Deserialize<List<Restaurant>>(restuarantsRaw.Replace("scheduledDate", "null"), _serializerOptions);
                    }catch{ }
                }
                return suggestedRestuarants;
            }
            catch { return null; }
        }

        public async Task<List<Activity>?> GetGeneratedActivities(UserInput userInput)
        {
            try
            {
                //Had cases when AI model randomly returned bad data, attempting 3 times in case when bad data is returned to reduce this error margin
                List<Activity>? suggestedActivities = null;
                int attempts = 0;
                while (suggestedActivities is null && attempts < 3)
                {
                    attempts++;
                    try
                    {
                        var activitiesRaw = await GetAIGeneratedRawData(DataRequestType.Activities, userInput);
                        suggestedActivities = JsonSerializer.Deserialize<List<Activity>>(activitiesRaw.Replace("scheduledDate", "null"), _serializerOptions);
                    }
                    catch { }
                }
                return suggestedActivities;
            }
            catch { return null; }
        }

        public static JsonSerializerOptions _serializerOptions => new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
        };
    }
}
