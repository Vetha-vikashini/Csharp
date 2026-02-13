namespace MovieTicketBooking.Models
{
    public class Show
    {
        public int ShowId { get; set; }
        public int MovieId { get; set; }
        public TimeSpan ShowTime { get; set; }
        public decimal Price { get; set; }
    }
}
