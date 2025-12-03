using System.ComponentModel.DataAnnotations;

namespace Euroquest.Models;
//här skriver vi in userklass utan konstruktor 

public class User
{
    //I ett Entity Framework (EF Core) API-projekt behövs get set för att EF ska kunna skriva och läsa från/till databasen 
    public int Id { get; set; }

    [MaxLength(100)]
    public string Name { get; set; } = "";
    [MaxLength(255)]
    public string Email { get; set; } = ""; //="" för att inget ska vara nullable 
    [MaxLength(255)]
    public string Password { get; set; } = "";
    [MaxLength(10)]
    public string Role { get; set; } = "customer";
}

