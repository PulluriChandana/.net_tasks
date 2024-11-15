using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace TASKS_6_MVC_.Models
{
    public class Employee
    {
        public int Employeeno { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string? EmpName { get; set; }
        [Required(ErrorMessage = "Job Title is required")]
        public string? JobTitle { get; set; }
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }
        public DateTime DateOfBirth { get; set; }
        [Range(0, int.MaxValue,ErrorMessage ="Salary must be a positive Integer")]
        public int Salary { get; set; }
        public Gender Gender { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
    public enum Gender
        {
            Male = 0, Female = 1, Others = 2
        }
}
