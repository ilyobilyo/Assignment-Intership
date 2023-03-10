using System.ComponentModel.DataAnnotations;

namespace Assignment_Intership.Data.Models
{
    public class Task
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        public Guid? EmployeeId { get; set; }

        public Employee? Employee { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime CompletedAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
