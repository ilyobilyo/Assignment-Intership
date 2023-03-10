using Assignment_Intership.Models.Task;
using Assignment_Intership.Models.Task.TaskViewModels;
using AutoMapper;

namespace Assignment_Intership.MapProfiles
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<Assignment_Intership.Data.Models.Task, TaskServiceModel>();
            CreateMap<CreateTaskViewModel, TaskServiceModel>();
            CreateMap<TaskServiceModel, Assignment_Intership.Data.Models.Task>();
            CreateMap<TaskServiceModel, TaskViewModel>()
                .ForMember(x => x.Employee, y => y.MapFrom(s => s.Employee.FullName))
                .ForMember(x => x.DueDate, y => y.MapFrom(s => s.DueDate.ToString("dd.MM.yyyy")))
                .ForMember(x => x.CreatedAt, y => y.MapFrom(s => s.CreatedAt.ToString("dd.MM.yyyy")));
            CreateMap<Assignment_Intership.Data.Models.Task, TaskEditViewModel>();
            CreateMap<TaskEditViewModel, TaskServiceModel>();
            CreateMap<TaskServiceModel, EmployeeTasksModel>()
                .ForMember(x => x.CompletedAt, y => y.MapFrom(s => s.CompletedAt.ToString("dd.MM.yyyy")));
        }
    }
}
