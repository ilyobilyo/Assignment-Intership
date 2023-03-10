namespace Assignment_Intership.Models.Employee.EmployeeViewModels
{
    public class EmployeeViewModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
		public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string DateOfBirth { get; set; }
        public int CompletedTasksForLastMonth { get; set; }
        public decimal MonthlySalary { get; set; }
    }
}
