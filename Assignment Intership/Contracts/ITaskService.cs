using Assignment_Intership.Models.Task;

namespace Assignment_Intership.Contracts
{
    public interface ITaskService
    {
        Task<TaskServiceModel> Create(TaskServiceModel model);

        Task<IEnumerable<TaskServiceModel>> GetAll(int pageNumber);

        Task<Assignment_Intership.Data.Models.Task> GetById(Guid id);

        Task<TaskServiceModel> Edit(TaskServiceModel model);

        Task<bool> Delete(Guid id);

        Task<TaskServiceModel> CompleteTask(Guid id);

        Task<int> GetTotalPages();
    }
}
