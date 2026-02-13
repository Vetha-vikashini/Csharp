using Microsoft.Data.SqlClient;
using MovieTicketBooking.Data;

namespace MovieTicketBooking.Services
{
    public static class DatabaseService
    {
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(DbConfig.ConnectionString);
        }
    }
}
