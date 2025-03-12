using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;
using System.ComponentModel.DataAnnotations;

namespace travelExpense.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [StringLength(200, ErrorMessage = "Email can't be longer than 200 characters.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 20 characters.")]
        public string Password { get; set; }
        public int RoleId { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        public Role Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public User() {
            CreatedAt = DateTime.UtcNow;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
