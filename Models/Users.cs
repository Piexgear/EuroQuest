
namespace server;

using MySql.Data.MySqlClient;

class Users
{
    static List<User> users = new();
    static List<Hotels> hotel = new();

    public record Get_Data(int Id, string name, string Email, string Password, int role);
    public static async Task<List<Get_Data>> Get(Config config)
    {
        List<Get_Data> result = new();
        string query = "SELECT id, name, email, password, role FROM users";
        using (var reader = await MySqlHelper.ExecuteReaderAsync(config.db, query))
        {
            while (reader.Read())
            {
                result.Add(new(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4)));
            }
        }
        return result;
    }


    public record GetById_Data(string Email);
    public static async Task<GetById_Data?> GetById(int id, Config config)
    {
        GetById_Data? result = null;
        string query = "SELECT email, FROM users WHERE id = @id";
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



    public record Post_Args(string Name, string Email, string Password, int Role);
    public static async Task Post(Post_Args user, Config config)
    {
        string querry = "INSERT INTO user(name, email, password role) VALUES(@name, @email, @password, @role)";

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

    public static async Task Delete(int id, Config config)
    {
        string query = "DELETE FROM user WHERE id = @id";
        var parameters = new MySqlParameter[] { new("@id", id) };
        await MySqlHelper.ExecuteNonQueryAsync(config.db, query, parameters);
    }

}

record User(string Name, string Email, string Password, int Role);

