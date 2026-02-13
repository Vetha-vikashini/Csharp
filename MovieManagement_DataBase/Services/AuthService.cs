using Microsoft.Data.SqlClient;
using MovieTicketBooking.Data;
using MovieTicketBooking.Models;
using System.Data;

namespace MovieTicketBooking.Services
{
    public class AuthService
    {
        public User? Login(string username, string password)
        {
            using var con = DbConnection.Get();
            var cmd = new SqlCommand("sp_Login", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);

            con.Open();
            var r = cmd.ExecuteReader();
            if (!r.Read()) return null;

            return new User
            {
                UserId = (int)r["UserId"],
                Username = r["Username"].ToString()!,
                Role = r["Role"].ToString()!
            };
        }

        public void Register(string username, string password,string role)
        {
            using var con = DbConnection.Get();
            var cmd = new SqlCommand("sp_RegisterUser", con);  // calling Stored procedure sp_RegisterUser
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@username", username); // passing values
            cmd.Parameters.AddWithValue("@password", password);
            cmd.Parameters.AddWithValue("@Role", role.Trim());

            con.Open();
            cmd.ExecuteNonQuery();
        }

    }
}
