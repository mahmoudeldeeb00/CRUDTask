using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Task.DAL.IdentityEntities
{
    public class TaskIdentityDbContext:IdentityDbContext<AppUser>
    {
        public TaskIdentityDbContext(DbContextOptions<TaskIdentityDbContext> options ):base(options)
        {
            
        }
    }
}
