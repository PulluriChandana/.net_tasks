using System.ComponentModel.DataAnnotations;

namespace TASKS_6_MVC_.Models
{
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
