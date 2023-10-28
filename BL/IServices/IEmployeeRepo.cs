
using TaskProject.Models;

namespace TaskProject.BL.IServices
{
    public interface IEmployeeRepo
    {
        Task<string> AddEmployeeAsync(EmployeeVM Model);
        Task<List<EmployeeVM>> GetAllEmployeeAsync();
        Task<EmployeeVM> GetEmployeeByIdAsync(int EmployeeId);
        Task<int> DeleteEmployeeAsync(int EmployeeId);
        Task<int> EditEmployeeAsync(EmployeeVM Model);
        Task<List<EmployeeReportV>> GetEmployeeReport(int Id);
    }
}
