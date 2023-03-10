using Assignment_Intership.Contracts;
using Assignment_Intership.Models;
using Assignment_Intership.Models.Employee.EmployeeViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Diagnostics;

namespace Assignment_Intership.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmlpoyeeService employeeService;
        private readonly IMapper mapper;

        public HomeController(IEmlpoyeeService employeeService,
            IMapper mapper)
        {
            this.employeeService = employeeService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var top5Employees = await employeeService.GetAll(1, "top5");

            var employeesViewModels = mapper.Map<IEnumerable<EmployeeViewModel>>(top5Employees);

            return View(employeesViewModels);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string error)
        {
            return View(new ErrorViewModel { Error = error });
        }
    }
}