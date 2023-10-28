using AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel;
using AutoMapper;
using Task.BL.UOW;
using Task.DAl.Entities;
using TaskProject.BL.IServices;
using TaskProject.Models;

namespace TaskProject.BL.Services
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper mapper;
        public EmployeeRepo(IUnitOfWork UOW, IMapper mapper)
        {
            this._unit = UOW;
            this.mapper = mapper;
        }

        public async Task<string> AddEmployeeAsync(EmployeeVM Model)
        {
            try {

                var ifExist = await _unit.Employees.FindAsync(x => x.NationalId == Model.NationalId);
                if (ifExist != null)
                    return "There is Enmployee With This Nathional Id ";
                Employee newone = mapper.Map<Employee>(Model);
                newone.Village = null;
                await _unit.Employees.AddAsync(newone);
                var res = _unit.Complete();
                if (res > 0)// succed
                    return "ok";
                return "Employee doesnot added";
            }
            catch (Exception ex)
            {
                return $"Error :{ex.Message} " + ex.InnerException;
            }

        }

        public async Task<int> DeleteEmployeeAsync(int EmployeeId)
        {
            try
            {
                Employee Deleted = await _unit.Employees.FindAsync(x => x.Id == EmployeeId);
                _unit.Employees.Delete(Deleted);
                return _unit.Complete();
            }
            catch { return 0; }
        }

        public async Task<int> EditEmployeeAsync(EmployeeVM Model)
        {
            try {
                Employee oldone = await _unit.Employees.GetByIdToEditAsync(Model.Id);
                oldone = mapper.Map<Employee>(Model);
                _unit.Employees.Edit(oldone);
                return _unit.Complete();
            }
            catch { return 0; }
        }

        public async Task<List<EmployeeVM>> GetAllEmployeeAsync() {
            try
            {
                var entity = await _unit.Employees.FindAllAsync(x => true, new[] { "Village" });
                var models = mapper.Map<List<Employee>, List<EmployeeVM>>(entity.ToList());
                return models;
            }
            catch
            {
                return new List<EmployeeVM>();
            }


        }

        public async Task<EmployeeVM> GetEmployeeByIdAsync(int EmployeeId)
        {
            var res = await _unit.EmployeeReportVs.FindAsync(x => x.Id == EmployeeId);
            EmployeeVM model = mapper.Map<EmployeeReportV, EmployeeVM>(res);

            return model;
        }


        public async Task<List<EmployeeReportV>> GetEmployeeReport(int Id =0)
        {
            if (Id == 0)
                return _unit.EmployeeReportVs.GetAllAsync().Result.ToList();
            return _unit.EmployeeReportVs.FindAllAsync(x => x.Id == Id).Result.ToList(); 

        }

      
    }
}
