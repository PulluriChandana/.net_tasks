using System;
using System.Collections.Generic;
using System.Linq;
class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Enter a list of numbers by spaces:");
        var input=Console.ReadLine();
        var numbers = input.Split(' ')
                    .Select(num => int.TryParse(num, out var n) ? n : (int?)null)
                    .Where(n => n > 0) 
                    .ToList();
        Console.WriteLine($"You have entered list is:{string.Join(", ",numbers)}");
        var firsteven = numbers.FirstOrDefault(n => n % 2 == 0);
        Console.WriteLine($"First Even:{firsteven}");
        var Orderednumbers=numbers.OrderBy(n => n).ToList();
        Console.WriteLine($"Ordered numbers: {string.Join(", ", Orderednumbers)}");
        var distinct=numbers.Distinct().ToList();
        Console.WriteLine($"Distinct numbers:{string.Join(", ", distinct)}");
        var even=numbers.Where(n=>n% 2 == 0).ToList();
        Console.WriteLine($"Even numbers:{string.Join("," ,even)}");
        var groupednumbers = numbers.GroupBy(n => n % 2 == 0 ? "Even" : "Odd");
        foreach( var group in groupednumbers )
        {
            Console.WriteLine($"{ group.Key}:{string.Join(", ",group)}");
        }
    }
}
