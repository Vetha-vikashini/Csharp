using MovieTicketBooking.Services;

namespace MovieTicketBooking.Menus
{
    public class AdminMenu
    {
        MovieRepository movieRepo = new();
        ShowRepository showRepo = new();

        public void Show()
        {
            try {
                string needToContinue;
                do {
                    Console.WriteLine("--Your work Category--");
                    Console.WriteLine("1.Movie 2.Show");
                    Console.Write("Choose (1 or 2) :");
                    var ch = Console.ReadLine();

                    if (ch == "1")
                    {
                        MovieMenu movieMenu = new MovieMenu();
                        movieMenu.MovieHandler();
                    }
                    else if (ch == "2")
                    {
                        ShowMenu showMenu = new ShowMenu();
                        showMenu.ShowHandler();
                    }
                    Console.Write("Need To Continue(Yes or No) : ");
                    needToContinue = Console.ReadLine()!;

                } while (needToContinue.ToUpper() == "YES");

                Console.WriteLine("*****Thanks for You job*****");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exeption {e} is occured");
            }
        }
    }
}
