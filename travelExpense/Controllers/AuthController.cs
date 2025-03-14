using Microsoft.AspNetCore.Mvc;
using travelExpense.Data;
using travelExpense.Utils;

namespace travelExpense.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AuthController(ApplicationDbContext context)
        {
            this._context = context;
        }

        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("User") != null)
                return Redirect("/");

            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            if (user == null)
            {
                ViewBag.ErrorMessage = "Email not found.";
                return View();
            }

            if (!UserUtils.VerifyPassword(password, user.Password))
            {
                ViewBag.ErrorMessage = "Incorrect password.";
                return View();
            }

            var userJson = UserUtils.UserToJson(user);
            HttpContext.Session.SetString("User", userJson);

            Console.WriteLine($"User logged in: {user}");
            return Redirect("/");
        }



        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Auth");
        }
    }
}
