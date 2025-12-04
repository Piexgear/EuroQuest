using Microsoft.EntityFrameworkCore;
using Euroquest.Data;
using Euroquest.Models;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext (koppla EF core till MySQL)
builder.Services.AddDbContext<AppDbContext>(options =>
{
    //hämtar connection string från appsetting.json
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    //kopplar EF core till MySql & anger serverversion
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 33)));

    /*Varför?
    - AddDbContext registrerar databasen i DI-container  (Dependency Injection)
    - UseMySql talar om att MySQL ska användas som databas
    - Version behövs för att EF Core ska kunna generera    korrekt SQL*/
});

var app = builder.Build();

//test endpoint, enkelt test för att se att API:et svarar
app.MapGet("/", () => "EuroQuest API is running");

// Minimal API: Register (POST/api/user/register)
//tar emot user-modell och sparar den i databasen 
app.MapPost("/api/user/register", async (AppDbContext db, User user) =>
{
    //kontrollera om e-post redan finns 
    if (await db.Users.AnyAsync(u => u.Email == user.Email))
        return Results.BadRequest(new { message = "Email already exists" });

    //Lägg till användaren
    db.Users.Add(user);

    //spara ändringarna i databasen 
    await db.SaveChangesAsync();

    //returnera ett svar (200 OK)
    return Results.Ok(new { message = "User registered", id = user.Id });
});

//Minimal API: Login (POST(api/user/login))
//kontrollerar email + lösenord
app.MapPost("/api/user/login", async (AppDbContext db, User loginData) =>
{
    //försöker hämta användare som matchar email + lösenord
    var user = await db.Users
        .SingleOrDefaultAsync(u => u.Email == loginData.Email && u.Password == loginData.Password);

    //om ingen matchning = Unauthorized
    if (user == null)
        return Results.Unauthorized();

    //Returnera användardata (ej lösenord)
    return Results.Ok(new
    {
        id = user.Id,
        name = user.Name,
        email = user.Email,
        role = user.Role
    });
});

app.Run(); //startar API-appen
