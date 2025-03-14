using System.ComponentModel.DataAnnotations;
using travelExpense.Utils;

namespace travelExpense.Models
{
    public class Travel
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public List<Client> Clients { get; set; }
        public List<Expense> Expenses { get; set; }

        [Required(ErrorMessage = "Travel Name is required.")]
        [StringLength(200, ErrorMessage = "Travel Name can't be longer than 200 characters.")]
        public string TravelName { get; set; }

        [StringLength(500, ErrorMessage = "Description can't be longer than 500 characters.")]
        public string Description { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Budget must be greater than 0.")]
        public decimal Budget { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Invalid Start Date.")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Invalid End Date.")]
        public DateTime? EndDate { get; set; }

        [BetweenDateUtils(ErrorMessage = "EndDate must be later than StartDate.")]
        public bool IsValidDateRange()
        {
            if (StartDate.HasValue && EndDate.HasValue)
            {
                return EndDate > StartDate;
            }
            return true;
        }
        public DateTime CreatedAt { get; set; }

        public Travel()
        {
            CreatedAt = DateTime.UtcNow;
        }

        public override string ToString()
        {
            return $"Travel: Id={Id}, Name={TravelName}, CategoryId={CategoryId}, Budget={Budget:C}, " +
                   $"StartDate={(StartDate.HasValue ? StartDate.Value.ToString("yyyy-MM-dd") : "N/A")}, " +
                   $"EndDate={(EndDate.HasValue ? EndDate.Value.ToString("yyyy-MM-dd") : "N/A")}, " +
                   $"Clients={Clients?.Count ?? 0}, Expenses={Expenses?.Count ?? 0}, CreatedAt={CreatedAt:yyyy-MM-dd HH:mm:ss}";
        }

    }
}
