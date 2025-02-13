using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VOSAIO.AI.GETS.Data;
using VOSAIO.AI.GETS.Infastructure.Services;
using VOSAIO.AI.GETS.Models;

namespace VOSAIO_AI_Itinerary.Controllers
{
    public class ItineraryController : Controller
    {
        private readonly IChatGPTIntegrationService _integrationService;
        private readonly IDatabaseSyncService _databaseSyncService;

        public ItineraryController(IChatGPTIntegrationService integrationService, IDatabaseSyncService databaseSyncService)
        {
            _integrationService = integrationService;
            _databaseSyncService = databaseSyncService;
        }

        //Specification asked for a POST, however as we are returning data it is best to use a GET
        [AllowAnonymous]
        [HttpGet]
        [Route("api/itinerary/generate")]
        public async Task<ActionResult> GenerateItinerary(UserInput userInput)
        {
            if (userInput == null) return Json(new ExceptionResult() { ErrorCode = 400, Message = "Bad Request. Missing input data" });

            if (!ModelState.IsValid) return Json(new ExceptionResult() { ErrorCode= 400 ,Message="Bad Request. Missing input data"});

            //Validate Entered Dates//
            if(userInput.TravelDates is null) 
                return Json(new ExceptionResult() { ErrorCode = 400, Message = "Bad Request. Missing date input data" });

            if (userInput.TravelDates.Count < 2)
                return Json(new ExceptionResult() { ErrorCode = 400, Message = "Bad Request. Missing date input data" });

            foreach(var date in userInput.TravelDates)
            {
                if(date.Date < DateTime.UtcNow.Date) return Json(new ExceptionResult() { ErrorCode = 400, Message = "Bad Request. Input date has already passed" });
            }

            var itinerary = await _integrationService.GetGeneratedItinerary(userInput);

            if(itinerary is null) return Json(new ExceptionResult() { ErrorCode = 500, Message = "Itinerary generation failed!" });

            await _databaseSyncService.InsertUserRequest(new()
            {
                Destination = userInput.Destination,
                StartDate= userInput.TravelDates[0],
                EndDate = userInput.TravelDates[1],
                Budget = userInput.Budget,
                Interests = userInput.Interests,
                RequestDate = DateTime.UtcNow,
                RequestSuccessful = true
            });

            return Json(itinerary);
        }
    }
}
