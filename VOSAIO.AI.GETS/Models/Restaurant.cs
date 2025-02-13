namespace VOSAIO.AI.GETS.Models
{
    public class Restaurant
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public double StarRating { get; set; }
        public DateTime? ScheduledDate { get; set; }
        public string? DressCode { get; set; }
        public string? ContactNumber { get; set; }
        public string? EmailAddress { get; set; }
        public string? WebsiteURL { get; set; }
        public string? TotalCost { get; set; }
    }
}
