using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Task5_RESTAPI.Db;
using Task5_RESTAPI.Services;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddDbContext<HrDbContext>(options =>
//options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<HrDbContext>();
builder.Services.AddScoped<IDepartmentService, DepartmentServiceWithEF>();
builder.Services.AddScoped<IRoleService, RoleServiceWithEF>();



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
