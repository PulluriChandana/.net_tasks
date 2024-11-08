using TASKS_6_MVC_.Models;
namespace TASKS_6_MVC_.Services
{   
    public interface IEmployeeService
    {
        bool Create(Employee employee);
        bool Upadte(int empno, Employee employee);
        bool DeleteByNo(int empno);
        Employee GetByEmpNo(int empno);
        List<Employee> GetAll();
        List<Employee> GetByGender(Gender gender);
        List<Employee> GetByDepartmentId(int departmentId);
    }
}