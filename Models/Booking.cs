namespace server;

using System.Security.Cryptography.X509Certificates;
using MySql.Data.MySqlClient;

class Bookings
{
    static List<Bookings> booking = new();


    // Visa vilka bokningar användara har!   ADMIN VIEW 
    public record Get_Data(int BookingId, string Customer, string Country, string City, string Hotel, int Rooms, DateTime CheckIn, DateTime CheckOut, int guests);
    public static async Task<List<Get_Data>> GetBookings(Config config)
    {
        List<Get_Data> result = new();
        string query = """
            SELECT 
            b.id AS booking_id,
            u.name AS customer_name,
            c.country_name,
            ci.city_name,
            h.name AS hotel_name,
            r.number AS room_number,
            b.check_in,
            b.check_out,
            b.guests
            FROM bookings b
            JOIN user u 
            ON b.user = u.id
            JOIN package p 
            ON b.package = p.id
            JOIN hotels h 
            ON p.hotel = h.id
            JOIN city ci 
            ON h.city = ci.id
            JOIN country c 
            ON ci.country = c.id
            LEFT JOIN room_booking rb 
            ON b.id = rb.booking
            LEFT JOIN rooms r 
            ON rb.room = r.id
            ORDER BY b.id, r.number;
        """;

       using(var reader = await MySqlHelper.ExecuteReaderAsync(config.db, query))
        {
            while(reader.Read())
            {
                result.Add(new Get_Data(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.IsDBNull(5) ? 0 : reader.GetInt32(5), reader.GetDateTime(6), reader.GetDateTime(7), reader.GetInt32(8)
                ));
            }
        }
        return result;
    }








            // =====================Mål att göra denna för den inloggade användaren=========================
    public record GetByUser_Data(int BookingId, string Customer, string Country, string City, string Hotel, int Rooms, DateTime CheckIn, DateTime CheckOut, int guests);
    public static async Task<List<Get_Data>> GetByUser_data(Config config, HttpContext ctx)
    {
        List<Get_Data> result = new();
        bool loggedIn = false;
        string query = """
            SELECT 
            b.id AS booking_id,
            u.name AS customer_name,
            c.country_name,
            ci.city_name,
            h.name AS hotel_name,
            r.number AS room_number,
            b.check_in,
            b.check_out,
            b.guests
            FROM bookings b
            JOIN user u ON b.user = u.id
            JOIN package p ON b.package = p.id
            JOIN hotels h ON p.hotel = h.id
            JOIN city ci ON h.city = ci.id
            JOIN country c ON ci.country = c.id
            LEFT JOIN room_booking rb ON b.id = rb.booking
            LEFT JOIN rooms r ON rb.room = r.id
            ORDER BY b.id, r.number;
        """;
        var parameters = new MySqlParameter[]
        {
            new("@UserId", userid.Customer)
        };
  

        object query_result = await MySqlHelper.ExecuteScalarAsync(config.db, query, parameters);
        if (query_result is int id)
        {
            if (ctx.Session.IsAvailable)
            {
                loggedIn = true;
            }
        }

        using(var reader = await MySqlHelper.ExecuteReaderAsync(config.db, query))
        {
            while(reader.Read())
            {
                result.Add(new Get_Data(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.IsDBNull(5) ? 0 : reader.GetInt32(5), reader.GetDateTime(6), reader.GetDateTime(7), reader.GetInt32(8)
                ));
            }
        }
        if (!loggedIn)
        {
            return null;
        }
        else
        {
        return result;  
        }
    }
    
}