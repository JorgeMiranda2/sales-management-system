using System.ComponentModel.DataAnnotations;

namespace SalesSystemApp.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; set; } // Hashed password

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required]
        [MaxLength(15)]
        [Phone]
        public string Phone { get; set; }

        public ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }
}