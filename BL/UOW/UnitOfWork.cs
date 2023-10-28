using Microsoft.EntityFrameworkCore;
using Task.DAl.Entities;

namespace Task.BL.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TaskDataBaseContext _context;
        public UnitOfWork(TaskDataBaseContext context)
        {
            this._context = context;
            this.Employees = new BaseRepo<Employee>(context);
            this.Governates = new BaseRepo<Governate>(context);
            this.Cities = new BaseRepo<City>(context);
            this.Villages = new BaseRepo<Village>(context);
            this.EmployeeReportVs = new BaseRepo<EmployeeReportV>(context);
        }
     
        public IBaseRepo<Employee> Employees { get; private set; }
        public IBaseRepo<Governate> Governates { get; private set; }
        public IBaseRepo<City> Cities { get; private set; }
        public IBaseRepo<Village> Villages { get; private set; }
        public IBaseRepo<EmployeeReportV> EmployeeReportVs { get; private set; }
        public int Complete() => _context.SaveChanges();
        public void RollBack() => _context.ChangeTracker.Clear();
        public void Dispose() { _context.Dispose(); }
    }
}
