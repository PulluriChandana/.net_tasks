using Task5_RESTAPI.Db;

namespace Task5_RESTAPI.Services
{
    public interface IEmployeeService
    {
        bool Create(Employee employee);
        bool Upadte(int empno, Employee employee);
        bool DeleteByNo(int empno);
        Employee GetByNo(int empno);
        List<Employee> GetAll();
        List<Employee> GetByGender(Gender gender);
        List<Employee> GetByDepartmentId(int departmentId);
    }
}
