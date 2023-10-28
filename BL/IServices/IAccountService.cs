using Microsoft.AspNetCore.Identity;
using Task.DAL.IdentityEntities;
using Task.Models.IdentityModels;

namespace Task.BL.IServices
{
    public interface IAccountService
    {


        Task<string> RegisterAsync(SignUpVM Model);
        Task<string> LoginAsync(LoginVM Model);
        void LogOut();
        Task<string> AddRoleAsync(string Role);
        Task<List<IdentityRole>> RoleAsync();

        Task<List<AppUser>> UsersAsync();
        Task<int> ResetRolesAsync(string userName , List<string>Roles);
        Task<List<string>> GetUserRoles(string UserName);
    }
}
