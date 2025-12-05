namespace server;
using MySql.Data.MySqlClient;

class Countrys
{
    static List<Hotels> Country = new();

    public record Get_Data(int Id, string country);
    public static async Task<List<Get_Data>> Get(Config config)
    {
        List<Get_Data> result = new();
        string query = "SELECT id, country_name, FROM country";
        using(var reader = await MySqlHelper.ExecuteReaderAsync(config.db, query))
        {
            while(reader.Read())
            {
                result.Add(new(reader.GetInt32(0), reader.GetString(1)));
            }
        }
        return result;
    }
}