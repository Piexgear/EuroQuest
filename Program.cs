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
//users 
app.MapGet("/users", Users.Get);
<<<<<<< HEAD
app.MapGet("/hotels", Hotels.Get);
app.MapGet("/countries", Countries.Get);
app.MapGet("/countries/{id}", Countries.GetById);
app.MapGet("/cities", Cities.Get);
app.MapGet("/activity", Activity.Get);
app.MapGet("/users/{id}", Users.GetById);
app.MapDelete("/users/{id}", Users.Delete);
app.MapPost("/users", Users.Post);
app.MapGet("/cities/country/{countryId}", Cities.GetByCountryId);
app.MapGet("/bookings", Bookings.GetBookings);
=======
app.MapGet("/profile", Profile.Get);
app.MapPost("/login", Login.Post);
app.MapDelete("/login", Login.Delete);   //kan vara onödiga eventuellt ta bort 
app.MapDelete("/users{id}", Users.Delete);  // -|| -
app.MapGet("/users/{id}", Users.GetById);  // - || -
>>>>>>> main


//countries
app.MapGet("/countries", Countries.Get);
app.MapPost("/countries/{id}", Countries.PostById);

//cities
app.MapGet("/cities", Cities.Get);
app.MapGet("/cities/countries/{countryId}", Cities.GetByCountryId);

//hotels
app.MapGet("/hotels", Hotels.Get);
app.MapGet("/hotels/cities/{cityId}", Hotels.GetCityHotels);

//activities
app.MapGet("/activities", Activities.Get);
app.MapGet("/activity/cities/{cityId}", Activities.GetCityActivity);


app.UseSession();
app.MapDelete("/db", db_reset_to_default);

app.Run();

// async task är samma som void
async Task db_reset_to_default(Config config)
{
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
        description TEXT(65535) NOT NULL,
        beach_distance INTEGER NOT NULL COMMENT 'avstånd till strand',
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
        hotels_id INTEGER NOT NULL,
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
        description TEXT(65535) NOT NULL,
        PRIMARY KEY(id)
    );
    """;

    string packages_create = """
    CREATE TABLE IF NOT EXISTS packages (
        id INTEGER NOT NULL AUTO_INCREMENT,
        hotel_id INTEGER NOT NULL,
        PRIMARY KEY(id)
    );
    """;

    string packages_activities_create = """
    CREATE TABLE IF NOT EXISTS packages_activities (
        package_id INTEGER NOT NULL,
        activity_id INTEGER NOT NULL,
        PRIMARY KEY(package_id, activity_id)
    );
    """;

    string bookings_create = """
    CREATE TABLE IF NOT EXISTS bookings (
        id INTEGER NOT NULL AUTO_INCREMENT,
        package_id INTEGER NOT NULL,
        user_id INTEGER NOT NULL,
        check_in DATE NOT NULL,
        check_out DATE NOT NULL,
        guests INTEGER NOT NULL,
        PRIMARY KEY(id)
    );
    """;

    string rooms_bookings_create = """
    CREATE TABLE IF NOT EXISTS rooms_bookings (
        booking_id INTEGER NOT NULL,
        room_id INTEGER NOT NULL,
        PRIMARY KEY(booking_id, room_id)
    );
    """;

    string foreign_keys = """
    ALTER TABLE cities ADD FOREIGN KEY(country) REFERENCES countries(id) ON UPDATE NO ACTION ON DELETE NO ACTION;
    ALTER TABLE hotels ADD FOREIGN KEY(city) REFERENCES cities(id) ON UPDATE NO ACTION ON DELETE NO ACTION;
    ALTER TABLE rooms ADD FOREIGN KEY(hotels_id) REFERENCES hotels(id) ON UPDATE NO ACTION ON DELETE NO ACTION;
    ALTER TABLE activities ADD FOREIGN KEY(city) REFERENCES cities(id) ON UPDATE NO ACTION ON DELETE NO ACTION;
    ALTER TABLE packages ADD FOREIGN KEY(hotel_id) REFERENCES hotels(id) ON UPDATE NO ACTION ON DELETE NO ACTION;
    ALTER TABLE packages_activities ADD FOREIGN KEY(package_id) REFERENCES packages(id) ON UPDATE NO ACTION ON DELETE NO ACTION;
    ALTER TABLE packages_activities ADD FOREIGN KEY(activity_id) REFERENCES activities(id) ON UPDATE NO ACTION ON DELETE NO ACTION;
    ALTER TABLE bookings ADD FOREIGN KEY(package_id) REFERENCES packages(id) ON UPDATE NO ACTION ON DELETE NO ACTION;
    ALTER TABLE bookings ADD FOREIGN KEY(user_id) REFERENCES users(id) ON UPDATE NO ACTION ON DELETE NO ACTION;
    ALTER TABLE rooms_bookings ADD FOREIGN KEY(booking_id) REFERENCES bookings(id) ON UPDATE NO ACTION ON DELETE NO ACTION;
    ALTER TABLE rooms_bookings ADD FOREIGN KEY(room_id) REFERENCES rooms(id) ON UPDATE NO ACTION ON DELETE NO ACTION;
    """;





    // Create tables
    await MySqlHelper.ExecuteNonQueryAsync(config.db, users_create);
    await MySqlHelper.ExecuteNonQueryAsync(config.db, countries_create);
    await MySqlHelper.ExecuteNonQueryAsync(config.db, cities_create);
    await MySqlHelper.ExecuteNonQueryAsync(config.db, hotels_create);
    await MySqlHelper.ExecuteNonQueryAsync(config.db, rooms_create);
    await MySqlHelper.ExecuteNonQueryAsync(config.db, activities_create);
    await MySqlHelper.ExecuteNonQueryAsync(config.db, packages_create);
    await MySqlHelper.ExecuteNonQueryAsync(config.db, packages_activities_create);
    await MySqlHelper.ExecuteNonQueryAsync(config.db, bookings_create);
    await MySqlHelper.ExecuteNonQueryAsync(config.db, rooms_bookings_create);
    await MySqlHelper.ExecuteNonQueryAsync(config.db, foreign_keys);

    await MySqlHelper.ExecuteNonQueryAsync(config.db,
        "DELETE FROM users WHERE email='david@email.com';");

    // Insert test user
    await MySqlHelper.ExecuteNonQueryAsync(config.db,
        "INSERT INTO users(email, password) VALUES ('david@email.com','password123');");
}
