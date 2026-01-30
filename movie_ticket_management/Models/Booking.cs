using System.Collections.Generic;

namespace MovieTicketBooking.Models;

class Booking
{
    public string MovieTitle { get; set; }
    public TimeSpan ShowTime { get; set; }
    public List<int> SeatNumbers { get; set; }
    public decimal TotalAmount { get; set; }

    public Booking(string movie, TimeSpan time, List<int> seats, decimal total)
    {
        MovieTitle = movie;
        ShowTime = time;
        SeatNumbers = seats;
        TotalAmount = total;
    }
}
