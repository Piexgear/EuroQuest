namespace server;

using MySql.Data.MySqlClient;

class Activities
{
    static List<Activities> activities = new();
    public record Get_Data(
        int Id,
        string Name,
        int Duration,
        int Price,
        string Address,
        int City,
        int Capacity,
        string Description
    );

    public static async Task<List<Get_Data>> Get(Config config)
    {
        List<Get_Data> result = new();

        string query = "SELECT id, name, duration, price, address, city, capacity, description FROM activities";

        using (var reader = await MySqlHelper.ExecuteReaderAsync(config.db, query))
        {
            while (reader.Read())
            {
                result.Add(new(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetInt32(2),
                    reader.GetInt32(3),
                    reader.GetString(4),
                    reader.GetInt32(5),
                    reader.GetInt32(6),
                    reader.GetString(7)
                ));
            }
        }

        return result;
    }

    public static async Task<List<Get_Data>> GetCityActivity(int cityId, Config config)
    {
        List<Get_Data> result = new();
        string query = "SELECT id, name, duration, price, address, city, capacity, description FROM activities WHERE city = @cityId";
        var parameters = new MySqlParameter[]
        {
            new ("@cityId", cityId),
        };

        using (var reader = await MySqlHelper.ExecuteReaderAsync(config.db, query, parameters))
        {
            while (reader.Read())
            {
                result.Add(new(
                    reader.GetInt32(0), //id
                    reader.GetString(1), //name
                    reader.GetInt32(2), //duration
                    reader.GetInt32(3), //price
                    reader.GetString(4), //adress
                    reader.GetInt32(5), //city
                    reader.GetInt32(6), //capacity
                    reader.GetString(7)  //description


                ));
            }
        }
        return result;
    }
}
