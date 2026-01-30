namespace MovieTicketBooking.Models;

class Seat
{
    public int SeatNumber { get; private set; }
    public bool IsBooked { get; private set; }
    public decimal Price { get; private set; }

    public Seat(int number, decimal price)
    {
        SeatNumber = number;
        Price = price;
        IsBooked = false;
    }

    public void Book()
    {
        IsBooked = true;
    }
}
