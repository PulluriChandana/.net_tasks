using Microsoft.AspNetCore.Mvc;
using Task5_RESTAPI.Db;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using Task5_RESTAPI.Db.Filter;
using Microsoft.EntityFrameworkCore;
using Task5_RESTAPI.Db.DTO;
namespace Task5_RESTAPI.Services
{
    public class EmployeeService:IEmployeeService
    {
        private static int _nextno = 6;
        public bool Create(Employee employee)
        {
            var result=Validate(employee);
            if (!string.IsNullOrEmpty(result))
            {
                throw new BadHttpRequestException(result);
            }
            employee.Employeeno = _nextno++;
            Employees.employees.Add(employee);
            return true;
        }
        public bool Upadte(int empno, Employee employee)
        {
            var result = Validate(employee);
            if (!string.IsNullOrEmpty(result))
            {
                throw new BadHttpRequestException(result);
            }
            var existingEmployee = Employees.employees.FirstOrDefault(e => e.Employeeno == empno);
            if (existingEmployee == null)
            {
                throw new ArgumentNullException("Employee not found");
            }
            existingEmployee.Employeeno = employee.Employeeno;
            existingEmployee.EmpName = employee.EmpName;
            existingEmployee.JobTitle = employee.JobTitle;
            existingEmployee.HireDate = employee.HireDate;
            existingEmployee.Salary = employee.Salary;
            return true;
        }
        public bool DeleteByNo(int empno)
        {
            var employee = Employees.employees.FirstOrDefault(e => e.Employeeno == empno);
            if (employee == null)
            {
                throw new ArgumentNullException("Id not found");              
            }
           return Employees.employees.Remove(employee);
        }
        public Employee GetByNo(int empno)
        {
            return Employees.employees.FirstOrDefault(e => e.Employeeno == empno);
          
        }
        /*public List<Employee> GetAll()
        {
            return Employees.employees;
        }*/
        public List<Employee> GetAll(EmployeeFilter filter)
        {
            var employees = Employees.employees.AsQueryable();

            // Apply filters based on the filter criteria
            if (filter.DepartmentId.HasValue)
            {
                employees = employees.Where(e => e.DepartmentId == filter.DepartmentId.Value);
            }

            if (filter.Gender.HasValue)
            {
                employees = employees.Where(e => e.Gender == filter.Gender.Value);
            }

            if (filter.SalaryGreaterThan.HasValue)
            {
                employees = employees.Where(e => e.Salary > filter.SalaryGreaterThan.Value);
            }

            if (filter.SalaryLessThan.HasValue)
            {
                employees = employees.Where(e => e.Salary < filter.SalaryLessThan.Value);
            }

            return employees.ToList();
        }

        public List<EmployeDTO> GetAllEmployeeWithDepartment(string location)
        {
            if (!string.IsNullOrEmpty(location))
            {
                var query = Employees.employees.AsQueryable().Where(e =>
                e.Department.Location == location).Select(e => new EmployeDTO
                {
                    Name = e.EmpName,
                    DepartmentName = e.Department.Name,
                    Location = e.Department.Location
                });
                return query.ToList();
            }
            return new List<EmployeDTO>();
        }
        public List<EmployeeDepartmentReport> GetEmployeeDepartmentReport()
        {
            return Employees.employees.GroupBy(e => e.Department.DepartmentId).Select(g => new EmployeeDepartmentReport
            {
                DepartmentId = g.Key,
                DepartmentName = g.FirstOrDefault().Department.Name,
                NoOfEmployees = g.Count()
            })
            .ToList();
        }

        public List<Employee> GetByDepartmentId(int departmentId)
        {
            return Employees.employees.Where(e=>e.DepartmentId == departmentId).ToList();
        }
        public List<Employee> GetByGender(Gender gender)
        {
            return Employees.employees.Where(e=>e.Gender == gender).ToList();
        }
        private static string Validate(Employee employee)
        {
            if(string.IsNullOrEmpty(employee.EmpName) || string.IsNullOrEmpty(employee.JobTitle))
            {
                return "Employee Name and Job Title cannot be empty.";
            }
            if(employee.Salary<0)
            {
                return "Salary must be a Positive Value.";
            }
            //hire date must be within 60 days
    
            var validatingdate=(DateTime.Now).AddDays(-60);
            if(employee.HireDate<validatingdate)
            {
                return "Hire Date must be within the last 60 days";
            }
            var departmentExists=Departments.departments.Any(d=>d.DepartmentId==employee.DepartmentId);
            if (!departmentExists)
            {
                return "Invalid Department ID.";
            }
            return string.Empty;
        }
    }
}
