namespace VOSAIO.AI.GETS.Models
{
    public class UserInput
    {
        public required string Destination { get; set; }
        public List<DateTime>? TravelDates { get; set; }
        public double Budget { get; set; }
        public List<string>? Interests { get; set; }
    }
}
