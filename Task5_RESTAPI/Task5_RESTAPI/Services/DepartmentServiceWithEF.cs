﻿using Task5_RESTAPI.Db;

namespace Task5_RESTAPI.Services
{
    public class DepartmentServiceWithEF : IDepartmentService
    {
        private readonly HrDbContext hrDbContext;
        public DepartmentServiceWithEF(HrDbContext hrDbContext)
        {
            this.hrDbContext = hrDbContext;
        }

        public bool Create(Department department)
        {
            // add validation in both departmen tand employee
            var validationResult=Validate(department,hrDbContext);
            if (!string.IsNullOrEmpty(validationResult))
            {
                throw new BadHttpRequestException(validationResult);
            }
            this.hrDbContext.Add(department);
            return hrDbContext.SaveChanges()>0;
        }
        public bool DeleteById(int id)
        {
            var department=hrDbContext.Departments.Find(id);
            if (department == null)
            {
                throw new ArgumentNullException("Id not found");
            }
            this.hrDbContext.Remove(department);
            return hrDbContext.SaveChanges()>0;
        }
        public List<Department> GetAll()
        {
            return this.hrDbContext.Departments.ToList();
        }

        public Department GetById(int id)
        {
            return this.hrDbContext.Departments.Find(id);
        }

        public bool Update(int id, Department department)
        {
            var validationResult = Validate(department,hrDbContext);
            if (!string.IsNullOrEmpty(validationResult))
            {
                throw new BadHttpRequestException(validationResult);
            }
            var existingdepartment = hrDbContext.Departments.Find(id);
            if (existingdepartment == null)
            {
                throw new ArgumentNullException("Employee not found");
            }
            existingdepartment.Name = department.Name;
            existingdepartment.Location = department.Location;
            return hrDbContext.SaveChanges() > 0;
        }
        private static string Validate(Department department,HrDbContext hrDbContext)
        {
            //should not be empty
            if (string.IsNullOrEmpty(department.Name) || string.IsNullOrEmpty(department.Location))
            {
                return "Department name and location cannot be empty.";
            }
            // name should be unique
            bool isNameUnique = !hrDbContext.Departments.Any(d =>
            string.Equals(d.Name, department.Name, StringComparison.OrdinalIgnoreCase));
            if (!isNameUnique)
            {
                return "Department name must be unique";
            }
            return string.Empty;
        }
    }
}