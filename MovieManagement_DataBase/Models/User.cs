//namespace MovieTicketBooking.Models
//{
//    public class User
//    {
//        public int UserId { get; set; }
//        public string Username { get; set; } = "";
//        public string Role { get; set; } = "";
//    }
//}

namespace MovieTicketBooking.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; } = "";

        // Keep this for DB mapping
        public string Role { get; set; } = "";

        // Enum wrapper (does NOT affect database)
        public UserRole RoleEnum
        {
            get
            {
                return Enum.TryParse<UserRole>(Role, out var role)
                    ? role
                    : UserRole.Customer; // fallback
            }
            set
            {
                Role = value.ToString();
            }
        }
    }
}
