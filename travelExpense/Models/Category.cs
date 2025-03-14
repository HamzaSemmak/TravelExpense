using System.ComponentModel.DataAnnotations;

namespace travelExpense.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(200, ErrorMessage = "Name can't be longer than 200 characters.")]
        public string Name { get; set; }


        [StringLength(500, ErrorMessage = "Description can't be longer than 500 characters.")]
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }

        public Category() { }

        public override string ToString()
        {
            return $"Category: Id={Id}, Name={Name}, Description={Description}, CreatedAt={CreatedAt:yyyy-MM-dd HH:mm:ss}";
        }
    }
}
