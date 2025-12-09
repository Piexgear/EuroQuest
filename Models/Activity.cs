namespace server;

using MySql.Data.MySqlClient;

class Activities
{
    static List<Hotels> hotel = new();

    public record Get_Data(int Id, string Name, int Duration, int Price, string Adress, string Description);
    public static async Task<List<Get_Data>> Get(Config config)
    {
        List<Get_Data> result = new();
        string query = "SELECT id, name, duration, price, address, description FROM activities";
        using (var reader = await MySqlHelper.ExecuteReaderAsync(config.db, query))
        {
            while (reader.Read())
            {
                result.Add(new(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3),
                reader.GetString(4), reader.GetString(5)));
            }
        }
        return result;
    }
}