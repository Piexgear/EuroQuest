namespace server;

using MySql.Data.MySqlClient;

class Packages
{
    public record PackageOutput(
    int PackageId,
    string Country,
    string City,
    HotelOutput Hotel,
    List<ActivityOutput> Activities
    );

    public record HotelOutput(
        string Name,
        string Description,
        int BeachDistance,
        int CenterDistance
    );

    public record ActivityOutput(
        string Name,
        string Description,
        int Duration,
        int Price
    );

    public record Post_Data(int HotelId, List<int> ActivityId);

    public static async Task<int> Post(Post_Data data, Config config)
    {
        // insert package
        string insertPackageQuery = "INSERT INTO packages (hotel) VALUES (@hotelId)";
        var packageParameters = new MySqlParameter[]
        {
            new("@hotelId", data.HotelId)
        };

        await MySqlHelper.ExecuteNonQueryAsync(
            config.db,
            insertPackageQuery, packageParameters);

        // get inserted package id

        int packageId = Convert.ToInt32(
            MySqlHelper.ExecuteScalar(config.db, "SELECT LAST_INSERT_ID()")
        );

        // Insert activities
        string insertActivityQuery = "INSERT INTO package_activities (package, activity) VALUES (@package, @activity)";

        foreach (int activityId in data.ActivityId)
        {
            var activityParameters = new MySqlParameter[]
            {
                new("@package", packageId),
                new("@activity", activityId)
            };

            await MySqlHelper.ExecuteNonQueryAsync(config.db, insertActivityQuery, activityParameters);
        }

        return packageId;
    }

    //get all packages with info
    public static async Task<List<PackageOutput>> GetAll(Config config)
    {
        Dictionary<int, PackageOutput> packages = new();

        // Hämta package, hotel, city, country
        string hotelQuery = """
        SELECT 
            p.id AS packageId,
            co.country_name,
            c.city_name,
            h.name,
            h.description,
            h.beach_distance,
            h.center_distance
        FROM packages p
        JOIN hotels h ON h.id = p.hotel
        JOIN cities c ON c.id = h.city
        JOIN countries co ON co.id = c.country
        ORDER BY p.id;
    """;

        using (var reader = await MySqlHelper.ExecuteReaderAsync(
            config.db,
            hotelQuery))
        {
            while (reader.Read())
            {
                int packageId = reader.GetInt32("packageId");

                var hotel = new HotelOutput(
                    reader.GetString("name"),
                    reader.GetString("description"),
                    reader.GetInt32("beach_distance"),
                    reader.GetInt32("center_distance")
                );

                packages[packageId] = new PackageOutput(
                    packageId,
                    reader.GetString("country_name"),
                    reader.GetString("city_name"),
                    hotel,
                    new List<ActivityOutput>()
                );
            }
        }

        //hämta aktiviteterna för varje paket

        string activitiesQuery = """
        SELECT 
            pa.package AS packageId,
            a.name,
            a.description,
            a.duration,
            a.price
        FROM package_activities pa
        JOIN activities a ON a.id = pa.activity;
    """;

        using (var reader = await MySqlHelper.ExecuteReaderAsync(
            config.db,
            activitiesQuery))
        {
            while (reader.Read())
            {
                int packageId = reader.GetInt32("packageId");

                if (packages.TryGetValue(packageId, out var package))
                {
                    package.Activities.Add(new ActivityOutput(
                        reader.GetString("name"),
                        reader.GetString("description"),
                        reader.GetInt32("duration"),
                        reader.GetInt32("price")
                    ));
                }
            }
        }

        return packages.Values.ToList();
    }

}
