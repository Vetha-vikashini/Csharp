namespace MovieTicketBooking.Users;

abstract class User
{
    public int UserId { get; private set; }
    public string Name { get; private set; }
    public string UserType { get; private set; }

    protected User(int id, string name, string type)
    {
        UserId = id;
        Name = name;
        UserType = type;
    }

    public abstract void ViewProfile();
    public abstract void ContactHelp();
}
