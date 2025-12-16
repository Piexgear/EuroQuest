namespace server;

using MySql.Data.MySqlClient;

class Hotels
{
    //record used to store hotel data  
    public record Get_Data(int Id, string HotelName, string Description, int Beachdistance);

    //asynchronous method that retrives all hotels from db
    public static async Task<List<Get_Data>> Get(Config config)
    {
        //list that will store all hotels 
        List<Get_Data> result = new();

        //sql query to fetch all hotels 
        string query = "SELECT id, name, description, beach_distance FROM hotels";

        //execute query asynchronously 
        using (var reader = await MySqlHelper.ExecuteReaderAsync(config.db, query))
        {
            //read each row from db 
            while (reader.Read())
            {
                //add hotel data to the result list 
                result.Add(new(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3)));
            }
        }
        //return the list of hotels 
        return result;
    }

    //asynchronous method that retrieves hotels for a specific city 
    public static async Task<List<Get_Data>> GetCityHotels(int cityId, Config config)
    {
        List<Get_Data> result = new();
        string query = "SELECT id, name, description, beach_distance FROM hotels WHERE city = @cityId";

        //parameters to safley pass the city id to the query 
        var parameters = new MySqlParameter[]
        {
            new ("@cityId", cityId),
        };

        using (var reader = await MySqlHelper.ExecuteReaderAsync(config.db, query, parameters))
        {
            while (reader.Read())
            {
                result.Add(new(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetString(2),
                reader.GetInt32(3)));
            }
        }
        //returne the list of hotels for the city 
        return result;
    }
}