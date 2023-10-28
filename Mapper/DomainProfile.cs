
using AutoMapper;
using Task.DAl.Entities;
using TaskProject.Models;

namespace Task.Mapper
{
    public class DomainProfile  : Profile
    {

        public DomainProfile()
        {
            CreateMap<Employee, EmployeeVM>()
             .ForMember(x => x.VillageName, y => y.MapFrom(t => t.Village.Name ));
            CreateMap<EmployeeVM, Employee>()
              .ForMember(x => x.Village, y => y.Ignore());

            CreateMap<EmployeeReportV, EmployeeVM>();
            CreateMap<EmployeeVM, EmployeeReportV>();
        }
    }
}
