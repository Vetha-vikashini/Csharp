// See https://aka.ms/new-console-template for more information

using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;
using System.Xml.Serialization;
class Seat
{
    public int seatNumber { get; private set; }   // encapsulation
    public bool isbooked { get; private set; }
    public decimal price { get; private set; }

    public Seat(int num, decimal ticketprice) // Parameterized Constructor
    {
        seatNumber = num;
        isbooked = false;
        price = ticketprice;
    }

    public void bookticket()
    {
        isbooked = true;
    }

    public decimal GetPrice(Movie movie, string showtime, int seatno)
    {
        Show show = movie.shows.Find(s => s.showTime == showtime); //Find the show object with that showtime
        Seat seat = show.seats.Find(s => s.seatNumber == seatno);

        decimal Price = seat.price;
        return Price;

    }
}

class Show
{
    public string showTime { get; private set; }
    public List<Seat> seats { get; private set; } //Using Composition since it belongs to "Has-A" relationship
                                                  // Since show has seats. Without show seat wont exist
    public Show(string Time, int total_seats, decimal price)
    {
        showTime = Time;
        seats = new List<Seat>();

        for (int i = 1; i <= total_seats; i++)
        {
            seats.Add(new Seat(i, price));
        }
    }

    public List<int> AvailableSeats(string c_time)
    {
        List<int> avail = new List<int>();   // Using List 
        Console.WriteLine($"Available seats in the show {c_time}");
        foreach (var seat in seats)
        {
            if (!seat.isbooked)
            {
                avail.Add(seat.seatNumber);
            }
        }

        return avail;
    }
}

class Movie
{
    public string title { get; private set; }
    public int m_id { get; private set; }
    public List<Show> shows;   //Using Composition since it belongs to "Has-A" relationship

    public Movie(string name, int id)
    {
        title = name;
        m_id = id;

        shows = new List<Show>();
    }

    public void addShow(string time, int total_seats, decimal price)
    {
        if (shows.Exists(s => s.showTime == time))
        {
            Console.WriteLine("Show with same time already exists");
            return;
        }
        Show newshow = new Show(time, total_seats, price);
        shows.Add(newshow);
    }

}

class MovieManager   //This class is Parent class or Owns other class like Movie,Shows,Seats
{
    public List<Movie> movies; // This list stores the references of Movie Object

    public MovieManager()
    {
        movies = new List<Movie>();
    }

    public void AddMovie(string name, int id)
    {   

        bool status= movies.Exists(m => m.m_id == id); // Checking if movie with same id already exists

        if (!status)
        {
            Movie newMovie = new Movie(name, id); // Creating object for movie class

            movies.Add(newMovie); // Add reference of that object to the list

            return;
        }

        else
        {
            Console.WriteLine("Movie with same ID already exists");
        }

    }
}
abstract class User    // using The abstarct class since here we have two types of user---Customer and Admin
{
    public int userid { get; private set; }
    public string user_name { get; private set; }

    public string userType;

    public User(int id, string name, string type = "User")
    {
        userid = id;
        user_name = name;
        userType = type;
    }

    public abstract void call_contact();

    public abstract void view_info();
}

class Booking
{
    public string movie_title { get; set; }
    public string showTime { get; set; }
    public List<int> seats { get; set; }

    public decimal total_price { get; set; }


    public Booking(string movie_title, string showTime, List<int> seats, decimal total_price)
    {
        this.movie_title = movie_title;
        this.showTime = showTime;
        this.seats = seats;
        this.total_price = total_price;
    }
}

class Customer : User
{
    public List<Booking> bookings { get; set; }
    public Customer(int id, string name, string type) : base(id, name, type)
    {
        bookings = new List<Booking>();
    }

    public override void call_contact()
    {
        Console.WriteLine("Helpline contact +912345667657");
    }

    public override void view_info()
    {
        Console.WriteLine($"UserID: {userid}");
        Console.WriteLine($"Name: {user_name}");
    }

