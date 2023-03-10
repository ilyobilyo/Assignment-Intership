using System.ComponentModel.DataAnnotations;

namespace Assignment_Intership.Models.Employee.EmployeeViewModels
{
    public class CreateEmployeeViewModel
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public IFormFile PhotoFile { get; set; }

        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public decimal MonthlySalary { get; set; }
    }
}
