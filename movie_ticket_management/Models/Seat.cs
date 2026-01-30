namespace MovieTicketBooking.Models;

class Seat
{
    public int SeatNumber { get; private set; }
    public bool IsBooked { get; private set; }  // encapsulation
    public decimal Price { get; private set; }

    public Seat(int number, decimal price)
    {
        SeatNumber = number;
        Price = price;
        IsBooked = false;
    }

    public void Book()    // method to book the seat
    {
        IsBooked = true; 
    }
}
