using Assignment_Intership.Data.Models;
using Assignment_Intership.Models.Employee;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Assignment_Intership.Contracts
{
    public interface IEmlpoyeeService
    {
        Task<EmployeeServiceModel> Create(EmployeeServiceModel model);

        Task<IEnumerable<EmployeeServiceModel>> GetAll(int pageNumber, string criteria = null);

        Task<Employee> GetById(Guid? id);

        Task<EmployeeServiceModel> Edit(EmployeeServiceModel model);

        Task<bool> Delete(Guid id);

        Task<List<SelectListItem>> GetSelectListItems();

        Task<int> GetTotalPages();

        Task<EmployeeServiceModel> GetDetails(Guid id);

        Task<byte[]> GetImage(Guid id);
    }
}
