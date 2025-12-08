

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

app.MapGet("/profile", Profile.Get);
app.MapPost("/login", Login.Post);
app.MapDelete("/login", Login.Delete);

app.MapGet("/users", Users.Get);
app.MapGet("/hotels", Hotels.Get);
app.MapGet("/country", Countrys.Get);

app.MapGet("/city", City.Get);
app.MapGet("/activity", Activity.Get);
app.MapGet("/users{id}", Users.GetById);

app.MapDelete("/users{id}", Users.Delete);
app.MapPost("/users", Users.Post);

app.MapDelete("/db", db_reset_to_default);

app.Run();

// async task är samma som void
async Task db_reset_to_default(Config config)
{

    string users_create = """
CREATE TABLE users
(
    id INT PRIMARY KEY AUTO_INCREMENT,
    name (VARCHAR255),
    email VARCHAR(256) UNIQUE NOT NULL,
    password VARCHAR(255) NOT NULL,
    role ENUM('admin', 'customer') NOT NULL DEFAULT 'customer'
)
""";

    string countries_create = """
    CREATE TABLE countries
    (
        id INTEGER NOT NULL AUTO_INCREMENT,
	    country_name VARCHAR(255) NOT NULL
    )
""";

    string cities_create = """
    CREATE TABLE cities
    (
    )
    """;

    string hotels_create = """
    CREATE TABLE hotels
    (
    )
    """;

    string rooms_create = """
    CREATE TABLE rooms
    (
    )
    """;

    string activities_create = """
    CREATE TABLE activities
    (
    )
    """;

    string packages_create = """
    CREATE TABLE packages
    (
    )
    """;

    string packages_activities_create = """
    CREATE TABLE packages_activities
    (
    )
    """;

    string bookings_create = """
    CREATE TABLE bookings
    (
    )
    """;

    string rooms_bookings_create = """
    CREATE TABLE rooms_bookings
    (
    )
    """;

    await MySqlHelper.ExecuteNonQueryAsync(config.db, "DROP TABLE IF EXISTS users");
    await MySqlHelper.ExecuteNonQueryAsync(config.db, users_create);

    await MySqlHelper.ExecuteNonQueryAsync(config.db, "INSERT INTO users(email, password) VALUES ('david@email.com','password123')");
}


