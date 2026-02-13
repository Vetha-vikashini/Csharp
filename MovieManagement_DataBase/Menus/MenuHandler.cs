using MovieTicketBooking.Models;

namespace MovieTicketBooking.Menus
{
    public class MenuHandler
    {
        public void Start(User user)
        {
            if (user.Role == "Admin")
                new AdminMenu().Show();
            else
                new CustomerMenu().Show(user);
        }
    }
}
