namespace server;

using MySql.Data.MySqlClient;

static class Login
{
    public static void Delete(HttpContext ctx)
    {
        if (ctx.Session.IsAvailable)
        {
            ctx.Session.Clear();
        }
    }

    public record Post_Args(string Email, string Password);
    public static async Task<bool>
    Post(Post_Args credentials, Config config, HttpContext ctx)
    {
        bool result = false;
        string queryID = "SELECT id FROM users WHERE email = @email AND password = @password";
        string queryRole = "SELECT role FROM users WHERE email = @email AND password = @password";

        var parameters = new MySqlParameter[]
        {
            new("@email", credentials.Email),
            new("@password", credentials.Password),
        };

        object query_resultID = await MySqlHelper.ExecuteScalarAsync(config.db, queryID, parameters);
        object query_resultRole = await MySqlHelper.ExecuteScalarAsync(config.db, queryRole, parameters);
        if (query_resultID is int id)
        {
            if (ctx.Session.IsAvailable)
            {
                ctx.Session.SetInt32("user_id", id);
                result = true;
            }
        }
        if (query_resultRole is string role)
        {
            if (ctx.Session.IsAvailable)
            {
            ctx.Session.SetString("role", role);
            }
        }
        
        return result;
    }
}
