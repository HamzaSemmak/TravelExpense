using System.ComponentModel.DataAnnotations;
using travelExpense.Data;

namespace travelExpense.Models.ViewModel
{
    public class ClientViewModel
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

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }
        public int TravelId { get; set; }

        public ClientViewModel()
        {
        }

        public override string ToString()
        {
            return $"Client: Id={Id}, Name={Name}, Email={Email}, Phone={Phone}, Address={Address}";
        }
    }
}
