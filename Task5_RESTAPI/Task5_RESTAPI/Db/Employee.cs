using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace Task5_RESTAPI.Db
{
   public static class Employees
    {
        public static List<Employee> employees = new List<Employee>
        {
            new Employee{Employeeno=1,EmpName="Ashu",JobTitle="Software Engineer",
                HireDate=new DateOnly(2024,10,15).ToDateTime(TimeOnly.MinValue),
                DateOfBirth=new DateTime(2002,08,21),
                Salary=50000,Gender=Gender.Female,DepartmentId=1},
            new Employee{Employeeno=2,EmpName="Raju",JobTitle="Manager",
                HireDate=new DateOnly(2024,07,12).ToDateTime(TimeOnly.MinValue),
                DateOfBirth=new DateTime(2002,08,21),
                Salary=75000,Gender=Gender.Male,DepartmentId=2},
            new Employee{Employeeno=3,EmpName="Meghana",JobTitle="Developer",
                HireDate=new DateOnly(2023,10,28).ToDateTime(TimeOnly.MinValue),
                DateOfBirth=new DateTime(2002,08,21),
                Salary=85000,Gender=Gender.Female,DepartmentId=3},
            new Employee{Employeeno=4,EmpName="Sam",JobTitle="Tester",
                HireDate= new DateOnly(2024,9,13).ToDateTime(TimeOnly.MinValue),
                DateOfBirth=new DateTime(2002,08,21),
                Salary=60000,Gender=Gender.Female,DepartmentId=4},
            new Employee{Employeeno=5,EmpName="Ravi",JobTitle="Tester",
                HireDate=new DateOnly(2020, 1, 15).ToDateTime(TimeOnly.MinValue),
                DateOfBirth=new DateTime(2002,08,21),
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
        public DateTime DateOfBirth { get; set; }
        [NotMapped]
        public int Age
        {
            get => CalculateAge(DateOfBirth);
        }
        public int Salary {  get; set; }
        public Gender Gender { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public int? RoleId { get; set; }
        public Role Role { get; set; }
        private static int CalculateAge(DateTime dateOfBirth)
        {
            DateTime today = DateTime.Now;
            int age = today.Year - dateOfBirth.Year;
            if (today < dateOfBirth.AddYears(age))
            {
                age--;
            }
            return age;
        }
    }
    public enum Gender
    {
        Male=0,Female=1,Others=2
    }
}

