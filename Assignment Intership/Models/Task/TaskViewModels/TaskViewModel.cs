namespace Assignment_Intership.Models.Task.TaskViewModels
{
    public class TaskViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string DueDate { get; set; }

        public string? Status { get; set; }

        public string CreatedAt { get; set; }

		public bool IsCompleted { get; set; }

        public Guid? EmployeeId { get; set; }

        public string? Employee { get; set; }
    }
}
