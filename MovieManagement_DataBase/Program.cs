using MovieTicketBooking.Menus;
using MovieTicketBooking.Services;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        AuthService auth = new();
        MenuHandler menu = new();

        Console.WriteLine("1.Login  2.Register");
        var ch = Console.ReadLine();

        if (ch == "2")
        {
            Console.Write("Username: ");
            string name = Console.ReadLine()!;
            Console.Write("Password: ");
            string secretKey = Console.ReadLine()!;
            Console.WriteLine("Select Role");
            Console.WriteLine("1.Admin 2.Customer");
            Console.Write("Choose(1 or 2): ");
            string role =Console.ReadLine()!;
            role = role.Trim();
            if (role == "1")
            {
                role = "Admin";
            }
            else if (role == "2")
            {
                role = "Customer";
            }
            else
                throw new Exception("Invalid role. Allowed roles: Admin or Customer");
            Console.WriteLine(role);

            auth.Register(name, secretKey,role);
            Console.WriteLine("Registration successful. Please login.");
        }

        Console.Write("Username: ");
        string username = Console.ReadLine()!;
        Console.Write("Password: ");
        string password = Console.ReadLine()!;
        var user = auth.Login(username, password);
        if (user == null)
        {
            Console.WriteLine("Invalid credentials");
            return;
        }

        menu.Start(user);
    }
}