    public void BookTickets(Movie movie, string time, int seatno, out string msg)
    {
        Show show = movie.shows.Find(s => s.showTime == time);  //Finding the show obj with that time

        if (show == null)
        {
            msg = "No Shows Found";
            return;

        }

        Seat seat = show.seats.Find(s => s.seatNumber == seatno);

        if (seat == null)
        {
            msg = "No seats found";
            return;
        }

        if (seat.isbooked)
        {
            msg = "Seat is already booked";
            return;
        }

        msg = "Seat booked successfully";
        seat.bookticket();

    }

}

class Admin : User
{
    string dept;
    public Admin(int id, string name, string type, string branch) : base(id, name, type) //Calling User constructor and also initialize its extra fields
    {
        dept = branch;
    }

    public override void call_contact()     // Overriding the User class methods
    {
        Console.WriteLine("Contact Developer for need");
    }

    public override void view_info()
    {
        Console.WriteLine($"UserID: {userid}");
        Console.WriteLine($"Name: {user_name}");

        Console.WriteLine($"Department: {dept}");

    }


}


class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Movie Ticket Booking");

        MovieManager movieManager = new MovieManager();   // Creating object 

        while (true)
        {
            Console.WriteLine("Enter your Login Details");
            Console.WriteLine("Enter you name");
            string name = Console.ReadLine();
            Console.WriteLine("Enter your userid");
            int id = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Enter your Type--- Customer or Admin");
            string type = Console.ReadLine();


            if (type == "Admin")
            {
                Console.WriteLine("Enter your Department");
                string dept = Console.ReadLine();
                Admin admin = new Admin(id, name, type, dept);
                Console.WriteLine("You are Aithorised to ");
                Console.WriteLine("Add movies--1");
                Console.WriteLine("Add shows--2");
                Console.WriteLine("View list of movies--3");
                Console.WriteLine("View Profile--4");
                Console.WriteLine("HelpLine--5");
                Console.WriteLine("Exit -- 0");

                while (true)
                {
                    Console.WriteLine("Enter your need--- Number");

                    int choice = Int32.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            {
                                Console.WriteLine("Enter Movie title");
                                string movie_title = Console.ReadLine();
                                Console.WriteLine("Enter Movie ID");
                                int m_id = Int32.Parse(Console.ReadLine());

                                movieManager.AddMovie(movie_title, m_id); // Moviemanger has movie list which has references to Movie class objects which is created inside AddMovie
                                break;
                            }

                        case 2:
                            {
                                Console.WriteLine("Enter movie id");
                                int movie_id = Int32.Parse(Console.ReadLine());
                                Console.WriteLine("Enter Show Time");
                                string showTime = Console.ReadLine();
                                Console.WriteLine("Enter Total number of seats");
                                int Totat_no_seats = Int32.Parse(Console.ReadLine());
                                Console.WriteLine("Enter Ticket Price");
                                decimal price = Decimal.Parse(Console.ReadLine());

                                Movie movie = movieManager.movies.Find(m => m.m_id == movie_id);  // From the movies list filtering the Movie obj which matches the id

                                if (movie != null)
                                {
                                    movie.addShow(showTime, Totat_no_seats, price);
                                }
                                else
                                {
                                    Console.WriteLine("No Movie Found");
                                }
                                break;
                            }

                        case 3:
                            {
                                foreach (var item in movieManager.movies)
                                {
                                    Console.WriteLine($"{item.m_id}  :  {item.title}");
                                }
                                break;
                            }

                        case 4:
                            {
                                admin.view_info();
                                break;
                            }

                        case 5:
                            {
                                admin.call_contact();
                                break;
                            }

                        case 0:
                            {
                                Console.WriteLine("Thanks for Coming");
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("Invalid Choice");
                                Console.WriteLine("Enter valid choice");
                                break;
                            }

                    }
                    if (choice == 0)
                    {
                        break;
                    }

                }
            }

            else if (type == "Customer")
            {
                Console.WriteLine("------Welcome to MyTicket------");
                Customer customer = new Customer(id, name, type);
                Console.WriteLine("****** Movie List ******");
                Console.WriteLine("\n");
                foreach (var item in movieManager.movies)
                {
                    Console.WriteLine($"{item.title}");
                }
                Console.WriteLine("\n");
                Console.WriteLine("************************");

                Console.WriteLine("Enter 1 to book Tickets");
                Console.WriteLine("Enter 2 to view Booked Tickets");
                Console.WriteLine("Enter 3 to View your profile");
                Console.WriteLine("Enter 4 to Helpline");
                Console.WriteLine("Enter 0 to Exit");

                while (true)
                {
                    Console.WriteLine("Enter your option--- Number");
                    int choice = Int32.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            {
                                Console.WriteLine("Enter movie name");
                                string movie_title = Console.ReadLine();

                                Movie movie = movieManager.movies.Find(m => m.title == movie_title);

                                Console.WriteLine("----Available shows----");
                                if (movie != null)
                                {
                                    foreach (var item in movie.shows)
                                    {
                                        Console.WriteLine(item.showTime);
                                    }
                                    Console.WriteLine("Enter you desirable time : ");
                                    string Customer_time = Console.ReadLine();

                                    Show show = movie.shows.Find(s => s.showTime == Customer_time);

                                    if (show != null)
                                    {
                                        List<int> Avial = show.AvailableSeats(Customer_time);

                                        foreach (var item in Avial)
                                        {
                                            Console.WriteLine(item);
                                        }

                                        Console.WriteLine("Enter the no of Ticket you want");
                                        List<int> seat_list = new List<int>();
                                        int NumberOfTickets = Int32.Parse(Console.ReadLine());
                                        decimal Total_Price = 0;
                                        for (int i = 1; i <= NumberOfTickets; i++)
                                        {
                                            Console.WriteLine("Enter the seat Number");
                                            int seatNo = Int32.Parse(Console.ReadLine());
                                            string msg;
                                            customer.BookTickets(movie, Customer_time, seatNo, out msg);

                                            if (msg != "Seat booked successfully")
                                            {
                                                Console.WriteLine(msg);
                                                i -= 1;
                                                Console.WriteLine("Enter other seat number");
                                            }
                                            else if (msg == "Seat booked successfully")
                                            {
                                                seat_list.Add(seatNo);
                                                Seat seat = show.seats.Find(s => s.seatNumber == seatNo);
                                                Total_Price += seat.price;
                                            }
                                        }
                                        Booking placed = new Booking(movie_title, Customer_time, seat_list, Total_Price);

                                        customer.bookings.Add(placed);

                                        Console.WriteLine($"Your Total Bill amount {Total_Price}");
                                    }
                                }
                                break;
                            }

                        case 3:
                            {
                                customer.view_info();
                                break;
                            }
                        case 4:
                            {
                                customer.call_contact();
                                break;
                            }

                        case 2:
                            {
                                foreach (var item in customer.bookings)
                                {
                                    Console.WriteLine("------");

                                    Console.WriteLine($"Movie:{item.movie_title}");
                                    Console.WriteLine($"Show:{item.showTime}");
                                    Console.WriteLine("Seats");
                                    foreach (var seat in item.seats)
                                    {
                                        Console.WriteLine(seat);
                                    }
                                    Console.WriteLine($"Total Price {item.total_price}");
                                }
                                break;
                            }
                        case 0:
                            {
                                Console.WriteLine("Thanks for comming");
                                break;
                            }

                        default:
                            {
                                Console.WriteLine("Invalid Choice");
                                Console.WriteLine("Enter valid choice");
                                break;

                            }
                    }
                    if (choice == 0)
                    {
                        break;
                    }
                }

            }
            Console.WriteLine("If need to Relogin .... Type 1");
            int c = Int32.Parse(Console.ReadLine());
            if (c != 1)
            {
                break;
            }
        }
    }
}

