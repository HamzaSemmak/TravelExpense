using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using travelExpense.Data;

namespace travelExpense.Controllers
{
    public class TravelController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;

        public TravelController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var travels = applicationDbContext.Travels.Include(t => t.Category).ToList();
            return View(travels);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categorys = await applicationDbContext.Categories.ToListAsync();
            return View(categorys);
        }
    }
}
