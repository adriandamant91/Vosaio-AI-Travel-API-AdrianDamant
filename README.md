Demo URL links

[Service URL] /api/itinerary/generate?Destination=Durban&TravelDates=2025-02-12T20%3A45%3A24.832Z&TravelDates=2025-02-22T20%3A45%3A25.665Z&Budget=5000&Interests=golf&Interests=hiking&Interests=swimming&Interests=gym

curl -X 'GET' \
  '[Service URL] /api/itinerary/generate?Destination=Durban&TravelDates=2025-02-12T20%3A45%3A24.832Z&TravelDates=2025-02-22T20%3A45%3A25.665Z&Budget=5000&Interests=golf&Interests=hiking&Interests=swimming&Interests=gym' \
  -H 'accept: */*'

[Service URL]: the URL which points to your Docker container where you are running the API, will most likely be localhost.
API also has Swagger accessible from the following URL:
[Service URL] /swagger/index.html

AI Integration Explanation

I integrated in ChatGPT as I had  not used this model before and was curios to give a test and learn more about it and see how it handled structured data returns.
It does require well structure prompt to get the model to return the structured data I require.

A challenge I had to overcome:
ChatGPT seemingly completely at random ignores my commands to structure the data and return gibberish, causing my Serialization to fail. 
To work around this, I have a try/ catch and a while look running each data collection request for each structure data component up to 3 times, this seems to have eliminated the error, if it persists we can always increase the attempt count as high as we need it to.

Example from my Code where this is being executed:
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

The AI model does require a ChatGPT apiKey which is obtained by registering an account with ChatGPT, and paying for credits to be used by the platform.

This key, along with the max token count per request are set within the Constants class. A future noteworthy change would be to move these into the appsettings.json file for better security, and to obfuscate the key within the project when it is deployed. 

Currently my key is included in this project for testing, and I have bought sufficient credits for you to test it with. However I will disable this key when you are done marking the assignment.

