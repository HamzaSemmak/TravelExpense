using Microsoft.AspNetCore.Mvc;
using travelExpense.Services;

namespace travelExpense.Controllers
{
    public class AuthController : Controller
    {

        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserEmail") != null)
                return RedirectToAction("Index", "Travel");

            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            if (_authService.ValidateUser(email, password))
            {
                HttpContext.Session.SetString("UserEmail", email);
                return RedirectToAction("Index", "Travel");
            }

            ViewBag.ErrorMessage = "Invalid email or password.";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Auth");
        }
    }
}
