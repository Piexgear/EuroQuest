

using server;
using MySql.Data.MySqlClient;


var builder = WebApplication.CreateBuilder(args);

Config config = new("server=127.0.0.1;uid=euroquest;pwd=euroquest;database=euroquest");
builder.Services.AddSingleton<Config>(config);

var app = builder.Build();
//users 
app.MapGet("/users", Users.Get);
app.MapPost("/users", Users.Post);
app.MapGet("/users/{id}", Users.GetById);
app.MapDelete("/users/{id}", Users.Delete);

//countries
app.MapGet("/countries", Countries.Get);
app.MapGet("/countries/{id}", Countries.GetById);

//cities
app.MapGet("/cities", Cities.Get);
app.MapGet("/cities/countries/{countryId}", Cities.GetByCountryId);

//hotels
app.MapGet("/hotels", Hotels.Get);
app.MapGet("/hotels/cities/{cityId}", Hotels.GetCityHotels);

//activities
app.MapGet("/activity", Activity.Get);
app.MapGet("/activity/cities/{cityId}", Activity.GetCityActivity);


app.MapDelete("/db", db_reset_to_default);

app.Run();

// async task är samma som void
async Task db_reset_to_default(Config config)
{

    string users_create = """
CREATE TABLE users
(
    id INT PRIMARY KEY AUTO_INCREMENT,
    email VARCHAR(256) UNIQUE NOT NULL,
    password VARCHAR(64)
)
""";

    // string countires_create = """
    // CREATE TABLE countries
    // (

    // )
    // """;

    await MySqlHelper.ExecuteNonQueryAsync(config.db, "DROP TABLE IF EXISTS users");
    await MySqlHelper.ExecuteNonQueryAsync(config.db, users_create);

    await MySqlHelper.ExecuteNonQueryAsync(config.db, "INSERT INTO users(email, password) VALUES ('david@email.com','password123')");
}


