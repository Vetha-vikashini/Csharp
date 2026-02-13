using MovieTicketBooking.Data;
using Microsoft.Data.SqlClient;

namespace MovieTicketBooking.Services
{
    public class SeatRepository
    {
        public List<int> GetAvailableSeats(int showId)
        {
            var seats = new List<int>();
            using var con = DbConnection.Get();
            var cmd = new SqlCommand("sp_GetAvailableSeats", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@showId", showId);
            con.Open();
            var r = cmd.ExecuteReader();
            while (r.Read())
                seats.Add((int)r["SeatNumber"]);
            return seats;
        }
    }
}
