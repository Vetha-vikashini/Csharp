namespace MovieTicketBooking.Users;

class Admin : User
{
    private string Department;

    public Admin(int id, string name, string dept)
        : base(id, name, "Admin")
    {
        Department = dept;
    }

    public override void ViewProfile()
    {
        Console.WriteLine($"Admin ID: {UserId}");
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Department: {Department}");
    }

    public override void ContactHelp()
    {
        Console.WriteLine("Contact system administrator");
    }
}
