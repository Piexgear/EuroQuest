
namespace server;

using MySql.Data.MySqlClient;


class Users
{
    static List<User> users = new();

    public record Get_Data(int Id, string Name, string Email, string Password);
    public static async Task<List<Get_Data>> Get(Config config, HttpContext ctx)
    {
        if (ctx.Session.GetString("role") == Role.admin.ToString())
        {
            List<Get_Data> result = new();
            string query = "SELECT id, name, email, password FROM users";
            using (var reader = await MySqlHelper.ExecuteReaderAsync(config.db, query))
            {
                while (reader.Read())
                {
                    result.Add(new(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.GetString(3)));
                }
            }
            return result;
        }
        else
        {
            return null;
        }
    }


    public record GetById_Data(string Email);
    public static async Task<GetById_Data?> GetById(int id, Config config, HttpContext ctx)
    {
        if (ctx.Session.GetString("role") == Role.admin.ToString())
        {
            GetById_Data? result = null;
            string query = "SELECT email FROM users WHERE id = @id";
            var parameters = new MySqlParameter[] { new("@id", id) };

            using (var reader = await MySqlHelper.ExecuteReaderAsync(config.db, query, parameters))
            {
                if (reader.Read())
                {
                    result = new(reader.GetString(0));
                }
            }

            return result;
        }
        else
        {
            return null;
        }
    }


    // Create new user in table users, only users with the role admin can create new users with the role admin.
    // People without admin creating new users default to customer
    public record Post_Args(string Name, string Email, string Password, string Role);
    public static async Task Post(Post_Args user, Config config, HttpContext ctx)
    {
        if (ctx.Session.GetString("role") == Role.admin.ToString())
        {
            string querry = "INSERT INTO users(name, email, password, role) VALUES(@name, @email, @password, @role)";

            //indexerar själv för inmatning av data 
            var parameters = new MySqlParameter[]
            {
                new("@email", user.Email),
                new("@name", user.Name),
                new("@password", user.Password),
                new("@role", user.Role)

            };

            await MySqlHelper.ExecuteNonQueryAsync(config.db, querry, parameters);
        }
        else
        {
            string querry = "INSERT INTO users(name, email, password, role) VALUES(@name, @email, @password, @role)";

            //indexerar själv för inmatning av data 
            var parameters = new MySqlParameter[]
            {
                new("@email", user.Email),
                new("@name", user.Name),
                new("@password", user.Password),
                new("@role", "customer")

            };

            await MySqlHelper.ExecuteNonQueryAsync(config.db, querry, parameters);
        }
    
    }

    // Delete User from database table users, only available to be used by users with the admin role
    public static async Task Delete(int id, Config config, HttpContext ctx)
    {
        if (ctx.Session.GetString("role") == Role.admin.ToString())
        {
        string query = "DELETE FROM users WHERE id = @id";
        var parameters = new MySqlParameter[] { new("@id", id) };
        await MySqlHelper.ExecuteNonQueryAsync(config.db, query, parameters);
        }
    }

}

record User(string Name, string Email, string Password);

