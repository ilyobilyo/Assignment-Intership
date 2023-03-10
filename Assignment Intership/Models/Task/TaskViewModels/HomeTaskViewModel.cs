namespace Assignment_Intership.Models.Task.TaskViewModels
{
    public class HomeTaskViewModel
    {
        public IEnumerable<TaskViewModel> Tasks { get; set; }

        public int Page { get; set; }

        public int TotalPages { get; set; }
    }
}
