namespace VOSAIO.AI.GETS.Models
{
    public class Itinerary
    {
        public int ItineraryID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double TotalPrice { get; set; }
        public Hotel? Hotel { get; set; }
        public List<Activity>? Activities { get; set; }
        public List<Restaurant>? Restaurants { get; set; }
    }
}
