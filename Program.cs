using Microsoft.EntityFrameworkCore;
using Euroquest.Data;
using Euroquest.Models;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext (koppla EF core till MySQL)
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 33)));
});

// Add Swagger (för test)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger
app.UseSwagger();
app.UseSwaggerUI();

//test endpoint
app.MapGet("/", () => "EuroQuest API is running");

// Minimal API: Register
app.MapPost("/api/user/register", async (AppDbContext db, User user) =>
{
    //kontrollera om e-post redan finns 
    if (await db.Users.AnyAsync(u => u.Email == user.Email))
        return Results.BadRequest(new { message = "Email already exists" });

    db.Users.Add(user);
    await db.SaveChangesAsync();
    return Results.Ok(new { message = "User registered", id = user.Id });
});

//Minimal API: Login
app.MapPost("/api/user/login", async (AppDbContext db, User loginData) =>
{
    var user = await db.Users
        .SingleOrDefaultAsync(u => u.Email == loginData.Email && u.Password == loginData.Password);

    if (user == null)
        return Results.Unauthorized();

    return Results.Ok(new
    {
        id = user.Id,
        name = user.Name,
        email = user.Email,
        role = user.Role
    });
});

app.Run();
