using Assignment_Intership.Contracts;
using Assignment_Intership.Models.Employee;
using Assignment_Intership.Models.Employee.EmployeeViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_Intership.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmlpoyeeService employeeService;
        private readonly IMapper mapper;

        public EmployeeController(IEmlpoyeeService employeeService,
            IMapper maper)
        {
            this.employeeService = employeeService;
            this.mapper = maper;
        }

        public async Task<IActionResult> Index(int pageNumber)
        {
            if (pageNumber == 0)
            {
                pageNumber = 1;
            }

            var homeViewModel = new HomeEmployeeViewModel();

            var employeesServiceModels = await employeeService.GetAll(pageNumber);

            var employeesViewModels = mapper.Map<IEnumerable<EmployeeViewModel>>(employeesServiceModels);

            homeViewModel.Employees = employeesViewModels;
            homeViewModel.TotalPages = await employeeService.GetTotalPages();
            homeViewModel.Page = pageNumber;

            return View(homeViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var error = string.Join(Environment.NewLine, ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));

                return RedirectToAction("Error", "Home", new { error });
            }

            var employeeServiceModel = mapper.Map<EmployeeServiceModel>(model);
            try
            {
                var newEmployee = await employeeService.Create(employeeServiceModel);

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
            var employeeModel = await employeeService.GetById(id);

            if (employeeModel == null)
            {
                return RedirectToAction("Error", "Home", new { error = "Employee not found" });
            }

            var viewModel = mapper.Map<EmployeeEditViewModel>(employeeModel);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var error = string.Join(Environment.NewLine, ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));

                return RedirectToAction("Error", "Home", new { error });
            }

            var employeeToEdit = mapper.Map<EmployeeServiceModel>(model);
            try
            {
                var employeeServiceModel = await employeeService.Edit(employeeToEdit);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Home", new { error = e.Message });
            }
            
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await employeeService.Delete(id);

                return NoContent();
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Home", new { error = e.Message });
            }

        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var employee = await employeeService.GetDetails(id);

                var detailsModel = mapper.Map<EmployeeDetailsModel>(employee);

                return View(detailsModel);
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Home", new { error = e.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeeImage([FromRoute] Guid id)
        {
            var img = await employeeService.GetImage(id);

            return Ok(new { Photo = img });
        }
    }
}
