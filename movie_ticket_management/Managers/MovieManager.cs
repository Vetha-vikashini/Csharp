using MovieTicketBooking.Models;
using System.Collections.Generic;

namespace MovieTicketBooking.Managers;

class MovieManager
{
    public List<Movie> Movies { get; private set; }

    public MovieManager()
    {
        Movies = new List<Movie>();
    }

    public void AddMovie(string title, int id)
    {
        
        if (Movies.Exists(m => m.MovieId == id))
        {
            Console.WriteLine("Movie with same ID already exists");
            return;
        }

        Movies.Add(new Movie(title, id));
    }

    public Movie GetMovieById(int id)
    {
        return Movies.Find(m => m.MovieId == id);
    }

    public Movie GetMovieByTitle(string title)
    {
        return Movies.Find(m => m.Title == title);
    }
}
