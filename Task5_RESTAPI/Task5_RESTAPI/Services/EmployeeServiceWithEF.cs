using Task5_RESTAPI.Db;

namespace Task5_RESTAPI.Services
{
    public class EmployeeServiceWithEF : IEmployeeService
    {
        private readonly HrDbContext hrDbContext;
        public EmployeeServiceWithEF(HrDbContext hrDbContext) 
        {
            this.hrDbContext = hrDbContext;
        }

        public bool Create(Employee employee)
        {
            var result=Validate(employee,hrDbContext);
            if(!string.IsNullOrEmpty(result))
            {
                throw new BadHttpRequestException(result);
            }
            this.hrDbContext.Add(employee);
            return hrDbContext.SaveChanges()>0;
        }
        public bool DeleteByNo(int empno)
        {
            var employee = hrDbContext.Employees.Find(empno);
            if (employee == null)
            {
                throw new ArgumentNullException("Id not found");
            }
            this.hrDbContext.Remove(employee);
            return hrDbContext.SaveChanges() > 0;
        }
        public List<Employee> GetByGender(Gender gender)
        {
            return hrDbContext.Employees.Where(e=>e.Gender == gender).ToList();
        }
        public List<Employee> GetAll()
        {
            return hrDbContext.Employees.ToList();
        }
        public List<Employee> GetByDepartmentId(int departmentId)
        {
            return this.hrDbContext.Employees.Where(e=>e.DepartmentId==departmentId).ToList();
        }
        public Employee GetByNo(int empno)
        {
            return this.hrDbContext.Employees.Find(empno);
        }
        public bool Upadte(int empno, Employee employee)
        {
            var result=Validate(employee, hrDbContext);
            if (!string.IsNullOrEmpty(result))
            {
                throw new BadHttpRequestException(result);
            }
            var existingemployee = this.hrDbContext.Employees.Find(empno);
            if (existingemployee == null) 
            {
                throw new ArgumentNullException("Employee not found"); 
            }
            existingemployee.EmpName = employee.EmpName;
            existingemployee.JobTitle = employee.JobTitle;
            existingemployee.HireDate = employee.HireDate;
            existingemployee.Salary = employee.Salary;
            return hrDbContext.SaveChanges() > 0;
        }
        private static string Validate(Employee employee,HrDbContext hrDbContext)
        {
            if(string.IsNullOrEmpty(employee.EmpName)||string.IsNullOrEmpty(employee.JobTitle))
            {
                return "Employee Name and Job Title cannot be empty.";
            }
            if(employee.Salary<0)
            {
                return "Salary must be a Positive Value.";
            }
            var validatingdate = (DateTime.Now).AddDays(-60);
            if (employee.HireDate < validatingdate)
            {
                return "Hire Date must be within the last 60 days";
            }
            var departmentExists=hrDbContext.Departments.Any(d=>d.DepartmentId == employee.DepartmentId);
            if (!departmentExists)
            {
                return "Invalid Department ID";
            }
            return string.Empty;
        }
    }
}