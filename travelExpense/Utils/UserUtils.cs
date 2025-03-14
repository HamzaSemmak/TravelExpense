using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Generators;
using travelExpense.Models;

namespace travelExpense.Utils
{
    public class UserUtils
    {
        public static string UserToJson(User user)
        {
            return JsonConvert.SerializeObject(user);
        }

        public static User AuthUser(HttpContext httpContext)
        {
            var userJson = httpContext.Session.GetString("User");
            return string.IsNullOrEmpty(userJson) ? null : JsonConvert.DeserializeObject<User>(userJson);
        }

        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
