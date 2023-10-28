using Task.DAl.Entities;

namespace Task.BL.UOW
{
    public interface IUnitOfWork : IDisposable
    {

        IBaseRepo<Employee> Employees { get; }
        IBaseRepo<EmployeeReportV> EmployeeReportVs { get; }
        IBaseRepo<Governate> Governates { get; }
        IBaseRepo<City> Cities { get; }
        IBaseRepo<Village> Villages { get; }
        int Complete();
        void RollBack();

    }
}
