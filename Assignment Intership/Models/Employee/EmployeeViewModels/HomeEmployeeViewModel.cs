namespace Assignment_Intership.Models.Employee.EmployeeViewModels
{
    public class HomeEmployeeViewModel
    {
        public IEnumerable<EmployeeViewModel> Employees { get; set; }

        public int Page { get; set; }

        public int TotalPages { get; set; }
    }
}
