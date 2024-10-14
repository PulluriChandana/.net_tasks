class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Enter number of elements in an array");
        var n = int.Parse(Console.ReadLine());
        var numbers = new int[n];
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"Element {i + 1}");
            numbers[i] = int.Parse(Console.ReadLine());
        }
        var sum=numbers.Sum();
        Console.WriteLine("Sum is: "+ sum);
        var max=numbers.Max();
        Console.WriteLine("Max is: " + max);
        var min=numbers.Min();
        Console.WriteLine("Min is: " + min);
        var sortednumbers= numbers.OrderBy(x => x).ToArray();
        Console.WriteLine("Sorted numbers: "+string.Join(", ", sortednumbers));
        var sortedDescending=numbers.OrderByDescending(x => x).ToArray();
        Console.WriteLine("Sorted numbers descending :" + string.Join(",", sortedDescending));
    }
}
