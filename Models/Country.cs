namespace server;

using MySql.Data.MySqlClient;

class Countries
{
    static List<Hotels> Country = new();

    public record Get_Data(int Id, string country);
    public static async Task<List<Get_Data>> Get(Config config)
    {
        List<Get_Data> result = new();
        string query = "SELECT id, country_name FROM country";
        using (var reader = await MySqlHelper.ExecuteReaderAsync(config.db, query))
        {
            while (reader.Read())
            {
                result.Add(new(reader.GetInt32(0), reader.GetString(1)));
            }
        }
        return result;
    }



    public record GetById_Data(string Name);
    public static async Task<GetById_Data?>
    GetById(int id, Config config)
    {
        GetById_Data? result = null;
        string queryid = "SELECT country_name, id FROM country WHERE id = @id";
        var parameters = new MySqlParameter[]
        {
            new ("@id", id),
        };

        using (var reader = await MySqlHelper.ExecuteReaderAsync(config.db, queryid, parameters))
        {
            while (reader.Read())
            {
                result = new(reader.GetString(0));
            }
        }
        
        return result;
    }
}