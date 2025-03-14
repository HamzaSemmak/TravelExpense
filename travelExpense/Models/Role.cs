using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace travelExpense.Models
{
    public class Role
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Role name is required.")]
        [StringLength(100, ErrorMessage = "Role name can't be longer than 100 characters.")]
        public string RoleName { get; set; }

        public List<User> Users { get; set; }

        public DateTime CreatedAt { get; set; }

        public Role()
        {
            CreatedAt = DateTime.UtcNow;
        }

        public override string ToString()
        {
            return $"Role: Id={Id}, Name={RoleName ?? "N/A"}";
        }

    }
}
