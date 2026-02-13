using Microsoft.Data.SqlClient;
using MovieTicketBooking.Data;
using MovieTicketBooking.Models;
using System.Data;

namespace MovieTicketBooking.Services
{
    public class BookingService
    {
        public void Book(int userId, int showId, string seatNumbers, decimal price)
        {
            using var con = DbConnection.Get();
            var cmd = new SqlCommand("sp_BookSeats", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@ShowId", showId);
            cmd.Parameters.AddWithValue("@SeatNumbers", seatNumbers);
            cmd.Parameters.AddWithValue("@Price", price);

            con.Open();
            cmd.ExecuteNonQuery();

            Console.WriteLine("Booking successful!");
        }


        public void ViewBookings(int userId)
        {
            using var con = DbConnection.Get();
            var cmd = new SqlCommand("sp_GetUserBookings", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@userId", userId);
            con.Open();
            var r = cmd.ExecuteReader();
            while (r.Read())
            {
                Console.WriteLine(
                     $"{r["Title"]} | {r["ShowTime"]} | Seat {r["SeatNumber"]} | " +
                     $"Seat Price ₹{r["Price"]} | Total ₹{r["TotalAmount"]}");

            }
        }
    }
}
