using TASKS_6_MVC_.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TASKS_6_MVC_.Services;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<HrDbContext>();
builder.Services.AddScoped<IDepartmentService, DepartmentServiceWithEF>();
builder.Services.AddScoped<IEmployeeService, EmployeeServiceWithEF>();
builder.Services.AddIdentity<SampleUser,SampleRole>().
    AddEntityFrameworkStores<HrDbContext>();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireUppercase = true;
    options.User.RequireUniqueEmail = true;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
