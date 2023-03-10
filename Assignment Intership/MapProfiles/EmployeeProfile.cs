using Assignment_Intership.Data.Models;
using Assignment_Intership.Models.Employee;
using Assignment_Intership.Models.Employee.EmployeeViewModels;
using AutoMapper;

namespace Assignment_Intership.MapProfiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeServiceModel>();
            CreateMap<CreateEmployeeViewModel, EmployeeServiceModel>();
            CreateMap<EmployeeServiceModel, EmployeeViewModel>()
                .ForMember(x => x.DateOfBirth, y => y.MapFrom(s => s.DateOfBirth.ToString("dd.MM.yyyy")));
            CreateMap<EmployeeServiceModel, EmployeeEditViewModel>();
            CreateMap<EmployeeEditViewModel, EmployeeServiceModel>();
            CreateMap<Employee, EmployeeEditViewModel>();
            CreateMap<EmployeeServiceModel, EmployeeDetailsModel>()
                .ForMember(x => x.DateOfBirth, y => y.MapFrom(s => s.DateOfBirth.ToString("dd.MM.yyyy")))
                .ForMember(x => x.CreatedAt, y => y.MapFrom(s => s.CreatedAt.ToString("dd.MM.yyyy")))
                .ForMember(x => x.UpdatedAt, y => y.MapFrom(s => s.UpdatedAt.ToString("dd.MM.yyyy")));
        }
    }
}
