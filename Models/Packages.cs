namespace server;

using System.Data;
using MySql.Data.MySqlClient;
class Packages
{
    //data models used to structure API input and output

    //output model for package with hotel and activities
    public record PackageOutput(
    int PackageId,
    string Country,
    string City,
    HotelOutput Hotel,
    List<ActivityOutput> Activities
    );

    // output for hotel information 
    public record HotelOutput(
        string Name,
        string Description,
        int BeachDistance,
        int CenterDistance
    );

    //output model for activities inside a package
    public record ActivityOutput(
        string Name,
        string Description,
        int Duration,
        int Price
    );

    //input model when creating a package 
    //contains hotel id and list of activities
    public record Post_Data(int HotelId, List<int> ActivityId);

    //create a package
    public static async Task<int> Post(Post_Data data, Config config, HttpContext ctx)
    {
        //get logged in userid från session 
        int? userId = ctx.Session.GetInt32("user_id");

        if (userId == null)
            throw new Exception("Ingen användare är inloggad.");

        //check if an identical package already exists
        //a package is identical if:
        //it has de same hotel id and it contains exactly the same activity ids, check with sum and count
        string activityList = string.Join(",", data.ActivityId);

        string checkQuery = $"""
            SELECT p.id
            FROM packages p
            JOIN package_activities pa ON pa.package = p.id
            WHERE p.hotel = @hotelId
            GROUP BY p.id
            HAVING
                COUNT(pa.activity) = @activityCount 
                AND SUM(pa.activity IN ({activityList})) = @activityCount;
        """;

        var checkParameters = new MySqlParameter[]
        {
            new("@hotelId", data.HotelId),
            new("@activityCount", data.ActivityId.Count)
        };

        //execute query to see if an identical package exists
        object? exisitingPackage = MySqlHelper.ExecuteScalar(config.db, checkQuery, checkParameters);

        if (exisitingPackage != null)
        {
            //package already exists, return existing packageid
            return Convert.ToInt32(exisitingPackage);
        }

        // insert package(hotel reference only)
        string insertPackageQuery = "INSERT INTO packages (hotel, created_by) VALUES (@hotelId, @userId)";
        var packageParameters = new MySqlParameter[]
        {
            new("@hotelId", data.HotelId),
            new("@userId", userId.Value)

        };

        await MySqlHelper.ExecuteNonQueryAsync(
            config.db,
            insertPackageQuery, packageParameters);

        // retrieve the id of the newly created package

        int packageId = Convert.ToInt32(
            MySqlHelper.ExecuteScalar(config.db, "SELECT LAST_INSERT_ID()")
        );

        // Insert activities connected to the package
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

        //return the id of the newly created package
        return packageId;
    }

    //get all packages with info
    public static async Task<List<PackageOutput>> GetAll(Config config)
    {
        //dictionary to store packages(key = packageId)
        Dictionary<int, PackageOutput> packages = new();

        // Retrieve all packages with neccesary info 
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

        //execute query and build package objects
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

                //create a package entry in dictionary 
                packages[packageId] = new PackageOutput(
                    packageId,
                    reader.GetString("country_name"),
                    reader.GetString("city_name"),
                    hotel,
                    new List<ActivityOutput>() //empty list, activities added later
                );
            }
        }

        //retrieve activities for all packages

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

                //add each activity to the correct package
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

        //convert dictionary to list and return 
        return packages.Values.ToList();
    }

}
