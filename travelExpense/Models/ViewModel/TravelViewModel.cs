using System.ComponentModel.DataAnnotations;
using travelExpense.Utils;

namespace travelExpense.Models.ViewModel
{
    public class TravelViewModel
    {
        [Required(ErrorMessage = "Travel name is required.")]
        [MaxLength(200, ErrorMessage = "Travel name can't be more than 200 characters.")]
        public string TravelName { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public int CategoryId { get; set; }

        public decimal Budget { get; set; }

        [Required(ErrorMessage = "Start date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid start date format.")]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "End date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid end date format.")]
        public DateTime? EndDate { get; set; }

        [MaxLength(500, ErrorMessage = "Description can't be more than 500 characters.")]
        public string Description { get; set; }

        public TravelViewModel() 
        {
            this.Budget = 0;
        }

        public override string ToString()
        {
            return $"{TravelName} | " +
                   $"Budget: {Budget:C} | " +
                   $"From: {StartDate:yyyy-MM-dd} To: {EndDate:yyyy-MM-dd} | " +
                   $"Description: {Description} |" +
                   $"CategoryId: {CategoryId}";
        }
    }
}
