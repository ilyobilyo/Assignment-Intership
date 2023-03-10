using Assignment_Intership.Models.Task.TaskViewModels;

namespace Assignment_Intership.Models.Employee.EmployeeViewModels
{
    public class EmployeeDetailsModel
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

		public string PhoneNumber { get; set; }

        public string DateOfBirth { get; set; }

        public decimal MonthlySalary { get; set; }

        public int CompletedTasks { get; set; }

        public ICollection<EmployeeTasksModel> Tasks { get; set; } = new HashSet<EmployeeTasksModel>();

        public string CreatedAt { get; set; }

        public string UpdatedAt { get; set; }
    }
}
