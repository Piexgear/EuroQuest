namespace server;

using MySql.Data.MySqlClient;

class Countries
{
    //record used to store data returned from the db 
    //id = country id , countries = country name 
    public record Get_Data(int Id, string countries);

    //a method that retrieves all countries from the db
    public static async Task<List<Get_Data>> Get(Config config)
    {
        //list that will store the result from db
        List<Get_Data> result = new();

        //sql query to fetch wanted data 
        string query = "SELECT id, country_name FROM countries";

        //execute the query asynchronously and get a data reader
        using (var reader = await MySqlHelper.ExecuteReaderAsync(config.db, query))
        {
            //read each row returned from db
            while (reader.Read())
            {
                //add each row to the result list 
                result.Add(new(reader.GetInt32(0), reader.GetString(1)));
            }
        }
        //return the list of countries 
        return result;
    }

}