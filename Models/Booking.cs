namespace server;

using MySql.Data.MySqlClient;

class Bookings
{
    static List<Hotels> booking = new();


    // finns inget kul att visa så man kan ta bort hela blocket
    /*
        public record Get_Data(int Id, string City, int CoutryId);
        public static async Task<List<Get_Data>> Get(Config config)
        {
            List<Get_Data> result = new();
            string query = "SELECT id, city_name, country_id FROM city";
            using(var reader = await MySqlHelper.ExecuteReaderAsync(config.db, query))
            {
                while(reader.Read())
                {
                    result.Add(new(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2)));
                }
            }
            return result;
        }
        */


}