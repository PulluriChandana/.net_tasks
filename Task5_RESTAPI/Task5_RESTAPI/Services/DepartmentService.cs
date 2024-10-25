using Microsoft.AspNetCore.Mvc;
using Task5_RESTAPI.Db;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Task5_RESTAPI.Services
{
    public class DepartmentService:IDepartmentService
    {
        private static int _nextId = 9;
        public bool Create(Department department)
        {
            // Validate
            var validationResult = Validate(department);
            if (!string.IsNullOrEmpty(validationResult))
            {
                throw new BadHttpRequestException(validationResult);
            }
            //db insert
            department.DepartmentId = _nextId++;
            Departments.departments.Add(department);
            return true;

        }
        public List<Department> GetDepartmentByLocation(string location)
        {
            return Departments.departments.Where(d => d.Location.Equals(location, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        public bool Update(int id,Department department)
        {
            var validationResult = Validate(department);
            if (!string.IsNullOrEmpty(validationResult))
            {
                throw new BadHttpRequestException(validationResult);
            }
            var existingDepartment = Departments.departments.FirstOrDefault(d => d.DepartmentId == id);
            if (existingDepartment == null)
            {
                throw new ArgumentNullException("Employee not found");
            }

            existingDepartment.Name = department.Name;
            existingDepartment.Location = department.Location;

            return true;
        }

        public bool DeleteById(int id)
        {
            var department = Departments.departments.FirstOrDefault(d => d.DepartmentId == id);
            if (department == null)
            {
                throw new ArgumentNullException("Id not found");
            }

            return Departments.departments.Remove(department);
        }
        public Department GetById(int id)
        {
            return Departments.departments.FirstOrDefault(d => d.DepartmentId == id);
        }

        public List<Department> GetAll()
        {
            return Departments.departments;
        }

        private static string Validate(Department department)
        {
            //should not be empty
            if (string.IsNullOrEmpty(department.Name) || string.IsNullOrEmpty(department.Location))
            {
                return "Department name and location cannot be empty.";
            }

            // name should be unique
            if (Departments.departments.Any(d => string.Equals(d.Name,department.Name,StringComparison.OrdinalIgnoreCase)))
            {
                return "Department name must be unique.";
            }

            return string.Empty; 
        }
    }
}