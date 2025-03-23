using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;
using travelExpense.Data;
using travelExpense.Models;
using travelExpense.Models.ViewModel;

namespace clientExpense.Controllers
{
    public class ClientController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;

        public ClientController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var clients = await applicationDbContext.Clients.ToListAsync();
            return View(clients);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var client = applicationDbContext.Clients.FirstOrDefault(t => t.Id == id);
            if (client == null)
            {
                return RedirectToAction("Index", "Client");
            }
            return View(client);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await applicationDbContext.Clients.FindAsync(id);
            if (client == null)
            {
                return RedirectToAction("Index", "Client");
            }
            applicationDbContext.Clients.Remove(client);
            await applicationDbContext.SaveChangesAsync();
            return RedirectToAction("Index", "Client");
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var travels = await applicationDbContext.Travels
                .Include(t => t.Category).ToListAsync();
            ViewBag.travels = travels;
            return View(new ClientViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClientViewModel client)
        {
            Console.WriteLine($"Clients model : {client.ToString()}");
            var travels = await applicationDbContext.Travels.Include(t => t.Category).ToListAsync();
            if (ModelState.IsValid)
            {
                try
                {
                    var selectedTravel = await applicationDbContext.Travels.FindAsync(client.TravelId);
                    if (selectedTravel == null)
                    {
                        ModelState.AddModelError(string.Empty, "An error occurred while add the travel");
                        ViewBag.travels = travels;
                        return View(client);
                    }
                    else
                    {
                        var newClient = new Client()
                        {
                            Address = client.Address,
                            Name = client.Name,
                            Email = client.Email,
                            Phone = client.Phone,
                            TravelId = client.TravelId,
                            Travels = new List<Travel> { selectedTravel }
                        };
                        await applicationDbContext.Clients.AddAsync(newClient);
                        await applicationDbContext.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                }
                catch(DbException ex)
                {
                    Console.WriteLine($"Error occurred while saving travel: {ex.Message}");
                    ModelState.AddModelError(string.Empty, $"Exception {ex.Message.ToString()}");
                    ViewBag.travels = travels;
                    return View(client);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error occurred while saving travel: {ex.Message}");
                    ModelState.AddModelError(string.Empty, $"Exception {ex.Message.ToString()}");
                    ViewBag.travels = travels;
                    return View(client);
                }
            }
            ViewBag.travels = travels;
            return View(client);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var client = await applicationDbContext.Clients
            .Include(t => t.Travels)
                .ThenInclude(travel => travel.Expenses)
            .FirstOrDefaultAsync(t => t.Id == id);


            if (client == null)
            {
                return RedirectToAction("Index", "Client");
            }
            return View(client);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var travels = await applicationDbContext.Travels
                .Include(t => t.Category).ToListAsync();
            ViewBag.travels = travels;

            var client = await applicationDbContext.Clients.FirstOrDefaultAsync(t => t.Id == id);
            if (client == null)
            {
                return RedirectToAction("Index", "Client");
            }
            else
            {
                ClientViewModel viewModel = new ClientViewModel()
                {
                    Address = client.Address,
                    Email = client.Email,
                    Id = id,
                    Name = client.Name,
                    Phone = client.Phone,
                    TravelId = client.TravelId,
                };
                return View(viewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ClientViewModel client)
        {
            if (client == null)
            {
                ModelState.AddModelError("", "Client data is missing.");
                return View(client);
            }
            if (!ModelState.IsValid)
            {
                return View(client);
            }
            var travels = await applicationDbContext.Travels.ToListAsync();
            try
            {
                var existingClient = await applicationDbContext.Clients.FindAsync(client.Id);
                if (existingClient == null)
                {
                    ModelState.AddModelError("", "Client not found.");
                    ViewBag.travels = travels;
                    return View(client);
                }
                existingClient.Name = client.Name;
                existingClient.Email = client.Email;
                existingClient.Phone = client.Phone;
                existingClient.Address = client.Address;
                existingClient.TravelId = client.TravelId;
                await applicationDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error occurred while saving the client: {ex.Message}");
                ViewBag.travels = travels;
                return View(client);
            }
        }


    }
}
