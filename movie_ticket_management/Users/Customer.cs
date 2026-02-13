using MovieTicketBooking.Models;
using System.Collections.Generic;

namespace MovieTicketBooking.Users;

class Customer : User
{
    public List<Booking> Bookings { get; private set; }

    public Customer(int id, string name)
        : base(id, name, "Customer")
    {
        Bookings = new List<Booking>();
    }

    public override void ViewProfile()
    {
        Console.WriteLine($"Customer ID: {UserId}");
        Console.WriteLine($"Name: {Name}");
    }

    public override void ContactHelp()
    {
        Console.WriteLine("Customer Care: +91 9876543210");
    }
}
