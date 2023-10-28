using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Task.BL.IServices;
using Task.BL.Services;
using Task.BL.UOW;
using Task.DAl.Entities;
using Task.DAL.IdentityEntities;
using Task.Mapper;
using TaskProject.BL.IServices;
using TaskProject.BL.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<TaskDataBaseContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("TaskDataBaseConnection"))
);

builder.Services.AddDbContext<TaskIdentityDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("TaskDataBaseConnection"))
);
builder.Services.AddIdentity<AppUser, IdentityRole>(options => {
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 3;
    options.Password.RequiredUniqueChars = 0;
})
    .AddEntityFrameworkStores<TaskIdentityDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Account/Login";
    options.LogoutPath = $"/Account/Logout";

    options.AccessDeniedPath = $"/Account/AccessDenied";
});
builder.Services.AddAutoMapper(options => options.AddProfile(new DomainProfile()));
builder.Services.AddScoped(typeof(IBaseRepo<>),typeof( BaseRepo<>));
builder.Services.AddScoped(typeof(IUnitOfWork),typeof( UnitOfWork));
builder.Services.AddScoped(typeof(IAccountService),typeof(AccountService));
builder.Services.AddScoped(typeof(IEmployeeRepo),typeof(EmployeeRepo));
builder.Services.AddScoped(typeof(ISelectService),typeof(SelectService));
builder.Services.AddScoped<TaskDataBaseContext>();
builder.Services.AddScoped<HttpContextAccessor>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{

    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();

