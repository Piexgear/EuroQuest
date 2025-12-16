namespace server;

using MySql.Data.MySqlClient;

class Cities
{
    //record used to store city data including its countryId 
    public record Get_Data(int Id, string City, int CountryId);

    //record used to store city data when filtering by country
    public record GetById_Data(int Id, string Name);

    //method that retrieves all cities from db
    public static async Task<List<Get_Data>> Get(Config config)
    {
        //List that will store all cities
        List<Get_Data> result = new();

        //sql query to fetch all cities
        string query = "SELECT id, city_name, country FROM cities";
        //execute the query asynchronously
        using (var reader = await MySqlHelper.ExecuteReaderAsync(config.db, query))
        {
            //read each row from db
            while (reader.Read())
            {
                //add city data to the result list 
                result.Add(new(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2)));
            }
        }
        //return list of cities 
        return result;
    }


    //asynchronous method that retrieves cities by country id 
    public static async Task<List<GetById_Data>> GetByCountryId(int countryId, Config config)
    {
        //list that will store filtered cities 
        List<GetById_Data> result = new();

        //sql query to fetch cities for a specific country 
        string query = "SELECT id, city_name FROM cities WHERE country = @countryId";

        //parameters to prevent sql injection 
        var parameters = new MySqlParameter[]
        {
            new ("@countryId", countryId),
        };

        //execute the query asynchronously with parameters
        using (var reader = await MySqlHelper.ExecuteReaderAsync(config.db, query, parameters))
        {
            //read each row returned from db 
            while (reader.Read())
            {
                //add city data to the result list 
                result.Add(new(reader.GetInt32(0), reader.GetString(1)));
            }

        }
        //retrun the filtered list of cities
        return result;
    }
}