namespace server;

using MySql.Data.MySqlClient;

class Bookings
{

    // Visa vilka bokningar användara har!   ADMIN VIEW 
    public record Get_Data(int BookingId, string Customer, string Country, string City, string Hotel, int Rooms, DateTime CheckIn, DateTime CheckOut, int guests);
    public static async Task<List<Get_Data>> GetBookings(Config config, HttpContext ctx)
    {
        if (ctx.Session.GetString("role") == Role.admin.ToString())
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
            
            JOIN users u 
            ON b.user = u.id

            JOIN packages p 
            ON b.package = p.id
            
            JOIN hotels h 
            ON p.hotel = h.id
            
            JOIN cities ci 
            ON h.city = ci.id
            
            JOIN countries c 
            ON ci.country = c.id
            
            LEFT JOIN room_bookings rb 
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
        else
        {
            return null;
        }
    }


    // this block is to view booking for the logged in user and not any other, The reason for it to be a list is because one user can have many bookings.
    public record GetByUser_Data(int UserId);
    public static async Task<List<Get_Data>> GetByUser_data(Config config, HttpContext ctx)
    {
        // get the users ID from the ongoing session, If there is no session the result is null.
        int? userid = ctx.Session.GetInt32("user_id");

        // if the session is not avalible (not logged in/ not loaded) reurn null 
        if (!ctx.Session.IsAvailable)
        {
            return null;
        }

        // create a list that will be filled with the booking data and get returned.
        List<Get_Data> result = new();

        // SQL question that gets all bookings for the active user
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
            
            JOIN users u 
            ON b.user = u.id

            JOIN packages p 
            ON b.package = p.id
            
            JOIN hotels h 
            ON p.hotel = h.id
            
            JOIN cities ci 
            ON h.city = ci.id
            
            JOIN countries c 
            ON ci.country = c.id
            
            LEFT JOIN room_bookings rb 
            ON b.id = rb.booking
            
            LEFT JOIN rooms r 
            ON rb.room = r.id

            WHERE u.id = @user_id
            ORDER BY b.id, r.number;
        """;

        // create an array parameters with user_id that gets sent in sql question
        var parameters = new MySqlParameter[]
        {
            new("@user_id", userid)
        };

        // runs SQL question asynct and gets a reader that reades line for line
        using (var reader = await MySqlHelper.ExecuteReaderAsync(config.db, query, parameters))
        {
            // reads every line in the result
            while (reader.Read())
            {
                result.Add(new Get_Data(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.IsDBNull(5) ? 0 : reader.GetInt32(5), reader.GetDateTime(6), reader.GetDateTime(7), reader.GetInt32(8)
                ));
            }
        }
        // return a list with all bookings for the user
        return result;
    }



    // This block is for user to be able to choose how many guests they are
    public record SetGuests_Data(int BookingId, int Guests);

    public static async Task SetGuests(SetGuests_Data data, Config config)    // POST method: sets the number of guests for a booking
    {
        if (data.Guests < 1 || data.Guests > 10)   // Validate that the number of guests is between 1 to 10
            throw new Exception("Amount of guests must be between 1 -10");

        const string query = @"
            UPDATE booking
            SET guests = @guests
            WHERE id = @bookingId;
            ";   // SQL query that updates the guests column for a specific booking

        var parameters = new MySqlParameter[]   // SQL parameters to prevent SQL injection
        {
                new("@guests", data.Guests),   // Sets the new guest count
                new("@bookingId", data.BookingId)   // specifies which booking to update
        };

        int rows = await MySqlHelper.ExecuteNonQueryAsync(config.db, query, parameters);   // Execute the UPDATE query

        if (rows == 0)   // If no rows were updated, the booking ID does not exist
            throw new Exception("No booking was updated");
    }


    // This block is for cancelling a booking
    public record Cancel_Data(int BookingId);

    public static async Task Cancel(Cancel_Data data, Config config)   // DELETE method: should mark as cancelled
    {
        const string CancelQuery = @"
        UPDATE bookings
        SET is_cancelled = 1
        WHERE id = @bookingId;
        ";   // SQL query that marks the booking as cancelled instead of deleting it

        var parameters = new MySqlParameter[]    // SQL parameters for the booking ID 
        {
            new("@bookingId", data.BookingId)
        };

        int rows = await MySqlHelper.ExecuteNonQueryAsync(config.db, CancelQuery, parameters);    // Execute the UPDATE query

        if (rows == 0)    // If no rows were updated, the booking was not found
        {
            throw new Exception("No booking was found with the ID");
        }
    }

    // This block is for changes/updates on booking
    public record Update_Data(
    int BookingId,
    DateOnly NewCheckIn,
    DateOnly NewCheckOut,
    int NewGuests
    );

    public static async Task Update(Update_Data data, Config config)    // PUT method: updates dates and guests for a booking
    {
        if (data.NewGuests < 1 || data.NewGuests > 10)    // validates number of guests
            throw new Exception("Amount of guests must be between 1 - 10");

        if (data.NewCheckIn >= data.NewCheckOut)   // validates that check in is before check out
            throw new Exception("check-in must be before check-out");

        const string getPackageQuery = @"
        SELECT package
        FROM bookings
        WHERE id = @bookingId;
        ";   // SQL query that retrieves the package for the selected booking

        var getPackageParams = new MySqlParameter[]    //Creates SQL parameters used to safely pass values into the database query
        {
            new("@bookingId", data.BookingId)
        };

        object packageResult = await MySqlHelper.ExecuteNonQueryAsync(config.db, getPackageQuery, getPackageParams);   // Execute the query and retrieves the package result from the database

        int packageId = Convert.ToInt32(packageResult);   // Converts the returned package value to an integer

        const string updateBookingQuery = @"
        UPDATE bookings
        SET check_in = @checkIn,
        check_out = @checkOut,
        guests = @guests
        WHERE id = @bookingId;
        ";   // SQL query that updates booking details

        var updateBookingParams = new MySqlParameter[]   // SQL parameters for the update
        {
            // Convert DateOnly to DateTime for MySQL
            new("@checkIn", data.NewCheckIn.ToDateTime(TimeOnly.MinValue)),
            new("@checkOut", data.NewCheckOut.ToDateTime(TimeOnly.MinValue)),
            new("@guests", data.NewGuests),
            new("@bookingId", data.BookingId),
        };

        // Execute the UPDATE query
        int rows = await MySqlHelper.ExecuteNonQueryAsync(config.db, updateBookingQuery, updateBookingParams);

        if (rows == 0)   // If no rows were updated, something went wrong
            throw new Exception("No booking updated");
    }
}