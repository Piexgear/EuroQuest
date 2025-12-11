namespace server;

using MySql.Data.MySqlClient;

class AmountOfPeople
{
    public record Get_Data(
        int Id,
        int Package,
        int User,
        DateOnly CheckIn,
        DateOnly CheckOut,
        int Guests
    );

    public static async Task<List<Get_Data>> Get(Config config)
    {
        List<Get_Data> result = new();
        string query = "SELECT id, package, user, check_in, check_out, guests FROM bookings";

        using (var reader = await MySqlHelper.ExecuteReaderAsync(config.db, query))
        {
            while (reader.Read())
            {
                result.Add(new(
                    reader.GetInt32(0),
                    reader.GetInt32(1),
                    reader.GetInt32(2),
                    reader.GetDateTime(3).Date,
                    reader.GetDateTime(4).Date,
                    reader.GetInt32(5)
                ));
            }
        }
        return result;
    }
}