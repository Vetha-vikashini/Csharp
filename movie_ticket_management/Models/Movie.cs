using System.Collections.Generic;

namespace MovieTicketBooking.Models;

class Movie
{
    public string Title { get; private set; }
    public int MovieId { get; private set; }
    public List<Show> Shows { get; private set; }

    public Movie(string title, int id)
    {
        Title = title;
        MovieId = id;
        Shows = new List<Show>();
    }

    public void AddShow(TimeSpan time, int seats, decimal price)
    {
        
        if (Shows.Exists(s => s.ShowTime == time))
        {
            Console.WriteLine("Show already exists");
            return;
        }

        Shows.Add(new Show(time, seats, price));
    }
}
