namespace MovieTicketBooking.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public string MovieTitle { get; set; } = "";
        public TimeSpan ShowTime { get; set; }
        public int SeatNumber { get; set; }
        public decimal Price { get; set; }
    }
}
