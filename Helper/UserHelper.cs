
using Microsoft.AspNetCore.Identity;
using System;
using Task.DAL.IdentityEntities;

namespace Task.Helper
{


    public class UserHelper
    {
        private readonly HttpContextAccessor Http = new HttpContextAccessor();
       private readonly UserManager<AppUser> usermanager;
        public UserHelper( UserManager<AppUser> usermanager)
        {
            this.usermanager = usermanager;
        }
        public string UserName() {

            return Http.HttpContext.User.Identity.Name;
        } 
       
         public AppUser User()
        {

            if (UserName() == null)
               return new AppUser() { UserName = "UnKnown" };
            return usermanager.FindByNameAsync(UserName()).Result; 

        }
        public List<string> Roles() {
            if (User().UserName == "UnKnown")
                return new List<string>();
            return usermanager.GetRolesAsync(User()).Result.ToList();
                
                } 
       

    }
}
