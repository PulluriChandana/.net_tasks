using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace Task5_RESTAPI.Db
{
   public static class Employees
    {
        public static List<Employee> employees = new List<Employee>
        {
            new Employee{Employeeno=1,EmpName="Ashu",JobTitle="Software Engineer",HireDate=new DateOnly(2024,10,15).ToDateTime(TimeOnly.MinValue),
                Salary=50000,Gender=Gender.Female,DepartmentId=1},
            new Employee{Employeeno=2,EmpName="Raju",JobTitle="Manager",HireDate=new DateOnly(2024,07,12).ToDateTime(TimeOnly.MinValue),
                Salary=75000,Gender=Gender.Male,DepartmentId=2},
            new Employee{Employeeno=3,EmpName="Meghana",JobTitle="Developer",HireDate=new DateOnly(2023,10,28).ToDateTime(TimeOnly.MinValue),Salary=85000,Gender=Gender.Female,DepartmentId=3},
            new Employee{Employeeno=4,EmpName="Sam",JobTitle="Tester",HireDate= new DateOnly(2024,9,13).ToDateTime(TimeOnly.MinValue),
                Salary=60000,Gender=Gender.Female,DepartmentId=4},
            new Employee{Employeeno=5,EmpName="Ravi",JobTitle="Tester",HireDate=new DateOnly(2020, 1, 15).ToDateTime(TimeOnly.MinValue),
            Salary=60000,Gender=Gender.Male,DepartmentId=5}
        };
    }
    public class Employee
    {
        public int Employeeno {  get; set; }
      
        [Required(ErrorMessage = "Name is requried")]
        public string? EmpName {  get; set; }
        [Required(ErrorMessage ="Job Title is requried")]
        public string? JobTitle {  get; set; }
        public DateTime HireDate { get; set; }
        public int Salary {  get; set; }
        public Gender Gender { get; set; }
        public int DepartmentId { get; set; }
    }
    public enum Gender
    {
        Male=0,Female=1,Others=2
    }
}
