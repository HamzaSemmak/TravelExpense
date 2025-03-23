using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using travelExpense.Data;
using travelExpense.Models;
using travelExpense.Models.ViewModel;

namespace travelExpense.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;

        public ExpenseController(ApplicationDbContext applicationDbContext) 
        { 
            this.applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public async Task<ActionResult> Create(int id)
        {
            var viewModel = new ExpenseViewModel
            {
                TravelId = id
            };
            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Create(ExpenseViewModel expense)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(expense);
                }
                var travel = await applicationDbContext.Travels.FindAsync(expense.TravelId);
                if (travel == null)
                {
                    ModelState.AddModelError("", "An unexpected error occurred in select the travel.");
                    return View(expense);
                }

                var newExpense = new Expense
                {
                    TravelId = expense.TravelId,
                    Category = expense.Category,
                    Amount = expense.Amount,
                    Description = expense.Description,
                    Travel = travel
                };
                await applicationDbContext.Expenses.AddAsync(newExpense);
                await applicationDbContext.SaveChangesAsync();
                Console.WriteLine($"Expense created successfully: {expense.ToString()}");
                return RedirectToAction("Details", "Travel", new { id = expense.TravelId });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while creating expense: {ex.Message}");
                ModelState.AddModelError("", "An unexpected error occurred. Please try again later.");
                return View(expense);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id, int travel)
        {
            var expense = applicationDbContext.Expenses.FirstOrDefault(t => t.Id == id);
            if (expense == null)
            {
                return RedirectToAction("Details", "Travel", new { id = travel });
            }
            return RedirectToAction("Details", "Travel", new { id = travel });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id, int travel)
        {
            var expense = await applicationDbContext.Expenses.FindAsync(id);
            if (expense == null)
            {
                return RedirectToAction("Details", "Travel", new { id = travel });
            }
            applicationDbContext.Expenses.Remove(expense);
            await applicationDbContext.SaveChangesAsync();
            return RedirectToAction("Details", "Travel", new { id = travel });
        }

    }
}
