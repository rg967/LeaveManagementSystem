using AutoMapper;
using LeaveManagementSystem.Web.Data;
using LeaveManagementSystem.Web.Models.LeaveTypes;

namespace LeaveManagementSystem.Web.MappingProfile
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<LeaveType, LeaveTypeReadOnlyVM>();
               // .ForMember(dest => dest.Days, opt => opt.MapFrom(src => src.NumberOfDays)); use it when we don't have matching field in model to viewmodel
            CreateMap<LeaveTypeCreateVM, LeaveType>();
            CreateMap<LeaveTypeEditVM, LeaveType>().ReverseMap(); //ReverseMap to create mapping in both directions
        }

    }
}
