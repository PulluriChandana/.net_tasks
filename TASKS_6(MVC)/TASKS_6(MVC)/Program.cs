using TASKS_6_MVC_.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TASKS_6_MVC_.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<HrDbContext>();
builder.Services.AddScoped<IDepartmentService, DepartmentServiceWithEF>();
builder.Services.AddScoped<IEmployeeService, EmployeeServiceWithEF>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
