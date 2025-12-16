namespace server;

using System.Data.Common;
using System.Security.Cryptography.X509Certificates;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;

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

        using (var reader = await MySqlHelper.ExecuteReaderAsync(config.db, query))
        {
            while (reader.Read())
            {
                result.Add(new Get_Data(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.IsDBNull(5) ? 0 : reader.GetInt32(5), reader.GetDateTime(6), reader.GetDateTime(7), reader.GetInt32(8)
                ));
            }
        }
        return result;
    }








    // =====================Mål att göra denna för den inloggade användaren=========================
    public record GetByUser_Data(int BookingId, string Customer, string Country, string City, string Hotel, int Rooms, DateTime CheckIn, DateTime CheckOut, int guests);
    public static async Task<List<Get_Data>> GetByUser_data(GetByUser_Data userid, Config config, HttpContext ctx)
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

        using (var reader = await MySqlHelper.ExecuteReaderAsync(config.db, query))
        {
            while (reader.Read())
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



// Class for amount of people they can choose
public record Get_Data(int Guests);   // the user can only send how many guests they would like to be

public static int Get(Get_Data data)    // 
{
    if (data.Guests < 1 || data.Guests > 10)
    {
        throw new Exception("Amount of guests must be between 1 - 10.");
    }

    return data.Guests;
}



// Class för avbokning
public record Cancel_Data(int BookingId);

public static async Task Cancel(Cancel_Data data, Config config)
{
    const string cancelQuery = @"
    UPDATE bookings SET is_cancelled = 1 WHERE id = @bookingId;
    ";

    var parameters = new MySqlParameter[]
    {
        new("@bookingId", data.BookingId)
    };

    int rows = await MySqlHelper.ExecuteNonQueryAsync(config.db, cancelQuery, parameters);
    if ( rows == 0)
    {
        throw new Exception("No booking was found with the id");
    }
}




// Class för ändrade bokningar
public record Update_Data(
int BookingId,
DateOnly NewCheckIn,
DateOnly NewCheckOut,
int NewGuests
);

public static async Task Update(Update_Data data, Config config)
{
    if (data.NewGuests < 1 || data.NewGuests > 10)
        throw new Exception("Amount of guests must be between 1 - 10");

    if (data.NewCheckIn >= data.NewCheckOut)
        throw new Exception("Check-in must be before check-out.");

    const string getPackageQuery = @"
        SELECT package FROM bookings Where id = @bookingId;
        ";

    var getPackageParams = new MySqlParameter[]
    {
            new("@bookingId", data.BookingId)
    };

    object packageResult = await MySqlHelper.ExecuteScalarAsync(config.db, getPackageQuery, getPackageParams);

    int packageId = Convert.ToInt32(packageResult);

    const string updateBookingQuery = @"
    UPDATE bookings 
    SET check_in = @checkIn,
    check_out = @checkOut,
    guests = @guests
    WHERE id = @bookingId;
    ";

    var updateBookingParams = new MySqlParameter[]
    {
        new("@checkIn", data.NewCheckIn.ToDateTime(TimeOnly.MinValue)),
        new("@checkOut", data.NewCheckOut.ToDateTime(TimeOnly.MinValue)),
        new("@guests", data.NewGuests),
        new("@bookingId", data.BookingId),
    };

    int rows = await MySqlHelper.ExecuteNonQueryAsync(config.db, updateBookingQuery, updateBookingParams);

    if (rows == 0)
        throw new Exception("No booking updated");
}