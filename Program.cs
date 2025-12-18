using server;
using MySql.Data.MySqlClient;

Config config = new("server=127.0.0.1;uid=euroquest;pwd=euroquest;database=euroquest");

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton(config);
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();
app.UseSession();
//users 
app.MapGet("/users", Users.Get);
app.MapGet("/users/{id}", Users.GetById);
app.MapDelete("/users/{id}", Users.Delete);
app.MapPost("/users", Users.Post);
app.MapPost("/login", Login.Post);

// Ej katogeriserat 
app.MapDelete("/login", Login.Delete);   //kan vara onödiga eventuellt ta bort 

app.MapGet("/profile", Profile.Get);

//Bookings
app.MapGet("/bookings", Bookings.GetBookings);
app.MapGet("/bookings/user", Bookings.GetByUser_data);
// app.MapPut("/bookings/guests", Bookings.SetGuests);
// app.MapPost("/bookings/{bookingId}", Bookings.Update);
// app.MapDelete("/bookings/cancel", Bookings.Cancel);

//countries
app.MapGet("/countries", Countries.Get);

//cities
app.MapGet("/cities", Cities.Get);
app.MapGet("/cities/countries/{countryId}", Cities.GetByCountryId);

//hotels
app.MapGet("/hotels", Hotels.Get);
app.MapGet("/hotels/cities/{cityId}", Hotels.GetCityHotels);

//activities
app.MapGet("/activities", Activities.Get);
app.MapGet("/activities/cities/{cityId}", Activities.GetCityActivity);

//packages
app.MapPost("/packages", async (Packages.Post_Data data, Config config, HttpContext ctx)
    => await Packages.Post(data, config, ctx));
app.MapGet("/packages", Packages.GetAll);



app.MapDelete("/db", db_reset_to_default);

app.Run();

