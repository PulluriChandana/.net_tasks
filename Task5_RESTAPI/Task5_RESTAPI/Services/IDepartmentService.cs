using Task5_RESTAPI.Db;

namespace Task5_RESTAPI.Services
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
