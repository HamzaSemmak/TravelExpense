namespace travelExpense.Services
{
    public class AuthService
    {
        private static readonly Dictionary<string, string> _users = new()
        {
            { "admin@example.com", "password123" },
            { "user@example.com", "userpass" }
        };

        public bool ValidateUser(string email, string password)
        {
            return _users.ContainsKey(email) && _users[email] == password;
        }
    }
}
