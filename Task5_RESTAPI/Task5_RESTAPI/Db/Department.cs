using System.ComponentModel.DataAnnotations;

namespace Task5_RESTAPI.Db
{
    public static class Departments
    {
        public static List<Department> departments = new List<Department>
        {
            new Department {DepartmentId=1,Name="John",Location="Hyderabad"},
            new Department {DepartmentId=2,Name="Catriee",Location="Kolkata"},
            new Department {DepartmentId=3,Name="Pavan",Location="Kerala"},
            new Department {DepartmentId=4,Name="Sai",Location="Banglore"},
            new Department {DepartmentId=5,Name="Kumar",Location="Hyderabad"},
            new Department {DepartmentId=6,Name="Joe",Location="Kochi"},
            new Department {DepartmentId=7,Name="Jostyana",Location="Hyderabad"},
            new Department {DepartmentId=8,Name="Jeshvitha",Location="Hyderabad"},
        };
    }
    public class Department
    {
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "Name is requried")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Location is requried")]
        public string? Location { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
