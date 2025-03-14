using System;
using System.ComponentModel.DataAnnotations;

namespace travelExpense.Models
{
    public class Expense
    {
        public int Id { get; set; }

        public int TravelId { get; set; }
        public Travel Travel { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        [StringLength(100, ErrorMessage = "Category name can't be longer than 100 characters.")]
        public string Category { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        public decimal Amount { get; set; }

        [StringLength(500, ErrorMessage = "Description can't be longer than 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid Date format.")]
        public DateTime Date { get; set; }

        public DateTime CreatedAt { get; set; }

        public Expense()
        {
            CreatedAt = DateTime.UtcNow;
        }

        public override string ToString()
        {
            return $"Expense: Id={Id}, TravelId={TravelId}, Category={Category}, Amount={Amount:C}, Date={Date:yyyy-MM-dd}, Description={Description ?? "N/A"}, CreatedAt={CreatedAt:yyyy-MM-dd HH:mm:ss}";
        }

    }
}
