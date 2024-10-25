using Task5_RESTAPI.Db;
using Task5_RESTAPI.Db.DTO;
using Task5_RESTAPI.Db.Filter;

namespace Task5_RESTAPI.Services
{
    public interface IEmployeeService
    {
        bool Create(Employee employee);
        bool Upadte(int empno, Employee employee);
        bool DeleteByNo(int empno);
        Employee GetByNo(int empno);
        List<Employee> GetAll(EmployeeFilter filter);
        List<Employee> GetByGender(Gender gender);
        List<Employee> GetByDepartmentId(int departmentId);
        List<EmployeDTO> GetAllEmployeeWithDepartment(string location);
        List<EmployeeDepartmentReport> GetEmployeeDepartmentReport();
    }
}
