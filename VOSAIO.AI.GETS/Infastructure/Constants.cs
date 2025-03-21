﻿using VOSAIO.AI.GETS.Data;

namespace VOSAIO.AI.GETS.Infastructure
{
    public class Constants
    {
        public static string OpenAiApiKey = "[Add API Key Here]";

        public static Dictionary<DataRequestType, string> DataRequestPrompts = new()
        {
            {DataRequestType.Hotels,
                "Create a list of 3 hotels in CITY that cost no more than COSTPERNIGHTUSD per night for 2 guests, including the cost per night and their rating as well as a brief 15 word description of the hotel." },
            {DataRequestType.Restuarants,
                "Create a list of TRIPDAYS restuarants in CITY, including the average cost of a meal and their rating." },
            {DataRequestType.Activities,
                "Create a list of 5 activities to do in CITY, including the average cost per activity and their rating." }
        };

        public static int MaxTokenValue = 2000;
        public static string AIModel = "gpt-3.5-turbo-instruct";
    }
}
