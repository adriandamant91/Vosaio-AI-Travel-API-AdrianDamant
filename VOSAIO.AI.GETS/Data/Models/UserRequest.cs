
namespace VOSAIO.AI.GETS.Data.Models
{
    public class UserRequest
    {
        public int UserRequestID { get; set; }
        public required string Destination { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Budget { get; set; }
        public List<string>? Interests { get; set; }
        public DateTime RequestDate { get; set; }
        public bool RequestSuccessful { get; set; }
    }
}
