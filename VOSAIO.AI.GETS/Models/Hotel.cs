namespace VOSAIO.AI.GETS.Models
{
    public class Hotel
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public double StarRating { get; set; }
        public double PricePerNight { get; set; }
    }
}
