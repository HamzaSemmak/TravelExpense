using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using travelExpense.Data;

namespace travelExpense.Models
{
    public class Client
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(200, ErrorMessage = "Name can't be longer than 200 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [StringLength(200, ErrorMessage = "Email can't be longer than 200 characters.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone is required.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        [StringLength(15, ErrorMessage = "Phone can't be longer than 15 characters.")]
        public string Phone { get; set; }

        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public int TravelId { get; set; }
        public List<Travel> Travels { get; set; }

        public Client()
        {
            CreatedAt = DateTime.UtcNow; 
        }

        public bool IsUnique(string name, string email, string phone, ApplicationDbContext context)
        {
            return !context.Clients.Any(c => c.Name == name || c.Email == email || c.Phone == phone);
        }

        public override string ToString()
        {
            return $"Client: Id={Id}, Name={Name}, Email={Email}, Phone={Phone}, Address={Address}, CreatedAt={CreatedAt:yyyy-MM-dd HH:mm:ss}";
        }
    }
}
