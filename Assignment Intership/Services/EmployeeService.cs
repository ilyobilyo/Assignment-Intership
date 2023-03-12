using Assignment_Intership.Constants;
using Assignment_Intership.Contracts;
using Assignment_Intership.Data.Models;
using Assignment_Intership.Data.Repositories;
using Assignment_Intership.Models.Employee;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Assignment_Intership.Services
{
    public class EmployeeService : IEmlpoyeeService
    {
        private readonly IApplicationRepository repo;
        private readonly IMapper mapper;

        public EmployeeService(IApplicationRepository repo,
            IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        public async Task<EmployeeServiceModel> Create(EmployeeServiceModel model)
        {
            if (model.FullName == null)
            {
                throw new ArgumentException("Full Name is required");
            }

            if (model.Email == null)
            {
                throw new ArgumentException("Email is required");
            }

            if (model.PhoneNumber == null)
            {
                throw new ArgumentException("Phone Number is required");

            }

            if (model.PhotoFile.Length == 0)
            {
                throw new ArgumentException("Image is required");
            }

            var employee = new Employee()
            {
                FullName = model.FullName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                DateOfBirth = model.DateOfBirth,
                MonthlySalary = model.MonthlySalary,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            employee.Photo = PhotoToBinary(model.PhotoFile);

            await repo.AddAsync(employee);
            await repo.SaveChangesAsync();

            return mapper.Map<EmployeeServiceModel>(employee);
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                await repo.DeleteAsync<Employee>(id);
                await repo.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public async Task<EmployeeServiceModel> Edit(EmployeeServiceModel model)
        {
            if (model.FullName == null)
            {
                throw new ArgumentException("Full Name is required");
            }

            if (model.Email == null)
            {
                throw new ArgumentException("Email is required");
            }

            if (model.PhoneNumber == null)
            {
                throw new ArgumentException("Phone Number is required");

            }

            var employee = await GetById(model.Id);

            UpdateEmployee(employee, model);

            await repo.SaveChangesAsync();

            return mapper.Map<EmployeeServiceModel>(employee);
        }

        public async Task<IEnumerable<EmployeeServiceModel>> GetAll(int pageNumber, string criteria = null)
        {
            int pageSize = PagenationConstants.PageSize;

            List<Employee> employeesDataModels;

            if (criteria == "top5")
            {
                var employeesWithCompletedTasksForLastMonthExists = await repo.All<Employee>().AnyAsync(x => x.CompletedTasksForLastMonth > 0);

                if (employeesWithCompletedTasksForLastMonthExists)
                {
                    employeesDataModels = await repo.All<Employee>()
                        .Where(x => x.CompletedTasksForLastMonth > 0)
                        .OrderByDescending(x => x.CompletedTasksForLastMonth)
                        .Take(5)
                        .ToListAsync();
                }
                else
                {
                    employeesDataModels = await repo.All<Employee>()
                        .Include(x => x.Tasks)
                        .Where(x => x.Tasks.Any(s => s.CompletedAt.Month == DateTime.Now.Month - 1))
                        .OrderByDescending(x => x.Tasks.Count(s => s.CompletedAt.Month == DateTime.Now.Month - 1))
                        .Take(5)
                        .ToListAsync();
                }

                if (DateTime.Now.Day == 1 || employeesDataModels.Count() > 0)
                {
                    if (DateTime.Now.Day == 1)
                    {
                        foreach (var employee in employeesDataModels)
                        {
                            employee.CompletedTasksForLastMonth = await GetTasksCountForLastMonth(employee.Id);
                        }
                    }
                    else
                    {
                        foreach (var employee in employeesDataModels)
                        {
                            employee.CompletedTasksForLastMonth = await GetTasksCountForLastMonth(employee.Id);
                        }
                    }


                    await repo.SaveChangesAsync();
                }

                var serviceModels = mapper.Map<IEnumerable<EmployeeServiceModel>>(employeesDataModels);


                return serviceModels;

            }
            else
            {
                employeesDataModels = await repo.All<Employee>()
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }

            return mapper.Map<IEnumerable<EmployeeServiceModel>>(employeesDataModels);
        }

        public async Task<Employee> GetById(Guid? id)
        {
            return await repo.All<Employee>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<EmployeeServiceModel> GetDetails(Guid id)
        {
            var employee = await repo.All<Employee>()
                .Include(x => x.Tasks)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (employee == null)
            {
                throw new ArgumentException("Employee not found");
            }

            return mapper.Map<EmployeeServiceModel>(employee);
        }

        public async Task<List<SelectListItem>> GetSelectListItems()
        {
            var list = await repo.All<Employee>().Select(a =>
                                  new SelectListItem
                                  {
                                      Value = a.Id.ToString(),
                                      Text = a.FullName
                                  }).ToListAsync();

            list.Insert(0, new SelectListItem
            {
                Value = null,
                Text = null,
            });

            return list;
        }

        public async Task<int> GetTotalPages()
        {
            var totalEmployeesCount = await repo.All<Employee>().CountAsync();

            if (totalEmployeesCount % PagenationConstants.PageSize != 0)
            {
                return (totalEmployeesCount / PagenationConstants.PageSize) + 1;
            }

            return totalEmployeesCount / PagenationConstants.PageSize;
        }

        private void UpdateEmployee(Employee employee, EmployeeServiceModel model)
        {
            employee.FullName = model.FullName;
            employee.Email = model.Email;
            employee.PhoneNumber = model.PhoneNumber;
            employee.MonthlySalary = model.MonthlySalary;
            employee.DateOfBirth = model.DateOfBirth;
            employee.Photo = PhotoToBinary(model.PhotoFile);
            employee.UpdatedAt = DateTime.Now;
        }

        private byte[] PhotoToBinary(IFormFile file)
        {
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var bytes = ms.ToArray();

                return bytes;
            }
        }

        private async Task<int> GetTasksCountForLastMonth(Guid id)
        {
            return await repo.All<Data.Models.Task>()
                .Include(x => x.Employee)
                .Where(x => x.EmployeeId == id)
                .CountAsync(x => x.CompletedAt.Month == DateTime.Now.Month - 1);
        }

        public Task<byte[]> GetImage(Guid id)
        {
            return repo.All<Employee>()
                .Where(x => x.Id == id)
                .Select(x => x.Photo)
                .FirstOrDefaultAsync();
        }
    }
}
