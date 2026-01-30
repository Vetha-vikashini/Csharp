using System.Collections.Generic;

namespace MovieTicketBooking.Models;

class Show
{
    public TimeSpan ShowTime { get; private set; }
    public List<Seat> Seats { get; private set; }  // encapsulation

    public Show(TimeSpan time, int totalSeats, decimal price)
    {
        ShowTime = time;
        //Show and seat have has-a relationship , so Show class contains a list of Seat objects
        Seats = new List<Seat>();   // Storing the reference of seat objects  


        for (int i = 1; i <= totalSeats; i++)
        {
            Seats.Add(new Seat(i, price));  // Creating and adding new seat object
        }
    }

    public List<int> GetAvailableSeats()
    {
        List<int> available = new();

        foreach (var seat in Seats)
        {
            if (!seat.IsBooked)   // Accessing IsBooked property
                available.Add(seat.SeatNumber);
        }

        return available;
    }
}

