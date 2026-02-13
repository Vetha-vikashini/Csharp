using MovieTicketBooking.Services;

namespace MovieTicketBooking.Menus
{
    class MovieMenu
    {
        MovieRepository movieRepo = new();

        public void MovieHandler()
        {
            Console.WriteLine("Current Movie List");
            movieRepo.GetMovies();
            Console.WriteLine("1.Add Movie  2.Update Movie  3.Delete Movie");
            Console.WriteLine("Choose: ");
            int choice = int.Parse(Console.ReadLine()!);

            if (choice == 1)
            {
                Console.Write("Movie Title: ");
                movieRepo.AddMovie(Console.ReadLine()!);
            }
            else if (choice == 2)
            {
                Console.Write("Movie Id: ");
                int id = int.Parse(Console.ReadLine()!);
                Console.Write("New Title: ");
                movieRepo.UpdateMovie(id, Console.ReadLine()!);
            }
            else if (choice == 3)
            {
                Console.Write("Movie Id: ");
                int id = int.Parse(Console.ReadLine()!);
                movieRepo.DeleteMovie(id);
            }

        }




    }
}
