using Assignment_Intership.Models.Task;

namespace Assignment_Intership.Models.Employee
{
    public class EmployeeServiceModel
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public IFormFile PhotoFile { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int CompletedTasks { get; set; }

        public int CompletedTasksForLastMonth { get; set; }

        public decimal MonthlySalary { get; set; }

        public ICollection<TaskServiceModel> Tasks { get; set; } = new HashSet<TaskServiceModel>();

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
