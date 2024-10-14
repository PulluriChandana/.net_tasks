using System;
using System.Collections.Generic;
using System.Linq;
class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Enter the number of elements in list");
        int n=int.Parse(Console.ReadLine());
        List<int> numbers = new List<int>();
        for(int i = 0;i<n;i++)
        {
            Console.WriteLine($"Element {i + 1}");
            int e=int.Parse(Console.ReadLine());
            numbers.Add(e);
        }
        Console.Write("Enterd elements in the list are: ");
        Console.WriteLine(string.Join(",",numbers));
        double avg = numbers.Average();
        Console.WriteLine("Average: " + avg);
        int sum=numbers.Sum();
        Console.WriteLine("Sum is: " + sum);
        int max=numbers.Max();
        Console.WriteLine("Max is: " + max);
        int min=numbers.Min();
        Console.WriteLine("Min is: " + min);
       var sorted = numbers.OrderByDescending(x => x).ToList();
        Console.WriteLine("Sorted Numbers: " + string.Join(",",sorted));
    }
}