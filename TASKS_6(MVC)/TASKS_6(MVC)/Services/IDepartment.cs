using TASKS_6_MVC_.Models;

namespace TASKS_6_MVC_.Services
{
    public interface IDepartmentService
    {
        bool Create(Department department);
        bool Update(int id, Department department);
        bool DeleteById(int id);
        Department GetById(int id);
        List<Department> GetAll();
        List<Department> GetDepartmentByLocation(string location);

    }
}
