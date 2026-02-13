using Microsoft.Data.SqlClient;

namespace MovieTicketBooking.Data
{
    public static class DbConnection
    {
        public static SqlConnection Get()
        {
            return new SqlConnection(DbConfig.ConnectionString);
        }
    }
}
