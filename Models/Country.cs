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



    public record Get_DataById(int id, string name);
    public static async Task<List<Get_DataById>> GetById(Get_DataById credentials, Config config)
    {
        List<Get_DataById> query_result = new();
        string queryid = "SELECT country_name FROM country WHERE id = @id";
        var parameters = new MySqlParameter[]
        {
            new ("@id", credentials.id),
            new ("@name", credentials.name)

        };

        using (var reader = await MySqlHelper.ExecuteReaderAsync(config.db, queryid))
        {
            while (reader.Read())
            {
                query_result.Add(new(reader.GetInt32(0), reader.GetString(1)));
            }
        }
        return query_result;
    }
}