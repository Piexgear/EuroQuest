namespace server;

using MySql.Data.MySqlClient;

class Hotels
{
    static List<Hotels> hotel = new();

    public record Get_Data(int Id, string HotelName, string Description, int Beachdistance);
    public static async Task<List<Get_Data>> Get(Config config)
    {
        List<Get_Data> result = new();
        string query = "SELECT id, name, description, beach_distance FROM hotels";
        using (var reader = await MySqlHelper.ExecuteReaderAsync(config.db, query))
        {
            while (reader.Read())
            {
                result.Add(new(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3)));
            }
        }
        return result;
    }

    public static async Task<List<Get_Data>> GetCityHotels(int cityId, Config config)
    {
        List<Get_Data> result = new();
        string query = "SELECT id, name, description, beach_distance FROM hotels WHERE city = @cityId";
        var parameters = new MySqlParameter[]
        {
            new ("@cityId", cityId),
        };

        using (var reader = await MySqlHelper.ExecuteReaderAsync(config.db, query, parameters))
        {
            while (reader.Read())
            {
                result.Add(new(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3)));
            }
        }
        return result;
    }
}