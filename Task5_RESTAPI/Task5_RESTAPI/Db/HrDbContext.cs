using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics.Metrics;

namespace Task5_RESTAPI.Db
{
    public class HrDbContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=IN3563405W1\\SQLEXPRESS;Initial Catalog=office;Integrated Security = True; Connect Timeout = 30; Encrypt=False;Trust Server Certificate=False;Application Intent = ReadWrite; Multi Subnet Failover=False")
                .EnableSensitiveDataLogging(true).LogTo(Console.WriteLine);
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Role> roles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasKey(e => e.Employeeno);
            modelBuilder.Entity<Employee>()
        .Property(e => e.Age) 
        .HasComputedColumnSql("DATEDIFF(YEAR, DateOfBirth, GETDATE())");
            modelBuilder.Entity<Department>().HasKey(d=>d.DepartmentId);

            modelBuilder.Entity<Role>().HasKey(d=>d.RoleId);

            modelBuilder.Entity<Employee>().HasOne(e=>e.Department)
                .WithMany(d=>d.Employees).HasForeignKey(e=>e.DepartmentId);

            modelBuilder.Entity<Employee>().Ignore(e=>e.Age);
            modelBuilder.Entity<Employee>().HasOne(e => e.Role)
                .WithMany(d => d.Employees).HasForeignKey(e => e.RoleId);
            base.OnModelCreating(modelBuilder);
        }
    }
}
