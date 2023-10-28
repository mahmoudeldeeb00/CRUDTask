using Microsoft.AspNetCore.Identity;

namespace Task.DAL.IdentityEntities
{
    public class AppUser :IdentityUser
    {
        public string? PicSource { get; set; }
    }
}
