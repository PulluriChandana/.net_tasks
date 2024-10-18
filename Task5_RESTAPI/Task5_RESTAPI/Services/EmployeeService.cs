using Microsoft.AspNetCore.Mvc;
using Task5_RESTAPI.Db;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
namespace Task5_RESTAPI.Services
{
    public class EmployeeService
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
        public List<Employee> GetAll()
        {
            return Employees.employees;
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
