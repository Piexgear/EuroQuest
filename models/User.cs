using System.ComponentModel.DataAnnotations;

//alla entiteter av typen models har detta namespace för att organisera 
namespace Euroquest.Models;

public class User
{
    //I ett Entity Framework (EF Core) API-projekt behövs get set för att EF ska kunna skriva och läsa från/till databasen 
    public int Id { get; set; }

    //använder maxlength för att säga att de är en varchar istället för default longtext
    [MaxLength(100)]
    public string Name { get; set; } = "";
    [MaxLength(255)]
    public string Email { get; set; } = ""; //="" för att inget ska vara nullable 
    [MaxLength(255)]
    public string Password { get; set; } = "";
    [MaxLength(10)]
    public string Role { get; set; } = "customer"; //customer är default men finns admin också 
}

