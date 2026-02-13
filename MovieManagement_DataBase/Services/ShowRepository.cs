using MovieTicketBooking.Data;
using MovieTicketBooking.Models;
using Microsoft.Data.SqlClient;

namespace MovieTicketBooking.Services
{
    public class ShowRepository
    {
        
        public void AddShow(int movieId, TimeSpan showTime, decimal price)
        {
            try
            {
                using var con = DbConnection.Get();
                var cmd = new SqlCommand("sp_AddShow", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@movieId", movieId);
                cmd.Parameters.AddWithValue("@showTime", showTime);
                cmd.Parameters.AddWithValue("@price", price);

                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            { 
                Console.WriteLine(ex.Message);
            }
        }

        public void UpdateShow(int showId, TimeSpan showTime, decimal price)
        {
            try
            {
                using var con = DbConnection.Get();
                var cmd = new SqlCommand("sp_UpdateShow", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@showId", showId);
                cmd.Parameters.AddWithValue("@showTime", showTime);
                cmd.Parameters.AddWithValue("@price", price);

                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            { 
                Console.WriteLine(ex.Message);
            }
        }

        public void DeleteShow(int showId)
        {
            try
            {
                using var con = DbConnection.Get();
                var cmd = new SqlCommand("sp_DeleteShow", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@showId", showId);

                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public List<Show> GetShows(int movieId)
        {
            var list = new List<Show>();
            using var con = DbConnection.Get();
            var cmd = new SqlCommand("sp_GetShowsByMovie", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@movieId", movieId);
            con.Open();
            var r = cmd.ExecuteReader();
            while (r.Read())
            {
                list.Add(new Show
                {
                    ShowId = (int)r["ShowId"],
                    ShowTime = (TimeSpan)r["ShowTime"],
                    Price = (decimal)r["Price"]
                });
            }
            return list;
        }
    }
}
