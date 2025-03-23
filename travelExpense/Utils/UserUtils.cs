using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Generators;
using travelExpense.Data;
using travelExpense.Models;

namespace travelExpense.Utils
{
    public class UserUtils
    {
        private static IHttpContextAccessor _httpContextAccessor;

        public static void Initialize(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public static string UserToJson(User user)
        {
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            return JsonConvert.SerializeObject(user, settings);
        }

        public static User User()
        {
            var httpContext = _httpContextAccessor?.HttpContext;
            if (httpContext == null) return null;

            var userJson = httpContext.Session.GetString("User");
            User user = string.IsNullOrEmpty(userJson) ? null : JsonConvert.DeserializeObject<User>(userJson);
            return user;
        }

        public static bool isAdmin()
        {
            User user = User();
            if(user == null) return false;
            string role = user.Role.RoleName.ToString();
            if(role == "Admin") return true;
            return false;
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