// async task är samma som void
async Task db_reset_to_default(Config config)
{
    // Table creation SQL
    string users_create = """
    CREATE TABLE IF NOT EXISTS users (
        id INTEGER NOT NULL AUTO_INCREMENT,
        name VARCHAR(255),
        email VARCHAR(255) NOT NULL UNIQUE,
        password VARCHAR(255) NOT NULL,
        role ENUM('admin', 'customer') NOT NULL DEFAULT 'customer',
        PRIMARY KEY(id)
    );
    """;

    string countries_create = """
    CREATE TABLE IF NOT EXISTS countries (
        id INTEGER NOT NULL AUTO_INCREMENT,
        country_name VARCHAR(255) NOT NULL,
        PRIMARY KEY(id)
    );
    """;

    string cities_create = """
    CREATE TABLE IF NOT EXISTS cities (
        id INTEGER NOT NULL AUTO_INCREMENT,
        city_name VARCHAR(255) NOT NULL,
        country INTEGER NOT NULL,
        PRIMARY KEY(id)
    );
    """;

    string hotels_create = """
    CREATE TABLE IF NOT EXISTS hotels (
        id INTEGER NOT NULL AUTO_INCREMENT,
        name VARCHAR(255) NOT NULL,
        city INTEGER NOT NULL,
        amount_of_rooms INTEGER NOT NULL,
        description TEXT NOT NULL,
        beach_distance INTEGER NOT NULL,
        pool TINYINT NOT NULL,
        breakfast TINYINT NOT NULL,
        center_distance INTEGER NOT NULL,
        PRIMARY KEY(id)
    );
    """;

    string rooms_create = """
    CREATE TABLE IF NOT EXISTS rooms (
        id INTEGER NOT NULL AUTO_INCREMENT,
        number INTEGER NOT NULL,
        hotel INTEGER NOT NULL,   -- fixed column name
        capacity INTEGER NOT NULL,
        price INTEGER NOT NULL,
        PRIMARY KEY(id)
    );
    """;

    string activities_create = """
    CREATE TABLE IF NOT EXISTS activities (
        id INTEGER NOT NULL AUTO_INCREMENT,
        name VARCHAR(255) NOT NULL,
        duration INTEGER NOT NULL,
        price INTEGER NOT NULL,
        address VARCHAR(255) NOT NULL,
        city INTEGER NOT NULL,
        capacity INTEGER NOT NULL,
        description TEXT NOT NULL,
        PRIMARY KEY(id)
    );
    """;

    string packages_create = """
    CREATE TABLE IF NOT EXISTS packages (
        id INTEGER NOT NULL AUTO_INCREMENT,
        hotel INTEGER NOT NULL,   -- fixed column name
        PRIMARY KEY(id)
    );
    """;

    string package_activities_create = """
    CREATE TABLE IF NOT EXISTS package_activities (
        package INTEGER NOT NULL,
        activity INTEGER NOT NULL,
        PRIMARY KEY(package, activity)
    );
    """;

    string bookings_create = """
    CREATE TABLE IF NOT EXISTS bookings (
        id INTEGER NOT NULL AUTO_INCREMENT,
        package INTEGER NOT NULL,
        user INTEGER NOT NULL,
        check_in DATE NOT NULL,
        check_out DATE NOT NULL,
        guests INTEGER NOT NULL,
        PRIMARY KEY(id)
    );
    """;

    string room_bookings_create = """
    CREATE TABLE IF NOT EXISTS room_bookings (
        booking INTEGER NOT NULL,
        room INTEGER NOT NULL,
        PRIMARY KEY(booking, room)
    );
    """;

    // Foreign key constraints
    string foreign_keys = """
    ALTER TABLE cities ADD FOREIGN KEY(country) REFERENCES countries(id) ON UPDATE NO ACTION ON DELETE NO ACTION;
    ALTER TABLE hotels ADD FOREIGN KEY(city) REFERENCES cities(id) ON UPDATE NO ACTION ON DELETE NO ACTION;
    ALTER TABLE rooms ADD FOREIGN KEY(hotel) REFERENCES hotels(id) ON UPDATE NO ACTION ON DELETE NO ACTION;
    ALTER TABLE activities ADD FOREIGN KEY(city) REFERENCES cities(id) ON UPDATE NO ACTION ON DELETE NO ACTION;
    ALTER TABLE packages ADD FOREIGN KEY(hotel) REFERENCES hotels(id) ON UPDATE NO ACTION ON DELETE NO ACTION;
    ALTER TABLE package_activities ADD FOREIGN KEY(package) REFERENCES packages(id) ON UPDATE NO ACTION ON DELETE NO ACTION;
    ALTER TABLE package_activities ADD FOREIGN KEY(activity) REFERENCES activities(id) ON UPDATE NO ACTION ON DELETE NO ACTION;
    ALTER TABLE bookings ADD FOREIGN KEY(package) REFERENCES packages(id) ON UPDATE NO ACTION ON DELETE NO ACTION;
    ALTER TABLE bookings ADD FOREIGN KEY(user) REFERENCES users(id) ON UPDATE NO ACTION ON DELETE NO ACTION;
    ALTER TABLE room_bookings ADD FOREIGN KEY(booking) REFERENCES bookings(id) ON UPDATE NO ACTION ON DELETE NO ACTION;
    ALTER TABLE room_bookings ADD FOREIGN KEY(room) REFERENCES rooms(id) ON UPDATE NO ACTION ON DELETE NO ACTION;
    """;

    // Execute creation queries
    await MySqlHelper.ExecuteNonQueryAsync(config.db, users_create);
    await MySqlHelper.ExecuteNonQueryAsync(config.db, countries_create);
    await MySqlHelper.ExecuteNonQueryAsync(config.db, cities_create);
    await MySqlHelper.ExecuteNonQueryAsync(config.db, hotels_create);
    await MySqlHelper.ExecuteNonQueryAsync(config.db, rooms_create);
    await MySqlHelper.ExecuteNonQueryAsync(config.db, activities_create);
    await MySqlHelper.ExecuteNonQueryAsync(config.db, packages_create);
    await MySqlHelper.ExecuteNonQueryAsync(config.db, package_activities_create);
    await MySqlHelper.ExecuteNonQueryAsync(config.db, bookings_create);
    await MySqlHelper.ExecuteNonQueryAsync(config.db, room_bookings_create);
    await MySqlHelper.ExecuteNonQueryAsync(config.db, foreign_keys);
}
