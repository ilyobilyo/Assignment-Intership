using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Assignment_Intership.Models.Task.TaskViewModels
{
    public class TaskEditViewModel
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

		[Required]
        public DateTime DueDate { get; set; }

        public Guid? EmployeeId { get; set; }

        public List<SelectListItem> Employees { get; set; }
    }
}
