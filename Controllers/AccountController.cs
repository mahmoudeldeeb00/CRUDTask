

using Microsoft.AspNetCore.Mvc;
using Task.BL.IServices;

using Task.Models.IdentityModels;

namespace Task.Controllers
{
 
    public class AccountController : Controller
    {

        private readonly IAccountService _Acc;
        public AccountController(IAccountService _Acc)
        {
            this._Acc = _Acc;   
        }
        public IActionResult Login(string returnUrl=null)
        {
            if (returnUrl == null)
                returnUrl = "/Home/Index";
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login( LoginVM Model, string returnUrl="")
        {
            
            ViewBag.ReturnUrl = returnUrl;
            if (!ModelState.IsValid)
                return View(Model);
            
            var res =await _Acc.LoginAsync(Model);
            if (res == "ok")
            {
                if (returnUrl != "" && returnUrl != null)
                    return Redirect(returnUrl);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", res);
            return View(Model);
        }
        public IActionResult SignUp()
        {
          
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp( SignUpVM Model)
        {
            if (!ModelState.IsValid)
                return View(Model);
            var res = await _Acc.RegisterAsync(Model);
            if(res=="ok")
                return RedirectToAction("Login");
            ModelState.AddModelError("", res);
            return View(Model);
        }
        public IActionResult LogOut()
        {
            _Acc.LogOut();
            return RedirectToAction("Index","Home");
        }
      
        public IActionResult AccessDenied()=> View();
        
    }
}
