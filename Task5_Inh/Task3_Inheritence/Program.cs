using System;
using System.IO.Pipes;
using System.Net.Http.Headers;
abstract class People
{
    public string? Name { get; set; }
    public int Age { get; set; }
    public Gender Gender { get; set; }
    //Name Validation
    public static string GetValidName(string role)
    {
        string Name;
        do
        {
            Console.WriteLine($"Enter {role} name( cannot be empty):");
            Name = Console.ReadLine();
        } while (string.IsNullOrEmpty(Name) || (string.IsNullOrWhiteSpace(Name)));
        return Name;
    }
    //Age Validation
    public static int GetValidAge(int minage, string role)
    {
        int age;
        do
        {
            Console.WriteLine($"Enter {role} age (must be above age {minage})");
        } while (!int.TryParse(Console.ReadLine(), out age) || age <= minage);
        return age;
    }
    public virtual void ShowMyDetails()
    {
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Age: {Age}");
        Console.WriteLine($"Gender: {Gender}");
    }

}
public enum Gender
{
    Male = 1, Female = 2, Others = 3
}
class Student : People
{
    public int StudentId { get; set; }

    public string? PhoneNumber { get; set; }

    public override void ShowMyDetails()
    {
        Console.WriteLine("Studnet Details");
        Console.WriteLine($"Student Id: {StudentId}");
        base.ShowMyDetails();
        Console.WriteLine($"Student PhoneNumber: {PhoneNumber}");
    }

}
class Teacher : People
{
    public string? Subject { get; set; }
    public override void ShowMyDetails()
    {
        Console.WriteLine("Teacher Details");
        base.ShowMyDetails();
        Console.WriteLine($"Subject: {Subject}");
    }
}
class Program
{
    public static void Main(string[] args)
    {
        var peoples = new List<People>();
        while (true)
        {
            var student = GetStudentDetails();
            student.ShowMyDetails();
            //if true validate
            //peoples.Add( student );
            if (student != null && student.Age > 16)
            {
                peoples.Add(student);
                Console.WriteLine("Student added to the list");
            }
            else
            {
                Console.WriteLine("Invalid student details. Not added to the list");
            }
            //checking if you want to add one more student
            Console.WriteLine("Do you want to add another student (yes/no)?");
            string response = Console.ReadLine().ToLower();
            if (response != "yes")
            {
                break;
            }
        }
        var teacher = GetTeacherDetails();
        teacher.ShowMyDetails();
        Console.WriteLine("Enter search option");
        var Message = "Invalid Option";
        try
        {
            var option = int.Parse(Console.ReadLine());


            switch (option)
            {
                case 1:
                    Console.WriteLine("Enter search keyword:");
                    string keyword = Console.ReadLine();
                    SearchStudents(peoples, keyword);
                    break;
                default:
                    Console.WriteLine(Message);
                    break;
            }
        }
        catch (FormatException ex)
        {
            Console.WriteLine(Message);
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine("Null Exception");
        }
        catch (Exception Handling)
        {
            Console.WriteLine("General Exception");
        }

    }
    private static Teacher GetTeacherDetails()
    {
        var teacher = new Teacher();
        Console.WriteLine($"Enter Teacher Name:");
        teacher.Name = Teacher.GetValidName("Teacher");
        Console.WriteLine("Enter Teacher Subject:");
        teacher.Subject = Console.ReadLine();
        teacher.Age = Teacher.GetValidAge(16, "Teacher");
        Console.WriteLine("Select Gender (1: Male, 2: Female, 3: Others):");
        teacher.Gender = (Gender)Enum.Parse(typeof(Gender), Console.ReadLine());
        return teacher;
    }
    private static Student GetStudentDetails()
    {
        var student = new Student();
        Console.WriteLine("Enter Student Id:");
        student.StudentId = int.Parse(Console.ReadLine());
        //Console.WriteLine($"Enter Student Name:");
        student.Name = Student.GetValidName("Student");
        Console.WriteLine("Enter Student Phone Number:");
        student.PhoneNumber = Console.ReadLine();
        student.Age = Student.GetValidAge(16, "Student");
        Console.WriteLine("Select Gender (1: Male, 2: Female, 3: Others):");
        student.Gender = (Gender)Enum.Parse(typeof(Gender), Console.ReadLine());
        return student;
    }
    private static void SearchStudents(List<People> peoples, string keyword)
    {

        // first or default
        // where
        // any
        // all
        // disting
        //orderby
        // groupBy

        List<string?> MatchingStudents = new List<string?>();
        MatchingStudents = peoples.Where(c => c.Age == 22).Select(c => c.Name).ToList();

        if (MatchingStudents.Count > 0)
        {
            Console.WriteLine($"Matching Students  {string.Join(", ", MatchingStudents)}");
        }
        else
        {
            Console.WriteLine("No matching students found");
        }

    }
}