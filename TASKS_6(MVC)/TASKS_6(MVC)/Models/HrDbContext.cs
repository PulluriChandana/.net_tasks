using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TASKS_6_MVC_.Models
{
    public class HrDbContext : IdentityDbContext<SampleUser,SampleRole,string>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=IN3563405W1\\SQLEXPRESS;Initial Catalog=office;Integrated Security = True; Connect Timeout = 30; Encrypt=False;Trust Server Certificate=False;Application Intent = ReadWrite; Multi Subnet Failover=False")
                .EnableSensitiveDataLogging(true).LogTo(Console.WriteLine);
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<SampleUser> Users { get; set; }
        public DbSet<SampleRole> Roles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasKey(e => e.Employeeno);
            modelBuilder.Entity<Department>().HasKey(d => d.DepartmentId);
            modelBuilder.Entity<Employee>().HasOne(e => e.Department)
                    .WithMany(d => d.Employees).HasForeignKey(e => e.DepartmentId);
            modelBuilder.Entity<SampleUser>().Property(e=>e.FirstName).HasMaxLength(50);
            modelBuilder.Entity<SampleUser>().Property(e=>e.LastName).HasMaxLength(50);
            modelBuilder.Entity<SampleRole>().Property(e=>e.Description).HasMaxLength(255);
            base.OnModelCreating(modelBuilder);
        }
    }
}