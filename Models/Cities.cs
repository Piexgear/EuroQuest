namespace server;

using MySql.Data.MySqlClient;

class Cities
{
    // static List<Hotels> hotel = new();

    public record Get_Data(int Id, string City, int CountryId);
    public record GetById_Data(int Id, string Name);
    public static async Task<List<Get_Data>> Get(Config config)
    {
        List<Get_Data> result = new();
        string query = "SELECT id, city_name, country_id FROM city";
        using (var reader = await MySqlHelper.ExecuteReaderAsync(config.db, query))
        {
            while (reader.Read())
            {
                result.Add(new(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2)));
            }
        }
        return result;
    }


    public static async Task<List<GetById_Data>> GetByCountryId(int countryId, Config config)
    {
        List<GetById_Data> result = new();
        string query = "SELECT id, city_name FROM city WHERE country_id = @countryId";
        var parameters = new MySqlParameter[]
        {
            new ("@countryId", countryId),
        };

        using (var reader = await MySqlHelper.ExecuteReaderAsync(config.db, query, parameters))
        {
            while (reader.Read())
            {
                result.Add(new(reader.GetInt32(0), reader.GetString(1)));
            }

        }
        return result;
    }
}