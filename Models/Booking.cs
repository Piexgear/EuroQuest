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



// Class för antal gäster som användaren väljer
class AmountOfPeople
{
    public record Get_Data(
        int Id,               // bookings ID 
        int Package,          // Paket - ID kopplat till bokningen
        int User,             // Användarens ID
        DateOnly CheckIn,     // Datum för incheckning
        DateOnly CheckOut,    // Datum för utcheckning
        int Guests            // Antal gäster i bokningen
    );


// Hämtar bokningsdata från databasen
    public static async Task<List<Get_Data>> Get(Config config)
    {
        // Skapar en tom lista som ska fyllas med Get_Data objekt
        List<Get_Data> result = new();

        // SQL fråga som hämtar nödvändiga kolumner från bookings tabellen
        string query = "SELECT id, package, user, check_in, check_out, guests FROM bookings";

        // Kör SQL frågan och får tillbaka en reader som läser rad för rad
        using (var reader = await MySqlHelper.ExecuteReaderAsync(config.db, query))
        {
            // Loopa igenom varje rad i resultatet
            while (reader.Read())
            {
                // Lägger till ett nytt Get_Data objekt i listan
                result.Add(new(
                    reader.GetInt32(0),   // Läser ID (kolumn 0)
                    reader.GetInt32(1),   // Läser PackageID (kolumn 1)
                    reader.GetInt32(2),   // Läser UserID (kolumn 2)
                    DateOnly.FromDateTime(reader.GetDateTime(3)),     // Konverterar check_in från DateTime --> DateOnly
                    DateOnly.FromDateTime(reader.GetDateTime(4)),     // Konverterar check_out från DateTime --> DateOnly 
                    reader.GetInt32(5)    // Läser antal gäster (kolumn 5)
                ));
            }
        }
        // Returnerar listan med alla hämtade bokningar
        return result;
    }
}


// Class för att kunna avboka och FÖRHOPPNINGSVIS spara avbokade i vår package
class CancelBooking
{
    // public metod som körs när användaren avbokar en bokning
    // bookingId = vilken bokning som ska avbokas
    // config = databas - inställningar (connection string)
    public static async Task Delete(int bookingId, Config config)
    {
        // ==================================
        //  1. Markerar bokning som avbokad
        // ==================================

        // SQL query som ändrar bokningen så att den är satt på avbokad
        // men den raderas INTE från databasen
        const string cancelBookingQuery = "UPDATE bookings SET is_cancelled = 1 WHERE id = @bookingId";

        // Skapar en parameterlista till query
        // Här binder vi värdet bookingId till @bookingId i SQL
        var cancelParams = new MySqlParameter[]
        {
            new("@bookingId", bookingId)
        };

        // Kör SQL kommandot mot databasen 
        // ExecuteNonQueryAsync används för UPDATE/DELETE 
        await MySqlHelper.ExecuteNonQueryAsync(config.db, cancelBookingQuery, cancelParams);  // 1. config.db = databas connection. 2. cancelBookingQuery = SQL kod.  3. cancelParams = Parametrar


        // =================================================================
        //  2. Tar bort rumsbokningar (FRIVILLIGT)
        // =================================================================

        // Detta gör att rummet blir lediga igen.
        // Själva bokningen finns kvar i bookings tabellen
        // men kopplingen till bokade rum tas bort
        const string deleteRoomBookingQuery = "DELETE FROM room_booking WHERE booking = @bookingId";

        // Parametrar för att koppla värdet bookingId till SQL
        var roomParams = new MySqlParameter[]
        {
            new("@bookingId", bookingId)
        };

        // Kör DELETE på room_booking så rummen frigörs
        await MySqlHelper.ExecuteNonQueryAsync(config.db, deleteRoomBookingQuery, roomParams);
    }
}



// ==================================================================================
// Class: ChangeBooking
// Låta användaren ändra sin befintliga bokning (paket, datum och antal gäster)
// ===================================================================================
class ChangeBooking
{

    // -------------------------------------------------------
    // 1. Record som beskriver vilka ändringar användaren gör
    // -------------------------------------------------------
      public record Change_Data(
        int BookingId,            // ID på bokningen som ska ändras 
        int NewPackageId,         // Nytt paket (package_id)
        DateOnly NewCheckIn,      // Nytt datum för check in
        DateOnly NewCheckOut,     // Nytt datum för check out
        int NewGuests             // Nytt antal gäster
    );

    // -------------------------------------------------------------------
    // Metod som uppdaterar bokningen i databasen
    // data = alla nya värden
    // config = innehåller connection string (config.db)
    // -------------------------------------------------------------------
    public static async Task UpdateBooking(Change_Data data, Config config)
    {
        // SQL query som uppdaterar en rad i tabellen bookings
        // Vi ändrar paket, datum och antal gäster för en viss bokning (id)
        const string updateBookingQuery = @"
        UPDATE bookings
        SET package = @package,
        check_in = @check_in,
        check_out = @check_out,
        guests = @guests
        WHERE id = @id;
        ";

// Skapar en lista med parametrar som matchar @ värden i SQL queryn
        var updateParams = new MySqlParameter[]
        {
            // ID på bokningen som ska uppdateras
            new("@id", data.BookingId),
            // För nytt paket
            new("@package", data.NewPackageId),
            // DateOnly --> DateTime innan vi skickar till databasen
            new("@check_in", data.NewCheckIn.ToDateTime(TimeOnly.MinValue)),
            new("@check_out", data.NewCheckOut.ToDateTime(TimeOnly.MinValue)),
            // Nytt antal gäster
            new("@guests", data.NewGuests)
        };

        // Kör UPDATE - kommandot mot databasen (ingen data kommer att returneras)
        await MySqlHelper.ExecuteNonQueryAsync(config.db, updateBookingQuery, updateParams);    // 1. config.db = connection string.  2. updateBookingQuery = vår SQL text.  3. updateParams = parametrarna vi just skapade.
    }
}