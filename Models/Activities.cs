namespace server;

using MySql.Data.MySqlClient;

class Activities
{
    //record used to store activity data 
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

    //method that retrievs all activitities from db 
    public static async Task<List<Get_Data>> Get(Config config)
    {
        //list that will store all activities 
        List<Get_Data> result = new();

        //sql query to fetch all activities
        string query = "SELECT id, name, duration, price, address, city, capacity, description FROM activities";

        //execute the query asynchronously 
        using (var reader = await MySqlHelper.ExecuteReaderAsync(config.db, query))
        {
            //read each row retruned from db
            while (reader.Read())
            {
                //add activity data to the result list 
                result.Add(new(
                    reader.GetInt32(0), //activity ID
                    reader.GetString(1), //activity name
                    reader.GetInt32(2), //duration(min)
                    reader.GetInt32(3), //price
                    reader.GetString(4), //adress
                    reader.GetInt32(5), //city ID
                    reader.GetInt32(6), //max capacity 
                    reader.GetString(7) //description
                ));
            }
        }

        //return the list of activities 
        return result;
    }

    //method that retrieves activities for a specific city 
    public static async Task<List<Get_Data>> GetCityActivity(int cityId, Config config)
    {
        List<Get_Data> result = new();
        string query = "SELECT id, name, duration, price, address, city, capacity, description FROM activities WHERE city = @cityId";

        //parameters to safely pass the city id to the query 
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
        //return the filtered list of activities 
        return result;
    }
}
