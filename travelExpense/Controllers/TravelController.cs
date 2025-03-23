using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using travelExpense.Data;
using travelExpense.Models;
using travelExpense.Models.ViewModel;

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
            var categories = await applicationDbContext.Categories.ToListAsync();
            ViewBag.Categories = categories;
            return View(new TravelViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(TravelViewModel travel)
        {
            if (travel.StartDate.HasValue && travel.EndDate.HasValue)
            {
                if (travel.EndDate <= travel.StartDate)
                {
                    ModelState.AddModelError("EndDate", "End Date must be after Start Date.");
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var travelEntity = new Travel
                    {
                        TravelName = travel.TravelName,
                        CategoryId = travel.CategoryId,
                        Budget = travel.Budget,
                        StartDate = travel.StartDate,
                        EndDate = travel.EndDate,
                        Description = travel.Description
                    };
                    await applicationDbContext.Travels.AddAsync(travelEntity);
                    await applicationDbContext.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error occurred while saving travel: {ex.Message}");
                    ModelState.AddModelError(string.Empty, "An error occurred while saving the travel details. Please try again later.");
                    var categories = await applicationDbContext.Categories.ToListAsync();
                    ViewBag.Categories = categories;
                    return View(travel);
                }
            }
            var categoriesInvalid = await applicationDbContext.Categories.ToListAsync();
            ViewBag.Categories = categoriesInvalid;
            return View(travel);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var travel = applicationDbContext.Travels.FirstOrDefault(t => t.Id == id);
            if (travel == null)
            {
                return RedirectToAction("Index", "Travel");
            }
            return View(travel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var travel = await applicationDbContext.Travels.FindAsync(id);
            if (travel == null)
            {
                return RedirectToAction("Index", "Travel");
            }

            applicationDbContext.Travels.Remove(travel);
            await applicationDbContext.SaveChangesAsync();

            return RedirectToAction("Index", "Travel");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var travel = await applicationDbContext.Travels
                .Include(t => t.Expenses)
                .Include(t => t.Clients)
                .Include(t => t.Category)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (travel == null)
            {
                return RedirectToAction("Index", "Travel");
            }
            decimal total = 0;
            foreach (var item in travel.Expenses)
            {
                total += item.Amount;
            }
            travel.Budget = total;
            return View(travel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var travel = await applicationDbContext.Travels.FirstOrDefaultAsync(t => t.Id == id);
            if (travel == null)
            {
                return RedirectToAction("Index", "Travel");
            }
            TravelViewModel viewModel = new TravelViewModel()
            {
                CategoryId = travel.CategoryId,
                Budget = travel.Budget,
                Description = travel.Description,
                EndDate = travel.EndDate,
                StartDate = travel.StartDate,
                TravelName = travel.TravelName
            };
            var categories = await applicationDbContext.Categories.ToListAsync();
            ViewBag.Categories = categories;
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TravelViewModel viewModel, int id)
        {
            var travel = await applicationDbContext.Travels.FirstOrDefaultAsync(t => t.Id == id);
            if (travel == null)
            {
                return RedirectToAction("Index", "Travel");
            }
            if (viewModel.StartDate.HasValue && viewModel.EndDate.HasValue)
            {
                if (viewModel.EndDate <= viewModel.StartDate)
                {
                    ModelState.AddModelError("EndDate", "End Date must be after Start Date.");
                }
            }
            if (ModelState.IsValid)
            {
                try
                {
                    travel.TravelName = viewModel.TravelName;
                    travel.CategoryId = viewModel.CategoryId;
                    travel.Budget = viewModel.Budget;
                    travel.StartDate = viewModel.StartDate;
                    travel.EndDate = viewModel.EndDate;
                    travel.Description = viewModel.Description;
                    await applicationDbContext.SaveChangesAsync();
                    return RedirectToAction("Index", "Travel");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error occurred while saving travel: {ex.Message}");
                    ModelState.AddModelError(string.Empty, "An error occurred while saving the travel details. Please try again later.");
                    var categories = await applicationDbContext.Categories.ToListAsync();
                    ViewBag.Categories = categories;
                    return View(travel);
                }
            }
            var categoriesInvalid = await applicationDbContext.Categories.ToListAsync();
            ViewBag.Categories = categoriesInvalid;
            return View(travel);
        }
    }
}
