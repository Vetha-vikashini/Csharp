using MovieTicketBooking.Data;
using MovieTicketBooking.Models;
using Microsoft.Data.SqlClient;

namespace MovieTicketBooking.Services
{
    public class MovieRepository
    {
        public void AddMovie(string title)
        {
            try
            {
                using var con = DbConnection.Get();
                var cmd = new SqlCommand("sp_AddMovie", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@title", title);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public void UpdateMovie(int movieId, string title)
        {
            try
            {
                using var con = DbConnection.Get();
                var cmd = new SqlCommand("sp_UpdateMovie", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@movieId", movieId);
                cmd.Parameters.AddWithValue("@title", title);

                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DeleteMovie(int movieId)
        {
            try
            {
                using var con = DbConnection.Get();
                var cmd = new SqlCommand("sp_DeleteMovie", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@movieId", movieId);

                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void GetMovies()
        {
            try
            {
                var list = new List<Movie>();
                using var con = DbConnection.Get();
                var cmd = new SqlCommand("sp_GetMovies", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    list.Add(new Movie
                    {
                        MovieId = (int)r["MovieId"],
                        Title = r["Title"].ToString()!
                    });
                }
                foreach (var m in list)
                    Console.WriteLine($"{m.MovieId}. {m.Title}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
