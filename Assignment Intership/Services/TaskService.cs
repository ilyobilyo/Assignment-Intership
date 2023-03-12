using Assignment_Intership.Constants;
using Assignment_Intership.Contracts;
using Assignment_Intership.Data.Repositories;
using Assignment_Intership.Models.Task;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Assignment_Intership.Services
{
    public class TaskService : ITaskService
    {
        private readonly IApplicationRepository repo;
        private readonly IMapper mapper;
        private readonly IEmlpoyeeService employeeService;

        public TaskService(IApplicationRepository repo,
            IMapper mapper,
            IEmlpoyeeService employeeService)
        {
            this.repo = repo;
            this.mapper = mapper;
            this.employeeService = employeeService;
        }

        public async Task<TaskServiceModel> ChangeTaskStatus(Guid id)
        {
            var task = await GetById(id);

            if (task == null)
            {
                throw new ArgumentException("Task not found");
            }

            if (task.Status == Constants.TaskStatus.Start)
            {
                task.Status = Constants.TaskStatus.InProgress;
                task.UpdatedAt = DateTime.Now;
            }
            else if (task.Status == Constants.TaskStatus.InProgress)
            {
                var employee = await employeeService.GetById(task.EmployeeId);

                if (employee == null)
                {
                    throw new ArgumentException("This task dont have an employee");
                }

                task.Status = Constants.TaskStatus.Completed;
                task.IsCompleted = true;
                task.UpdatedAt = DateTime.Now;
                task.CompletedAt = DateTime.Now;
                employee.CompletedTasks++;
            }

            await repo.SaveChangesAsync();

            return mapper.Map<TaskServiceModel>(task);
        }

        public async Task<TaskServiceModel> Create(TaskServiceModel model)
        {
            if (model.Title == null)
            {
                throw new ArgumentException("Title is required");
            }

            if (model.Description == null)
            {
                throw new ArgumentException("Description is required");
            }

            if (model.DueDate < DateTime.Now)
            {
                throw new ArgumentException("The date cannot be for past days");

            }

            var task = new Assignment_Intership.Data.Models.Task()
            {
                Title = model.Title,
                Description = model.Description,
                DueDate = model.DueDate,
                Status = Constants.TaskStatus.Start,
                EmployeeId = model.EmployeeId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            await repo.AddAsync(task);
            await repo.SaveChangesAsync();

            return mapper.Map<TaskServiceModel>(task);
        }

        public async Task<bool> Delete(Guid id)
        {
            await repo.DeleteAsync<Data.Models.Task>(id);
            await repo.SaveChangesAsync();

            return true;
        }

        public async Task<TaskServiceModel> Edit(TaskServiceModel model)
        {
            if (model.Title == null)
            {
                throw new ArgumentException("Title is required");
            }

            if (model.Description == null)
            {
                throw new ArgumentException("Description is required");
            }

            if (model.DueDate < DateTime.Now)
            {
                throw new ArgumentException("The date cannot be for past days");
            }

            var task = await GetById(model.Id);

            UpdateTask(task, model);

            await repo.SaveChangesAsync();

            return mapper.Map<TaskServiceModel>(task);
        }


        public async Task<IEnumerable<TaskServiceModel>> GetAll(int pageNumber)
        {
            int pageSize = PagenationConstants.PageSize;

            var tasks = await repo.All<Assignment_Intership.Data.Models.Task>()
                .Include(x => x.Employee)
                .Where(x => x.CompletedAt <= x.DueDate)
                .OrderByDescending(x => x.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return mapper.Map<IEnumerable<TaskServiceModel>>(tasks);
        }

        public async Task<IEnumerable<TaskServiceModel>> GetExpiredTasks(int pageNumber)
        {
            int pageSize = PagenationConstants.PageSize;

            var tasks = await repo.All<Assignment_Intership.Data.Models.Task>()
                .Include(x => x.Employee)
                .Where(x => x.DueDate < DateTime.Now && x.Status != Constants.TaskStatus.Completed)
                .OrderByDescending(x => x.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return mapper.Map<IEnumerable<TaskServiceModel>>(tasks);
        }

        public async Task<int> GetTotalPages()
        {
            var totalTasksCount = await repo.All<Data.Models.Task>()
                .Where(x => x.CompletedAt <= x.DueDate)
                .CountAsync();

            if (totalTasksCount % PagenationConstants.PageSize != 0)
            {
                return (totalTasksCount / PagenationConstants.PageSize) + 1;
            }

            return totalTasksCount / PagenationConstants.PageSize;
        }

        public async Task<int> GetExpiredTasksTotalPages()
        {
            var totalTasksCount = await repo.All<Data.Models.Task>()
                .Where(x => x.DueDate < DateTime.Now && x.Status != Constants.TaskStatus.Completed)
                .CountAsync();

            if (totalTasksCount % PagenationConstants.PageSize != 0)
            {
                return (totalTasksCount / PagenationConstants.PageSize) + 1;
            }

            return totalTasksCount / PagenationConstants.PageSize;
        }

        public async Task<Data.Models.Task> GetById(Guid id)
        {
            return await repo.All<Assignment_Intership.Data.Models.Task>().FirstOrDefaultAsync(x => x.Id == id);
        }

        private void UpdateTask(Data.Models.Task task, TaskServiceModel model)
        {
            task.Title = model.Title;
            task.Description = model.Description;
            task.DueDate = model.DueDate;
            task.EmployeeId = model.EmployeeId;
            task.UpdatedAt = DateTime.Now;
        }

        
    }
}
