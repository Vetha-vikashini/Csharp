using MovieTicketBooking.Managers;
using MovieTicketBooking.Models;
using MovieTicketBooking.Users;
using MovieTicketBooking.Services;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MovieTicketBooking;

class Program
{
    static void Main()
    {
        MovieManager manager = new MovieManager();  // Single instance of MovieManager
        BookingService bookingService = new BookingService();

        while (true)
        {
            Console.WriteLine("\nLogin as Admin or Customer");
            string role = Console.ReadLine();

            Console.Write("Enter Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter ID: ");
            int id = int.Parse(Console.ReadLine());


            if (role == "Admin")
            {
                Console.Write("Department: ");
                string dept = Console.ReadLine();
                int choice;

                Admin admin = new Admin(id, name, dept);
                do
                {

                    Console.WriteLine("1.Add Movie  2.Add Show  3.View Movies   4.View Profile  5.HelpLine  0.Exit");
                    choice = int.Parse(Console.ReadLine());

                    if (choice == 1)
                    {
                        Console.Write("Movie Title: ");
                        string title = Console.ReadLine();
                        Console.Write("Movie ID: ");
                        int mid = int.Parse(Console.ReadLine());

                        manager.AddMovie(title, mid);
                    }
                    else if (choice == 2)
                    {
                        Console.Write("Movie ID: ");
                        int mid = int.Parse(Console.ReadLine());
                        Movie movie = manager.GetMovieById(mid);

                        if (movie != null)
                        {
                            Console.Write("Show Time: ");
                            TimeSpan time = TimeSpan.Parse(Console.ReadLine());
                            Console.Write("Seats: ");
                            int seats = int.Parse(Console.ReadLine());
                            Console.Write("Price: ");
                            decimal price = decimal.Parse(Console.ReadLine());

                            movie.AddShow(time, seats, price);
                        }
                        else
                        {
                            Console.WriteLine("No Movie Found");
                        }
                    }

                    else if (choice == 3)
                    {
                        foreach (var movie in manager.Movies)
                        {
                            Console.WriteLine($"Movie: {movie.Title} (ID: {movie.MovieId})");
                        }
                    }

                    else if (choice == 4)
                    {
                        admin.ViewProfile();
                    }

                    else if (choice == 5)
                    {
                        admin.ContactHelp();
                    }

                    else if (choice == 0)
                    {
                        Console.WriteLine("Thank You....");
                    }
                } while (choice != 0);
            }
            else if (role == "Customer")
            {
                Customer customer = new Customer(id, name);

                Console.WriteLine("----Available Movies:----");
                foreach (var m in manager.Movies)
                {
                    Console.WriteLine($"{m.Title} ");
                }
                Console.WriteLine("-------------------------");
                int choice;
                do {
                    Console.WriteLine("1.Book Tickets   2.View Profile  3.HelpLine  0.Exit");
                    choice = int.Parse(Console.ReadLine());
                    if (choice == 1)
                    {
                        Console.Write("Movie Name: ");
                        string movieName = Console.ReadLine();
                        Movie movie = manager.GetMovieByTitle(movieName);

                        if (movie == null)
                        {
                            Console.WriteLine("No movie found");
                        }  // Need to work
                        else
                        {
                            Console.WriteLine("Available Shows:");
                            foreach (var s in movie.Shows)
                            {
                                Console.WriteLine($"Show Time: {s.ShowTime}");
                            }
                            Console.Write(" Desirable Show Time: ");
                            TimeSpan time = TimeSpan.Parse(Console.ReadLine());
                            Show show = movie.Shows.Find(s => s.ShowTime == time);

                            if (show == null)  // Need to work

                            {
                                Console.WriteLine("No Show Found");

                            }
                            else
                            {
                                Console.WriteLine("Available Seats:");
                                foreach (int s in show.GetAvailableSeats())
                                    Console.Write(s + " ");
                                List<int> avail = show.GetAvailableSeats();
                                List<int> seatsToBook = new();


                                int NoOfTickets;
                                int k = 1;
                                do
                                {
                                    if (k != 1)
                                    {
                                        Console.WriteLine("Not enough seats available.");
                                    }
                                    Console.Write("\nNumber of Tickets: ");
                                    NoOfTickets = int.Parse(Console.ReadLine());
                                    k += 1;
                                } while (NoOfTickets > show.GetAvailableSeats().Count);


                                for (int i = 1; i <= NoOfTickets; i++)
                                {
                                    Console.Write("Enter Seat Number: ");
                                    seatsToBook.Add(int.Parse(Console.ReadLine()));
                                }

                                try
                                {
                                    Booking booking = bookingService.BookTickets(
                                        customer,
                                        movie,
                                        show,
                                        seatsToBook
                                    );
                                    Console.WriteLine();
                                    Console.WriteLine("Booking Successful!");
                                    Console.WriteLine($"Movie: {booking.MovieTitle}");
                                    Console.WriteLine($"Show Time: {booking.ShowTime}");
                                    Console.WriteLine($"Seats: {string.Join(", ", booking.SeatNumbers)}");
                                    Console.WriteLine($"Total Amount: ₹{booking.TotalAmount}");
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Booking Failed");
                                    Console.WriteLine(ex.Message);
                                }

                            }
                        }
                    }
                    else if (choice == 2)
                    {
                        customer.ViewProfile();
                    }
                    else if (choice == 3)
                    {
                        customer.ContactHelp();
                    }
                    else if (choice == 0)
                    {
                        Console.WriteLine("Thank You....");
                    }
                 } while (choice != 0);
            }

            Console.Write("\nContinue? (y/n): ");
            if (Console.ReadLine() != "y")
                break;
        }
    }
}
