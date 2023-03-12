using Assignment_Intership.Contracts;
using Assignment_Intership.Models.Task;
using Assignment_Intership.Models.Task.TaskViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_Intership.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskService taskService;
        private readonly IEmlpoyeeService employeeService;
        private readonly IMapper mapper;

        public TaskController(IEmlpoyeeService employeeService,
            IMapper mapper,
            ITaskService taskService)
        {
            this.taskService = taskService;
            this.employeeService = employeeService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index([FromQuery] int pageNumber)
        {
            if (pageNumber == 0)
            {
                pageNumber = 1;
            }

            var homeViewModel = new HomeTaskViewModel();

            var tasksServiceModels = await taskService.GetAll(pageNumber);

            var taskList = mapper.Map<IEnumerable<TaskViewModel>>(tasksServiceModels);

            homeViewModel.Tasks = taskList;
            homeViewModel.Page = pageNumber;
            homeViewModel.TotalPages = await taskService.GetTotalPages();

            return View(homeViewModel);
        }


        [HttpGet]
        public async Task<IActionResult> ExpiredTasks([FromQuery] int pageNumber)
        {
            if (pageNumber == 0)
            {
                pageNumber = 1;
            }

            var homeViewModel = new HomeTaskViewModel();

            var tasksServiceModels = await taskService.GetExpiredTasks(pageNumber);

            var taskList = mapper.Map<IEnumerable<TaskViewModel>>(tasksServiceModels);

            homeViewModel.Tasks = taskList;
            homeViewModel.Page = pageNumber;
            homeViewModel.TotalPages = await taskService.GetExpiredTasksTotalPages();

            return View(homeViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var viewModel = new CreateTaskViewModel();
            viewModel.Employees = await employeeService.GetSelectListItems();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var error = string.Join(Environment.NewLine, ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));

                return RedirectToAction("Error", "Home", new { error });
            }

            var serviceModel = mapper.Map<TaskServiceModel>(model);

            try
            {
                var newTask = await taskService.Create(serviceModel);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Home", new { error = e.Message });
            }

        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] Guid id)
        {
            var taskModel = await taskService.GetById(id);

            if (taskModel == null)
            {
                return RedirectToAction("Error", "Home", new { error = "Task not found" });
            }

            var viewModel = mapper.Map<TaskEditViewModel>(taskModel);

            viewModel.Employees = await employeeService.GetSelectListItems();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TaskEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var error = string.Join(Environment.NewLine, ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));

                return RedirectToAction("Error", "Home", new { error });
            }

            var taskToEdit = mapper.Map<TaskServiceModel>(model);

            try
            {
                var taskServiceModel = await taskService.Edit(taskToEdit);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Home", new {error = e.Message});
            }

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await taskService.Delete(id);

                return NoContent();
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Home", new { error = e.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ChangeTaskStatus([FromRoute] Guid id)
        {
            try
            {
                var task = await taskService.ChangeTaskStatus(id);

                var viewModel = mapper.Map<TaskViewModel>(task);

                return Ok(new {Task = viewModel });
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Home", new { error = e.Message });
            }
        }


    }
}
