using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Task.BL.IServices;
using Task.DAL.IdentityEntities;
using Task.Helper;

namespace Task.Controllers
{
    [Authorize(Roles="Admin")]
    public class AdminController : Controller
    {
        private readonly IAccountService _Acc;
        private readonly UserHelper us;
        public AdminController(IAccountService _Acc , UserManager<AppUser> userManager )
        {
            this._Acc = _Acc;
            this.us = new UserHelper(userManager);
        }

        public async Task<IActionResult> Roles()
        {
            ViewBag.Roles = await _Acc.RoleAsync();
            return View();
        }

        public ActionResult AddRole()
        {
            return PartialView("_AddRole");
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(string Role)
        {
            await _Acc.AddRoleAsync(Role);

            return RedirectToAction("Roles");
        }


        public async Task<IActionResult> Users()
        {
            ViewBag.Users = await _Acc.UsersAsync();
           
            return View();
        }

        public IActionResult EditUserRole(string UserName)
        {
            ViewBag.UserName = UserName; 
            var uesrroles = _Acc.GetUserRoles(UserName).Result;
            var allRoles = _Acc.RoleAsync().Result.Select(s => s.Name).ToList();
            ViewBag.NotUserRoles = allRoles.Except(uesrroles);
            ViewBag.UserRoles = uesrroles; 
            return PartialView("_EditUserRole");
        }

        [HttpPost]
       public  JsonResult EditUserRole(string UserName,List<string> NewRoles)
        {
            int result = _Acc.ResetRolesAsync(UserName, NewRoles).Result;
            return Json(new { data = result });
        }

    }
}
