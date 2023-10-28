
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Task.BL.IServices;
using Task.DAL.IdentityEntities;
using Task.Models.IdentityModels;

namespace Task.BL.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _US;
        private readonly SignInManager<AppUser> _SignIn;
        private readonly RoleManager<IdentityRole> _Role;
        public AccountService(UserManager<AppUser> US, SignInManager<AppUser> _SignIn, RoleManager<IdentityRole> _Role)
        {
            this._US = US;
            this._SignIn = _SignIn;
            this._Role = _Role;
        }
        public async Task< string> RegisterAsync(SignUpVM Model)
        {
            try
            {
                var user = _US.Users.FirstOrDefault(x=>x.UserName==Model.UserName || x.Email==Model.Email );
                if (user != null)
                    return "User Name Or Email Exist , Try Another One ";
                AppUser newone = new AppUser
                {
                    UserName = Model.UserName,
                    Email = Model.Email
                };
                var result = await _US.CreateAsync(newone, Model.Password);
                if (result.Succeeded)
                {
                    var newuser  = await  _US.FindByNameAsync(Model.UserName);
                    await _US.AddToRoleAsync(newuser, "User");
                    return "ok";
                }
                return "There Is An Issu";
            }
            catch
            { return "Error" ; }
        }
       
        public async Task<string> LoginAsync(LoginVM Model)
        {
            try
            {
                AppUser u = _US.FindByEmailAsync(Model.Email).Result;
                if(u is null)
                    u = _US.FindByNameAsync(Model.Email).Result;
                if (u != null)
                {
                    var result = await _SignIn.PasswordSignInAsync(u, Model.Password, Model.Remmember, false);
                    if (result.Succeeded)
                    {
                        return "ok";
                    }
                    return "invalid Email or Password ! ";

                }
                return "invalid Email or Password ! ";

            }
            catch(Exception ex )
            {
                return ex.Message;
            }
        }
        public void LogOut()
        {
            _SignIn.SignOutAsync();
        }
        public async Task<string> AddRoleAsync(string Role)
        {
           
            if (!await _Role.RoleExistsAsync(Role))
            {
                var result = await _Role.CreateAsync(new IdentityRole(Role));
                return result.Succeeded ? "ok" : "Error";
            }
            return "Role Exist !";
        }

        public async Task<List<IdentityRole>> RoleAsync()
        {
            var result = await _Role.Roles.ToListAsync();
            return result; 
        }


        ////////////////////////// Admin Zone ///////////////////////
        ///
        public async Task<List<AppUser>> UsersAsync()=>  _US.Users.ToListAsync().Result;
        
        
      

       public async  Task<int> ResetRolesAsync(string userName, List<string> Roles)
        {
            try
            {
                var user =await  _US.FindByNameAsync(userName);
                var allroles = (await _US.GetRolesAsync(user)).Union(Roles);
                foreach(var item in allroles)
                {
                    if (Roles.Contains(item))
                    {
                        await _US.AddToRoleAsync(user, item);
                    }
                    else
                    {
                        await _US.RemoveFromRoleAsync(user, item);
                    }
                }
                return 1;
            }
            catch { return 0;  }

        }


        public async Task<List<string>> GetUserRoles(string UserName)
        {
            var user = await _US.FindByNameAsync(UserName);
            return  _US.GetRolesAsync(user).Result.ToList();
        } 
    }
}
