using System.Collections.Generic;

namespace MovieTicketBooking.Models;

class Show
{
    public TimeSpan ShowTime { get; private set; }
    public List<Seat> Seats { get; private set; }

    public Show(TimeSpan time, int totalSeats, decimal price)
    {
        ShowTime = time;
        Seats = new List<Seat>();

        for (int i = 1; i <= totalSeats; i++)
        {
            Seats.Add(new Seat(i, price));
        }
    }

    public List<int> GetAvailableSeats()
    {
        List<int> available = new();

        foreach (var seat in Seats)
        {
            if (!seat.IsBooked)
                available.Add(seat.SeatNumber);
        }
        return available;
    }
}
