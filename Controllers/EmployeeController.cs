
using Microsoft.AspNetCore.Mvc;

using TaskProject.BL.IServices;
using TaskProject.Models;

using AspNetCore.Reporting;
using Task.DAL.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Task.Helper;
using Microsoft.AspNetCore.Authorization;

namespace TaskProject.Controllers
{
    
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepo _Emps;
        private readonly ISelectService _Select;
        
        private readonly UserHelper user;
        public EmployeeController(IEmployeeRepo _Emps , ISelectService _Select, UserManager<AppUser> _user)
        {
            this._Emps = _Emps;
            this._Select = _Select;
            this.user = new UserHelper(_user);
        }

        public IActionResult Index() => View(_Emps.GetAllEmployeeAsync().Result);
        [Authorize(Roles ="User")]
        public IActionResult AddEmployee() {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeVM Model) {

            if (!ModelState.IsValid)
                return View(Model);
            
            string res = await _Emps.AddEmployeeAsync(Model);
            if (res == "ok")
                return RedirectToAction("Index");

           
            ModelState.AddModelError("", res);

            return View(Model);
        }

        [HttpGet]
        [Authorize(Roles ="Editor")]
        public IActionResult EditEmployee(int Id)
        {
            var Model = _Emps.GetEmployeeByIdAsync(Id).Result;
            return View(Model);
        }
        public async Task<IActionResult> EditEmployee(EmployeeVM Model)
        {
            if (!ModelState.IsValid)
                return View(Model);
            
            int res = await _Emps.EditEmployeeAsync(Model);
            if (res == 1)
                return RedirectToAction("Index");


            ModelState.AddModelError("", "Not Editted Please Try Again ");
            return View(Model);
        }
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<JsonResult> DelteEmployee (int EmployeeId)
        {
            
            var result = await _Emps.DeleteEmployeeAsync(EmployeeId);
            return Json(new { data = result });

        }




        [HttpPost]
        public JsonResult Governates() => Json(_Select.Governates());
        [HttpPost]
        public JsonResult Cities( int GovernateId) => Json(_Select.Cities(GovernateId)); 
        [HttpPost]
        public JsonResult Villages(int CityId) => Json(_Select.Villages(CityId));


        [Authorize(Roles ="Admin")]
        public IActionResult EmployeePrint (int Id)
        {
            List<EmployeeReportV> data = _Emps.GetEmployeeReport(Id).Result;
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("User", user.User().UserName);
            string Path = "\\wwwroot\\Report\\Report1.rdlc";
            return GetPDF(Path, parameters,data);

        }
        public IActionResult AllEmployeePrint()
        {
            List<EmployeeReportV> data = _Emps.GetEmployeeReport(0).Result;
            Dictionary<string, string> parameters = new Dictionary<string, string>() { };
            parameters.Add("User", user.User().UserName);
            parameters.Add("Count", data.Count.ToString());
            string Path = "\\wwwroot\\Report\\Report12.rdlc";
            return GetPDF(Path, parameters, data);

        }




        private ActionResult GetPDF(string source , Dictionary<string, string> parameters , List<EmployeeReportV> data)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            string mimtype = "";
            int extension = 0;
            string Path = Directory.GetCurrentDirectory() + source;
            LocalReport localreport = new LocalReport(Path);
            
            localreport.AddDataSource("DataSet1", data);
            var result = localreport.Execute(RenderType.Pdf, extension, parameters, mimtype);
            return File(result.MainStream, "application/pdf");
        }

    }
}
