using System.ComponentModel.DataAnnotations;

namespace Assignment_Intership.Data.Models
{
    public class Employee
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public byte[] Photo { get; set; }

        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public decimal MonthlySalary { get; set; }

        public int CompletedTasks { get; set; }

        public ICollection<Task> Tasks { get; set; } = new HashSet<Task>();

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
