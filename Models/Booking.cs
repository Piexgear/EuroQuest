namespace server;

using MySql.Data.MySqlClient;

class Bookings
{
    static List<Bookings> booking = new();


    // Visa vilka bokningar användaren har!
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
            List<Get_Data> result = new();
            string query = "SELECT id, city_name, country_id FROM city";
            using(var reader = await MySqlHelper.ExecuteReaderAsync(config.db, query))
            {
                result.Add(new Get_Data(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.IsDBNull(5) ? 0 : reader.GetInt32(5), reader.GetDateTime(6), reader.GetDateTime(7), reader.GetInt32(8)
                ));
            }
            return result;
        }
        return result;
    }
}