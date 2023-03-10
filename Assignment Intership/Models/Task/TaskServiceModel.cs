using Assignment_Intership.Models.Employee;

namespace Assignment_Intership.Models.Task
{
    public class TaskServiceModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime DueDate { get; set; }

        public Guid? EmployeeId { get; set; }

        public EmployeeServiceModel? Employee { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime CompletedAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
