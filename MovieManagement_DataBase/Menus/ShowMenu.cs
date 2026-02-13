using MovieTicketBooking.Services;

namespace MovieTicketBooking.Menus
{
    class ShowMenu
    {
        ShowRepository showRepo = new();
        MovieRepository movieRepo = new();  
        public void ShowHandler()
        {
            Console.WriteLine("Current Movie List");
            movieRepo.GetMovies();
            Console.WriteLine("1.Add Show  2.Update Show  3.Delete Show");
            int choice = int.Parse(Console.ReadLine()!);
            
            if (choice == 1)
            {
                Console.Write("Movie Id: ");
                int movieId = int.Parse(Console.ReadLine()!);

                Console.Write("Show Time (HH:mm): ");
                TimeSpan time = TimeSpan.Parse(Console.ReadLine()!);

                Console.Write("Price: ");
                decimal price = decimal.Parse(Console.ReadLine()!);

                showRepo.AddShow(movieId, time, price);
            }
            else if (choice == 2)
            {
                Console.Write("Show Id: ");
                int showId = int.Parse(Console.ReadLine()!);

                Console.Write("New Time (HH:mm): ");
                TimeSpan time = TimeSpan.Parse(Console.ReadLine()!);

                Console.Write("New Price: ");
                decimal price = decimal.Parse(Console.ReadLine()!);

                showRepo.UpdateShow(showId, time, price);
            }
            else if (choice == 3)
            {
                Console.Write("Show Id: ");
                int showId = int.Parse(Console.ReadLine()!);

                showRepo.DeleteShow(showId);
            }

        }
    }

}
