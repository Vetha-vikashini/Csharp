using Microsoft.VisualBasic;
using MovieTicketBooking.Models;
using MovieTicketBooking.Users;
using System.Collections.Generic;

namespace MovieTicketBooking.Services;

class BookingService
{
    public Booking BookTickets(
        Customer customer,
        Movie movie,
        Show show,
        List<int> seatNumbers)
    {
        List<int> bookedSeats = new();
        decimal total = 0;

        // Validate all seats first
        foreach (int no in seatNumbers)
        {
            Seat seat = show.Seats.Find(s => s.SeatNumber == no);

            if (seat == null || seat.IsBooked)
            {
                throw new Exception($"Seat {no} is not available");
            }
        }

        // Book seats only if all are available
        foreach (int no in seatNumbers)
        {
            Seat seat = show.Seats.Find(s => s.SeatNumber == no);
            seat.Book();
            bookedSeats.Add(no);
            total += seat.Price;
        }

        // Create booking object
        Booking booking = new Booking(
            movie.Title,
            show.ShowTime,
            bookedSeats,
            total
        );

        return booking;
    }
}
