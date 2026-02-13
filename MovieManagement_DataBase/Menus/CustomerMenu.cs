using MovieTicketBooking.Models;
using MovieTicketBooking.Services;

namespace MovieTicketBooking.Menus
{
    public class CustomerMenu
    {
        MovieRepository movieRepo = new();
        ShowRepository showRepo = new();
        SeatRepository seatRepo = new();
        BookingService bookingService = new();

        public void BookTickets(User user)
        {
            try
            {
                movieRepo.GetMovies();

                Console.Write("Select Movie Id: ");
                int movieId;
                if (!Int32.TryParse(Console.ReadLine(), out movieId))
                {
                    Console.WriteLine("Invalid input");
                }

                var shows = showRepo.GetShows(movieId);

                foreach (var s in shows)
                    Console.WriteLine($"Show Id :{s.ShowId}.  Show Time: {s.ShowTime}.  Price: ₹{s.Price}");

                Console.Write("Select Show Id: ");
                int showId;
                if (!Int32.TryParse(Console.ReadLine(), out showId))
                {
                    Console.WriteLine("Invalid input");
                }

                //LINQ: get selected show
                var selectedShow = shows.SingleOrDefault(s => s.ShowId == showId);

                if (selectedShow == null)
                {
                    Console.WriteLine("Invalid Show Id");
                    return;
                }

                var seats = seatRepo.GetAvailableSeats(showId);

                Console.WriteLine("Available Seats:");
                seats.ForEach(s => Console.Write(s + " "));

                Console.Write("\nEnter seat numbers (comma separated): ");

                //LINQ: Convert input to List<int>
                var selectedSeats = Console.ReadLine()!
                    .Split(',')
                    .Select(s => int.Parse(s.Trim()))
                    .ToList();

                //LINQ: Validate seats
                var invalidSeats = selectedSeats
                    .Where(s => !seats.Contains(s))
                    .ToList();

                if (invalidSeats.Any())
                {
                    Console.WriteLine("Invalid seat(s): " + string.Join(",", invalidSeats));
                    return;
                }

                //LINQ: Calculate total
                decimal totalAmount = selectedSeats.Count * selectedShow.Price;
                string seatNumbers = string.Join(",", selectedSeats);

                bookingService.Book(user.UserId, showId, seatNumbers, selectedShow.Price);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception {e} is occured");
            }

        }


        public void Show(User user)
        {
            try
            {
                string needToContinue;
                do
                {

                    Console.WriteLine("--Customer Menu--");
                    Console.WriteLine("1.Book Tickets 2.View My Booked Tickets");
                    Console.Write("Choose:");
                    int choice;
                    if (!Int32.TryParse(Console.ReadLine(), out choice))
                    {
                        Console.WriteLine("Invalid option");
                    }
                    switch (choice)
                    {
                        case 1:
                            {
                                BookTickets(user);
                                break;
                            }
                        case 2:
                            {
                                Console.WriteLine("\nMy Bookings:");
                                bookingService.ViewBookings(user.UserId);
                                break;
                            }

                    }
                    Console.Write("Need to Do Continue(Yes or No) : ");
                    needToContinue = Console.ReadLine();

                } while (needToContinue.ToUpper() == "YES");
                Console.WriteLine("*****Thanks for Coming*****");

            }
            catch (Exception e)
            {
                Console.WriteLine($"Exeption {e} is occured");
            }
        }
    }
}
